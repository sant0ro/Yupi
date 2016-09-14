﻿namespace Yupi.Messages.User
{
    using System;

    using Yupi.Net;
    using Yupi.Protocol.Buffers;

    public class OpenBullyReportMessageComposer : Yupi.Messages.Contracts.OpenBullyReportMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(0); // TODO What does 0 mean?
                session.Send(message);
            }
        }

        #endregion Methods
    }
}