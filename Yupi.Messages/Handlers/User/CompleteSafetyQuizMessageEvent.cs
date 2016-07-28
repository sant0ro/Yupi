﻿using System;

namespace Yupi.Messages.User
{
	public class CompleteSafetyQuizMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(session, "ACH_SafetyQuizGraduate", 1);
		}
	}
}

