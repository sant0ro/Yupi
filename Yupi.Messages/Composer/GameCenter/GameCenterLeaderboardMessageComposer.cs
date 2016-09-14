﻿// ---------------------------------------------------------------------------------
// <copyright file="GameCenterLeaderboardMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.GameCenter
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class GameCenterLeaderboardMessageComposer : Yupi.Messages.Contracts.GameCenterLeaderboardMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo user)
        {
            // TODO hardcoded message
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(2014);
                message.AppendInteger(49);
                message.AppendInteger(0);
                message.AppendInteger(0);
                message.AppendInteger(6526);
                message.AppendInteger(1);
                message.AppendInteger(user.Id);
                message.AppendInteger(0);
                message.AppendInteger(1);
                message.AppendString(user.Name);
                message.AppendString(user.Look);
                message.AppendString(user.Gender);
                message.AppendInteger(1);
                message.AppendInteger(18);

                session.Send(message);
            }
        }

        #endregion Methods
    }
}