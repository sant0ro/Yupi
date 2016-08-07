﻿using System;

using Yupi.Messages.Items;

using Yupi.Messages.Notification;

namespace Yupi.Messages.User
{
	public class LoadItemsInventoryMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			InventoryComponent inventory = session.GetHabbo ().GetInventoryComponent ();

			int i = inventory._floorItems.Count + inventory.SongDisks.Count + inventory._wallItems.Count;

			if (i > 2800) {
				session.Send (StaticMessage.AdviceMaxItems);

				router.GetComposer<SuperNotificationMessageComposer> ()
					.Compose (session, "${generic.notice}", 
					"You've exceeded the maximum furnis inventory. " +
					"Only 2800 will show furnis if you want to see the others, places some Furni in your rooms.");
				
			} else {
				router.GetComposer<LoadInventoryMessageComposer> ().Compose (session, inventory._floorItems.Values, inventory._wallItems.Values, inventory.SongDisks.Values);
			}
		}
	}
}
