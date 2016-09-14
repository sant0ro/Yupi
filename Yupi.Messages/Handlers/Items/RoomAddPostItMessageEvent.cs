﻿// ---------------------------------------------------------------------------------
// <copyright file="RoomAddPostItMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Items
{
    using System;

    public class RoomAddPostItMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            if (session.Room == null || !session.Room.Data.HasRights(session.Info))
                return;

            int id = request.GetInteger();
            string locationData = request.GetString();

            /*
            UserItem item = session.GetHabbo ().GetInventoryComponent ().GetItem (id);

            if (item == null)
                return;

            WallCoordinate wallCoord = new WallCoordinate (":" + locationData.Split (':') [1]);

            RoomItem item2 = new RoomItem (item.Id, room.RoomId, item.BaseItem.Name, item.ExtraData, wallCoord, room,
                                  session.GetHabbo ().Id, item.GroupId, false);

            if (room.GetRoomItemHandler ().SetWallItem (session, item2))
                session.GetHabbo ().GetInventoryComponent ().RemoveItem (id, true);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}