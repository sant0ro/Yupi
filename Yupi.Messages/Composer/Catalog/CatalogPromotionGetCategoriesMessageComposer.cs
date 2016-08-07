﻿using System;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.Catalog
{
	public class CatalogPromotionGetCategoriesMessageComposer : Yupi.Messages.Contracts.CatalogPromotionGetCategoriesMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(Yupi.GetGame().GetNavigator().Promomessage.Count);

				foreach (PromoCategory cat in Yupi.GetGame().GetNavigator().Promomessage.Values)
				{
					message.AppendInteger(cat.Id);
					message.AppendString(cat.Caption);
					message.AppendBool(cat.Visible);
				}

				session.Send (message);
			}
		}
	}
}
