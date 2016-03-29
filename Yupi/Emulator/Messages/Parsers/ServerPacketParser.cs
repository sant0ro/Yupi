/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Data;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Messages.Encoding;
using Yupi.Emulator.Messages.Factorys;
using Yupi.Emulator.Messages.Parsers.Interfaces;

namespace Yupi.Emulator.Messages.Parsers
{
    /// <summary>
    ///     Class ServerPacketParser.
    /// </summary>
    public class ServerPacketParser : IDataParser
    {
        /// <summary>
        ///     Delegate ReceiveRequest
        /// </summary>
        /// <param name="message">The messageBuffer.</param>
        public delegate void HandlePacket(SimpleClientMessageBuffer messageBuffer);

        /// <summary>
        ///     The int size
        /// </summary>
        private const int IntSize = sizeof (int);

        /// <summary>
        ///     The memory container
        /// </summary>
        private static readonly ServerMemoryContainer ServerMemoryContainer = new ServerMemoryContainer(10, 4072);

        /// <summary>
        ///     The _buffered data
        /// </summary>
        private readonly byte[] _bufferedData;

        /// <summary>
        ///     The _buffer position
        /// </summary>
        private int _bufferPos;

        /// <summary>
        ///     The _current client
        /// </summary>
        private GameClient _currentClient;

        /// <summary>
        ///     The _current packet length
        /// </summary>
        private int _currentPacketLength;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ServerPacketParser" /> class.
        /// </summary>
        internal ServerPacketParser()
        {
            _bufferPos = 0;
            _currentPacketLength = -1;
            _bufferedData = ServerMemoryContainer.TakeBuffer();
        }

		// TODO Check this code
        /// <summary>
        ///     Handles the packet data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="length">The length.</param>
        public void HandlePacketData(byte[] data, int length)
        {
            if (length > 0 && _currentClient != null)
            {
                short messageId = 0;

                try
                {
                    int pos;

                    for (pos = 0; pos < length;)
                    {
                        if (_currentPacketLength == -1)
                        {
                            if (length < IntSize)
                            {
                                BufferCopy(data, length);
                                break;
                            }

                            _currentPacketLength = HabboEncoding.DecodeInt32(data, ref pos);
                        }

                        if (_currentPacketLength < 2 || _currentPacketLength > 4096)
                        {
                            _currentPacketLength = -1;

                            break;
                        }

                        if (_currentPacketLength == length - pos + _bufferPos)
                        {
                            if (_bufferPos != 0)
                            {
                                BufferCopy(data, length, pos);

                                pos = 0;

                                messageId = HabboEncoding.DecodeInt16(_bufferedData, ref pos);

                                HandleMessage(messageId, _bufferedData, 2, _currentPacketLength);
                            }
                            else
                            {
                                messageId = HabboEncoding.DecodeInt16(data, ref pos);
                                HandleMessage(messageId, data, pos, _currentPacketLength);
                            }

                            pos = length;
                            _currentPacketLength = -1;
                        }
                        else
                        {
                            int remainder = length - pos - (_currentPacketLength - _bufferPos);

                            if (_bufferPos != 0)
                            {
                                int toCopy = remainder - _bufferPos;

                                BufferCopy(data, toCopy, pos);

                                int zero = 0;

                                messageId = HabboEncoding.DecodeInt16(_bufferedData, ref zero);

                                HandleMessage(messageId, _bufferedData, 2, _currentPacketLength);
                            }
                            else
                            {
                                messageId = HabboEncoding.DecodeInt16(data, ref pos);

                                HandleMessage(messageId, data, pos, _currentPacketLength);

                                // ReSharper disable once RedundantAssignment
                                pos -= 2;
                            }

                            _currentPacketLength = -1;

                            pos = length - remainder;
                        }
                    }
                }
                catch (Exception exception)
                {
                    YupiLogManager.LogException(exception, $"Failed Parse Packet #{messageId}.");
                }
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() => ServerMemoryContainer.GiveBuffer(_bufferedData);

		// TODO THIS IS IN NO WAY AN EXACT COPY !!!

        /// <summary>
        ///     Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone() => new ServerPacketParser();

        /// <summary>
        ///     Sets the connection.
        /// </summary>
        /// <param name="me">Me.</param>
        public void SetConnection(GameClient me) => _currentClient = me;

        /// <summary>
        ///     Handles the messageBuffer.
        /// </summary>
        /// <param name="messageId">The messageBuffer identifier.</param>
        /// <param name="packetContent">Content of the packet.</param>
        /// <param name="position">The position.</param>
        /// <param name="packetLength">Length of the packet.</param>
        private void HandleMessage(int messageId, byte[] packetContent, int position, int packetLength)
        {
			// TODO Why does every client need a own message handler?
            using (SimpleClientMessageBuffer simpleClientMessageBuffer = ClientMessageFactory.GetClientMessage(messageId, packetContent, position, packetLength))
                if (_currentClient.GetMessageHandler() != null)
                    _currentClient.GetMessageHandler().HandleRequest(simpleClientMessageBuffer); 
        }

        // ReSharper disable once MethodOverloadWithOptionalParameter
        /// <summary>
        ///     Buffers the copy.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="bytes">The bytes.</param>
        /// <param name="offset">The offset.</param>
        private void BufferCopy(byte[] data, int bytes, int offset = 0)
        {
			// TODO Reimplement properly
            for (int i = 0; i < bytes - offset; i++)
                _bufferedData[_bufferPos++] = data[i + offset];
        }
    }
}