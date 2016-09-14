﻿namespace Yupi.Messages
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Yupi.Messages.Contracts;
    using Yupi.Model.Domain;
    using Yupi.Net;
    using Yupi.Protocol;
    using Yupi.Protocol.Buffers;

    public class Router : Yupi.Protocol.IRouter
    {
        #region Fields

        public static Router Default;

        private static readonly log4net.ILog Logger = log4net.LogManager
            .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Dictionary<short, AbstractHandler> Incoming;
        private PacketLibrary library;
        private Assembly MessageAssembly;
        private Dictionary<Type, IComposer> Outgoing;
        private ServerMessagePool pool;

        #endregion Fields

        #region Constructors

        public Router(string release, string configDir, Assembly messageAssembly)
        {
            pool = new ServerMessagePool();
            MessageAssembly = messageAssembly;
            library = new PacketLibrary(release, configDir);
            LoadHandlers();
            LoadComposers();
        }

        #endregion Constructors

        #region Methods

        public T GetComposer<T>()
        {
            IComposer composer;
            Outgoing.TryGetValue(typeof(T), out composer);

            if (composer == null)
            {
                Logger.ErrorFormat("Invalid composer {0}", typeof(T).Name);
            }

            if (Logger.IsDebugEnabled)
            {
                Logger.WarnFormat("Compose {0}", typeof(T).Name);
            }

            return (T) composer;
        }

        public void Handle(Habbo session, ClientMessage message)
        {
            AbstractHandler handler;
            Incoming.TryGetValue(message.Id, out handler);

            if (handler == null)
            {
                Logger.WarnFormat("Unknown incoming message {0}", message.Id);
            }
            else
            {
                if (Logger.IsDebugEnabled)
                {
                    Logger.WarnFormat("Handle [{0}] {1} for [{2}]: {3}",
                        message.Id,
                        handler.GetType().Name,
                        session.Session.RemoteAddress,
                        Encoding.Default.GetString(message.GetBody())
                    );
                }

                try
                {
                    handler.HandleMessage(session, message, this);
                }
                catch (Exception e)
                {
                    Logger.Error("Exception thrown in Handler", e);
                }
            }
        }

        private IEnumerable<Type> GetImplementing<T>()
        {
            return
                MessageAssembly.GetTypes()
                    .Where(p => typeof(T).IsAssignableFrom(p) && p.GetConstructor(Type.EmptyTypes) != null);
        }

        private void LoadComposers()
        {
            Outgoing = new Dictionary<Type, IComposer>();

            IEnumerable<Type> composers = GetImplementing<IComposer>();

            foreach (Type composerType in composers)
            {
                short id = library.GetOutgoingId(composerType.Name);

                if (id > 0)
                {
                    IComposer composer = (IComposer) Activator.CreateInstance(composerType);
                    composer.Init(id, pool);

                    // TODO Remove one Add
                    Outgoing.Add(composerType, composer);
                    if (composerType.BaseType.Namespace.StartsWith("Yupi.Messages.Contracts")
                        && !composerType.BaseType.Name.StartsWith("AbstractComposer"))
                    {
                        Outgoing.Add(composerType.BaseType, composer);
                    }
                }
            }
        }

        // TODO Fix handler names in *.incoming
        private void LoadHandlers()
        {
            Incoming = new Dictionary<short, AbstractHandler>();

            IEnumerable<Type> handlers = GetImplementing<AbstractHandler>();

            foreach (Type handlerType in handlers)
            {
                short id = library.GetIncomingId(handlerType.Name);

                if (id > 0)
                {
                    if (Incoming.ContainsKey(id))
                    {
                        Logger.ErrorFormat("Duplicate Handler Id [{0}]", id);
                    }
                    else
                    {
                        AbstractHandler handler = (AbstractHandler) Activator.CreateInstance(handlerType);
                        Incoming.Add(id, handler);
                    }
                }
            }
        }

        #endregion Methods
    }
}