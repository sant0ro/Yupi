﻿namespace Yupi.Messages.Groups
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class GroupForumNewResponseMessageComposer : Yupi.Messages.Contracts.GroupForumNewResponseMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int groupId, int threadId, int messageCount,
            UserInfo user, int timestamp, string content)
        {
            throw new NotImplementedException();
            /*
            using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
                message.AppendInteger(groupId);
                message.AppendInteger(threadId);
                message.AppendInteger(messageCount);
                message.AppendInteger(0);
                message.AppendInteger(user.Id);
                message.AppendString(user.Name);
                message.AppendString(user.Look);
                message.AppendInteger(Yupi.GetUnixTimeStamp() - timestamp);
                message.AppendString(content);
                message.AppendByte(0);
                message.AppendInteger(0);
                message.AppendString(string.Empty);
                message.AppendInteger(0);
                session.Send (message);
            }
            */
        }

        #endregion Methods
    }
}