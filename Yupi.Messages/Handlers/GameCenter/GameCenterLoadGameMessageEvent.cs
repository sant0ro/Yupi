﻿namespace Yupi.Messages.GameCenter
{
    using System;

    public class GameCenterLoadGameMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<GameCenterGameAchievementsMessageComposer>().Compose(session);
            router.GetComposer<GameCenterLeaderboardMessageComposer>().Compose(session, session.Info);
            router.GetComposer<GameCenterLeaderboard2MessageComposer>().Compose(session, session.Info);
            router.GetComposer<GameCenterGamesLeftMessageComposer>().Compose(session);
            router.GetComposer<GameCenterPreviousWinnerMessageComposer>().Compose(session);
            router.GetComposer<GameCenterAllAchievementsMessageComposer>().Compose(session);
            router.GetComposer<GameCenterEnterInGameMessageComposer>().Compose(session);
        }

        #endregion Methods
    }
}