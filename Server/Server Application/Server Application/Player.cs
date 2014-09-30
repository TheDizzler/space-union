using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application
{
    /// <summary>
    /// This class contains basic data about the player.
    /// </summary>
    class Player : Data
    {
        /// <summary>
        /// The username of the current player.
        /// </summary>
        string username;
        /// <summary>
        /// The password of the current player.
        /// </summary>
        string password;
        /// <summary>
        /// The IP address of the current player.
        /// </summary>
        string ipaddress;

        public Player(byte type) : base(type) {}

        /// <summary>
        /// The username of the current player.
        /// </summary>
        public string Username
        {
            set { username = value; }
            get { return username; }
        }
        /// <summary>
        /// The password of the current player.
        /// </summary>
        public string Password
        {
            set { password = value; }
            get { return password; }
        }
        /// <summary>
        /// The IP address of the current player.
        /// </summary>
        public string IPAddress
        {
            set { password = value; }
            get { return password; }
        }
    }
}
