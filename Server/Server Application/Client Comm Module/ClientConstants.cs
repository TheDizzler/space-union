﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Comm_Module
{
    class ClientConstants
    {
        /// <summary>
        /// The IP address of the server.
        /// </summary>
        public const string SERVER_IPADDRESS = "142.232.18.108";
        /// <summary>
        /// The port used by the TCP login request client to send data to a game client.
        /// </summary>
        public const int TCPLoginListener = 6980;
        /// <summary>
        /// The port used by the TCP chat message client to send data to a game client.
        /// </summary>
        public const int TCPMessageListener = 6981;
        /// <summary>
        /// The port used by the TCP error message client to send data to a game client.
        /// </summary>
        public const int TCPErrorListener = 6982;

        public const int NumberOfPlayers = 5;

        /// <summary>
        /// The interval between each message transmission.
        /// </summary>
        public const int MESSAGE_SEND_INTERVAL = 100;

        /// <summary>
        /// THe interval between each data transmission.
        /// </summary>
        public const int DATA_SEND_INTERVAL = 30;

        /// <summary>
        /// The duration of time to wait for a server response after sending a request.
        /// </summary>
        public const int CLIENT_REQUEST_WAIT_TIME = 2000;
    }
}