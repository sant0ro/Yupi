﻿using System;
using Yupi.Emulator.Game.Rooms;

namespace Yupi.Messages.Rooms
{
	public class RoomGetFilterMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint roomId = request.GetUInt32();

			Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);

			if (room == null || !room.CheckRights(session, true))
				return;

			router.GetComposer<RoomLoadFilterMessageComposer> ().Compose (session, room.WordFilter);
		}
	}
}
