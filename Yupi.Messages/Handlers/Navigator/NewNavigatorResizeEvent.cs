﻿using System;


namespace Yupi.Messages.Navigator
{
	public class NewNavigatorResizeEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int x = request.GetInteger();
			int y = request.GetInteger();
			int width = request.GetInteger();
			int height = request.GetInteger();

			UserPreferences preferences = session.GetHabbo ().Preferences;

			preferences.NewnaviX = x;
			preferences.NewnaviY = y;
			preferences.NavigatorWidth = width;
			preferences.NavigatorHeight = height;
			preferences.Save();
		}
	}
}
