﻿using System;


namespace Yupi.Messages.Guides
{
	public class OnGuideMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			request.GetBool();

			string idAsString = request.GetString ();

			int userId;
			int.TryParse (idAsString, out userId);

			if (userId == 0) {
				return;
			}

			string message = request.GetString();

			GuideManager guideManager = Yupi.GetGame().GetGuideManager();


			if (guideManager.GuidesCount <= 0) {
				router.GetComposer<OnGuideSessionErrorComposer> ().Compose (session);
				return;
			}

			GameClient guide = guideManager.GetRandomGuide();
			// TODO Refactor
			if (guide == null) {
				router.GetComposer<OnGuideSessionErrorComposer> ().Compose (session);
				return;
			}

			router.GetComposer<OnGuideSessionAttachedMessageComposer> ().Compose (session, false, userId, message, 30);

			router.GetComposer<OnGuideSessionAttachedMessageComposer> ().Compose (guide, true, userId, message, 15);

			// TODO Refactor
			guide.GetHabbo().GuideOtherUser = session;
			session.GetHabbo().GuideOtherUser = guide;
		}
	}
}
