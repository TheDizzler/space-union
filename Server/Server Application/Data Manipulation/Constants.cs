using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Manipulation
{
    public class Constants
    {
        /// <summary>
        /// The number of total UDP outgoing or incoming connections. This number
        /// is equal to the maximum amount of players who can be in a game room.
        /// </summary>
        public const int NumberOfUdpClients = 6;
        /// <summary>
        /// The number of TCP clients, specifically used for login requests and 
        /// message sent through the in game chat.
        /// </summary>
        public const int NumberOfTcpClients = 1;
        /// <summary>
        /// The first UDP port which listens to incoming data.
        /// </summary>
        public const int UDPClientToServerPort = 6940;
        /// <summary>
        /// The first UDP port which sends data.
        /// </summary>
        public const int UDPServerToClientPort = 6960;
        /// <summary>
        /// The port used by the TCP login request client to send data to a game client.
        /// </summary>
        public const int TCPMessageListener = 6980;
        /// <summary>
        /// The port used by the TCP login request client to send data to a game client.
        /// </summary>
        public const int TCPMessageClient = 6983;

        public const int GamePeriod = 10;

        // MESSAGE TYPES

        // ********* MESSAGE TYPE 0 IS RESERVED *********

        /// <summary>
        /// Game data = 1.
        /// </summary>
        public const byte GAME_DATA = 1;
        /// <summary>
        /// Chat message = 2.
        /// </summary>
        public const byte CHAT_MESSAGE = 2;
        /// <summary>
        /// Error message = 3.
        /// </summary>
        public const byte ERROR_MESSAGE = 3;
        /// <summary>
        /// Game setup message = 4.
        /// </summary>
        public const byte GAME_SETUP_MESSAGE = 4;
        /// <summary>
        /// Game frame message = 6.
        /// </summary>
        public const byte GAME_FRAME_MESSAGE = 6;
        /// <summary>
        /// Room list = 7.
        /// </summary>
        public const byte ROOM_LIST = 7;
        /// <summary>
        /// Login request data = 8.
        /// </summary>
        public const byte LOGIN_REQUEST = 8;
        /// <summary>
        /// Player request data = 9.
        /// </summary>
        public const byte PLAYER_REQUEST = 9;

        public const byte ROOM_INFO = 10;

        // END MESSAGE TYPES


        // REQUEST TYPES

        public const byte PLAYER_REQUEST_ROOMLIST = 0;

        public const byte PLAYER_REQUEST_ROOMCREATE = 1;

        public const byte PLAYER_REQUEST_ROOMJOIN = 2;

        public const byte PLAYER_REQUEST_ROOMEXIT = 3;

        public const byte PLAYER_REQUEST_ROOMINFO = 4;

        public const byte PLAYER_REQUEST_LOGOUT = 5;

        public const byte PLAYER_REQUEST_READY = 6;

        public const byte PLAYER_REQUEST_HEARTBEAT = 7;

        public const byte PLAYER_REQUEST_SHIP = 8;

        public const byte PLAYER_REQUEST_START = 9;
        public const byte PLAYER_REQUEST_END = 10;

        // END REQUEST TYPES


        /// <summary>
        /// Maximum number of people in a room = 6.
        /// </summary>
        public const int ROOM_MAX_SIZE = 6;

        public const int MAX_NUMBER_OF_ROOMS = 100;
    }
}
