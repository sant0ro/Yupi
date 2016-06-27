﻿using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class RoomRightsLevelMessageComposer : AbstractComposer<int>
	{
		// TODO Level should be enum
		public override void Compose (Yupi.Protocol.ISender session, int level)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (level);
				session.Send (message);
			}
		}
	}
}
