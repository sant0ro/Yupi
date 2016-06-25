﻿using System;
using Yupi.Emulator.Game.Pets;

namespace Yupi.Messages.Pets
{
	public class GetPetTrainerPanelMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint petId = request.GetUInt32();

			Room currentRoom = session.GetHabbo().CurrentRoom;

			if (currentRoom == null)
				return;

			Pet petData;

			if ((petData = currentRoom.GetRoomUserManager().GetPet(petId).PetData) == null)
				return;

			router.GetComposer<PetTrainerPanelMessageComposer> ().Compose (session, petId);
		}
	}
}

