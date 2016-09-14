﻿namespace Yupi.Messages.GameCenter
{
    using System;

    using Yupi.Protocol.Buffers;

    public class GameCenterLoadGameUrlMessageComposer : Yupi.Messages.Contracts.GameCenterLoadGameUrlMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            // TODO  hardcoded message
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(18);
                message.AppendString(UnixTimestamp.FromDateTime(DateTime.Now));
                message.AppendString(""); // TODO Reimplement: ServerExtraSettings.GameCenterStoriesUrl
                session.Send(message);
            }
        }

        #endregion Methods
    }
}