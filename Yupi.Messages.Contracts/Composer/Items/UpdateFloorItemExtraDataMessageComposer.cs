﻿namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class UpdateFloorItemExtraDataMessageComposer : AbstractComposer<FloorItem>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, FloorItem item)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}