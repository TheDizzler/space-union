using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application
{
    /// <summary>
    /// A chat message sent by a player.
    /// </summary>
    class GameMessage : Data
    {
        /// <summary>
        /// The username of the player sending the message.
        /// </summary>
        string username;
        /// <summary>
        /// The IP address of the player sending the message.
        /// </summary>
        string ipaddress;
        /// <summary>
        /// The message being sent by the player.
        /// </summary>
        string message;
        /// <summary>
        /// The gameroom in which the player is currently located in.
        /// </summary>
        int gameroom;
        /// <summary>
        /// The server port to which the message should be sent.
        /// </summary>
        short port;

        public GameMessage(byte type) : base(type) {}

        public string Username
        {
            set { username = value; }
            get { return username; }
        }
        public string IPAddress
        {
            set { username = value; }
            get { return username; }
        }
        public string Message
        {
            set { username = value; }
            get { return username; }
        }
        public int Gameroom
        {
            set { gameroom = value; }
            get { return gameroom; }
        }
        public short Port
        {
            set { port = value; }
            get { return port; }
        }
    }
}
