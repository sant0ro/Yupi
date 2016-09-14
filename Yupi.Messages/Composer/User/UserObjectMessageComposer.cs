﻿namespace Yupi.Messages.User
{
    using System;
    using System.Globalization;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;
    using Yupi.Util;

    public class UserObjectMessageComposer : Yupi.Messages.Contracts.UserObjectMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo habbo)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(habbo.Id);
                message.AppendString(habbo.Name);
                message.AppendString(habbo.Look);
                message.AppendString(habbo.Gender.ToUpper());
                message.AppendString(habbo.Motto);
                message.AppendString(string.Empty);
                message.AppendBool(false);
                message.AppendInteger(habbo.Respect.Respect);
                message.AppendInteger(habbo.Respect.DailyRespectPoints);
                message.AppendInteger(habbo.Respect.DailyPetRespectPoints);
                message.AppendBool(true);
                message.AppendString(habbo.LastOnline.ToUnix().ToString());
                message.AppendBool(habbo.CanChangeName());
                message.AppendBool(false);

                session.Send(message);
            }
        }

        #endregion Methods
    }
}