﻿// ---------------------------------------------------------------------------------
// <copyright file="NewNavigatorResizeEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Navigator
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Domain.Components;
    using Yupi.Model.Repository;

    public class NewNavigatorResizeEvent : AbstractHandler
    {
        #region Fields

        private IRepository<UserPreferences> PreferenceRepository;

        #endregion Fields

        #region Constructors

        public NewNavigatorResizeEvent()
        {
            PreferenceRepository = DependencyFactory.Resolve<IRepository<UserPreferences>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int x = request.GetInteger();
            int y = request.GetInteger();
            int width = request.GetInteger();
            int height = request.GetInteger();

            UserPreferences preferences = session.Info.Preferences;

            preferences.NewnaviX = x;
            preferences.NewnaviY = y;
            preferences.NavigatorWidth = width;
            preferences.NavigatorHeight = height;

            PreferenceRepository.Save(preferences);
        }

        #endregion Methods
    }
}