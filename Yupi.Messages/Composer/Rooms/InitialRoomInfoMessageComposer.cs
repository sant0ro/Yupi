﻿using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
	public class InitialRoomInfoMessageComposer : Yupi.Messages.Contracts.InitialRoomInfoMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, RoomData room)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(room.Model.Id);
				message.AppendInteger(room.Id);
				session.Send (message);
			}
		}
	}
}
