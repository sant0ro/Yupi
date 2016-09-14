﻿namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    // TODO Renamed from RoomsQueue
    public class RoomQueueComposer : Yupi.Messages.Contracts.RoomQueueComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int position)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(2);
                message.AppendString("visitors");
                message.AppendInteger(2);
                message.AppendInteger(1);
                message.AppendString("visitors");
                message.AppendInteger(position);
                message.AppendString("spectators");
                message.AppendInteger(1); // TODO Hardcoded
                message.AppendInteger(1);
                message.AppendString("spectators");
                message.AppendInteger(0);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}