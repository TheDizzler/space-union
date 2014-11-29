using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures
{
    /// <summary>
    /// This class contains basic data about the player.
    /// </summary>
    [Serializable]
    public class Player : Data
    {
        /// <summary>
        /// The username of the current player.
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// The password of the current player.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// The game room in which the current player is located.
        /// </summary>
        public int GameRoom { get; set; }
        /// <summary>
        /// The IP address of the current player.
        /// </summary>
        public string IPAddress { get; set; }
        /// <summary>
        /// The port to which the client sends data.
        /// </summary>
        public int PortSend { get; set; }
        /// <summary>
        /// The port through which the client receives data.
        /// </summary>
        public int PortReceive { get; set; }
        /// <summary>
        /// The time this packet was received. Used to time out players from the server.
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// Ready status to start a game.
        /// </summary>
        public bool Ready { get; set; }

        public byte ShipChoice { get; set; }

        public Player()
        {
            Type = 8;
        }

    }
}
