﻿using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
	public class CameraPurchaseOk : AbstractComposerVoid
	{
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				session.Send (message);
			}
		}
	}
}

