﻿using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
	public class SendMonsterplantIdMessageComposer : Yupi.Messages.Contracts.SendMonsterplantIdMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, uint entityId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (entityId);
				session.Send (message);
			}
		}
	}
}
