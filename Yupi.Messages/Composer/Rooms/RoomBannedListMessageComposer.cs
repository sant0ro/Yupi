﻿namespace Yupi.Messages.Rooms
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class RoomBannedListMessageComposer : Yupi.Messages.Contracts.RoomBannedListMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData room)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(room.Id);
                message.AppendInteger(room.BannedUsers.Count);

                foreach (UserInfo user in room.BannedUsers)
                {
                    message.AppendInteger(user.Id);
                    message.AppendString(user.Name);
                }
                session.Send(message);
            }
        }

        #endregion Methods
    }
}