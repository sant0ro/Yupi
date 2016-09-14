﻿// ---------------------------------------------------------------------------------
// <copyright file="MassCredits.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
/* copyright */
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class MassCredits. This class cannot be inherited.
    /// </summary>
     public sealed class MassCredits : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MassCredits" /> class.
        /// </summary>
        public MassCredits()
        {
            MinRank = 8;
            Description = "Gives all the users online credits.";
            Usage = ":masscredits [AMOUNT]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            uint amount;

            if (!uint.TryParse(pms[0], out amount))
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("enter_numbers"));

                return true;
            }

            foreach (GameClient client in Yupi.GetGame().GetClientManager().Clients.Values)
            {
                if (client?.GetHabbo() == null)
                    continue;

                client.GetHabbo().Credits += amount;

                client.GetHabbo().UpdateCreditsBalance();

                client.SendNotif(Yupi.GetLanguage().GetVar("command_mass_credits_one_give") + amount + Yupi.GetLanguage().GetVar("command_mass_credits_two_give"));
            }

            Yupi.GetGame().GetModerationTool().LogStaffEntry(session.GetHabbo().UserName, string.Empty, "Credits", string.Concat("RoomCredits in room [", session.GetHabbo().CurrentRoom.RoomId, "] with amount [", pms[0], "]"));


            return true;
        }
    }
}