using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application
{
    class Constants
    {
        /// <summary>
        /// The number of total UDP outgoing or incoming connections. This number
        /// is equal to the maximum amount of players who can be in a game room.
        /// </summary>
        public static const int NumberOfUdpClients = 6;
        /// <summary>
        /// The number of TCP clients, specifically used for login requests and 
        /// message sent through the in game chat.
        /// </summary>
        public static const int NumberOfTcpClients = 3;
        /// <summary>
        /// The port used by the TCP login request client to send data to a game client.
        /// </summary>
        public static const int TCPLoginClient = 6989;
        /// <summary>
        /// The port used by the TCP chat message client to send data to a game client.
        /// </summary>
        public static const int TCPMessageClient = 6990;
        /// <summary>
        /// The port used by the TCP error message client to send data to a game client.
        /// </summary>
        public static const int TCPErrorClient = 6991;
        /// <summary>
        /// Login request data = 0.
        /// </summary>
        public static const byte LOGIN_REQUEST = 0;
        /// <summary>
        /// Game data = 1.
        /// </summary>
        public static const byte GAME_DATA = 1;
        /// <summary>
        /// Chat message = 2.
        /// </summary>
        public static const byte CHAT_MESSAGE = 2;
        /// <summary>
        /// Error message = 3.
        /// </summary>
        public static const byte ERROR_MESSAGE = 3;
    }
}
