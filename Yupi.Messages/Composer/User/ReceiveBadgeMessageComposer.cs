﻿using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class ReceiveBadgeMessageComposer : Yupi.Messages.Contracts.ReceiveBadgeMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, string badgeId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(1);
				message.AppendString(badgeId);
				session.Send (message);
			}
		}
	}
}
