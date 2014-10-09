using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures
{
    /// <summary>
    /// A chat message sent by a player.
    /// </summary>
    [Serializable]
    public class GameMessage : Data
    {
        /// <summary>
        /// The username of the player sending the message.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The IP address of the player sending the message.
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// The message being sent by the player.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The gameroom in which the player is currently located in.
        /// </summary>
        public int Gameroom { get; set; }

        /// <summary>
        /// The server port to which the message should be sent.
        /// </summary>
        public short Port { get; set; }

        public GameMessage()
        {
            Type = 2;
        }
    }
}
