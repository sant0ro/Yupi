﻿using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Stand. This class cannot be inherited.
    /// </summary>
    internal sealed class Stand : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Stand" /> class.
        /// </summary>
        public Stand()
        {
            MinRank = 1;
            Description = "Stand";
            Usage = ":stand";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = session.GetHabbo().CurrentRoom;
            RoomUser user = room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);
            if (user == null) return true;

            if (user.IsSitting)
            {
                user.Statusses.Remove("sit");
                user.IsSitting = false;
                user.UpdateNeeded = true;
            }
            else if (user.IsLyingDown)
            {
                user.Statusses.Remove("lay");
                user.IsLyingDown = false;
                user.UpdateNeeded = true;
            }
            return true;
        }
    }
}