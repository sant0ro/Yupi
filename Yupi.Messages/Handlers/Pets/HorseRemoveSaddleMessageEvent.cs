﻿using System;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Messages.Room;

namespace Yupi.Messages.Pets
{
	public class HorseRemoveSaddleMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || (!room.RoomData.AllowPets && !room.CheckRights(session, true)))
				return;

			uint petId = request.GetUInt32();

			RoomUser pet = room.GetRoomUserManager().GetPet(petId);

			if (pet?.PetData == null || pet.PetData.OwnerId != session.GetHabbo().Id)
				return;

			pet.PetData.HaveSaddle = false;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryReactor.RunFastQuery(
					$"UPDATE pets_data SET have_saddle = '0' WHERE id = {pet.PetData.PetId}");

				queryReactor.RunFastQuery(
					$"INSERT INTO items_rooms (user_id, item_name) VALUES ({session.GetHabbo().Id}, 'horse_saddle1');");
			}

			session.GetHabbo().GetInventoryComponent().UpdateItems(true);

			router.GetComposer<SetRoomUserMessageComposer> ().Compose (room, pet);
			router.GetComposer<SerializePetMessageComposer> ().Compose (room, pet);
		}
	}
}