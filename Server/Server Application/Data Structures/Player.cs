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
        /// The IP address of the current player.
        /// </summary>
        public string IPAddress { get; set; }

        public Player()
        {
            Type = 0;
        }

        public Player(byte type) : base(type) {}
    }
}
