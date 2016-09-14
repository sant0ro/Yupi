﻿namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class FloodFilterMessageComposer : Yupi.Messages.Contracts.FloodFilterMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int remainingSeconds)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(remainingSeconds);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}