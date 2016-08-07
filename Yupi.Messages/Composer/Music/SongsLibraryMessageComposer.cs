﻿using System;
using System.Collections.Specialized;
using Yupi.Protocol.Buffers;



namespace Yupi.Messages.Music
{
	public class SongsLibraryMessageComposer : Yupi.Messages.Contracts.SongsLibraryMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, HybridDictionary songs)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {

				if (songs == null)
				{
					message.AppendInteger(0);
					session.Send (message);
					return;
				}

				message.StartArray();
				foreach (UserItem userItem in songs.Values)
				{
					if (userItem == null)
					{
						message.Clear();
						continue;
					}

					message.AppendInteger(userItem.Id);

					SongData song = SoundMachineSongManager.GetSong(userItem.SongCode);
					message.AppendInteger(song?.Id ?? 0);

					message.SaveArray();
				}

				message.EndArray();

				session.Send (message);
			}
		}
	}
}
