using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures
{
    /// <summary>
    /// This is a class which contains the main data about
    /// the current player. It is converted to and from a byte
    /// array in order to transmit it or perform calculations
    /// with it.
    /// </summary>
    [Serializable]
    public class GameData : Data
    {
        /// <summary>
        /// The username of the player.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The game room in which the current player is located.
        /// </summary>
        public int GameRoom { get; set; }

        /// <summary>
        /// The IP address of the player sending this transmission.
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// The port from which the player is sending this transmission.
        /// </summary>
        public short PortSend { get; set; }
        /// <summary>
        /// The port from which the player is receiving this transmission.
        /// </summary>
        public short PortReceive { get; set; }

        /// <summary>
        /// The horizontal X position of the player's ship.
        /// </summary>
        public short XPosition { get; set; }

        /// <summary>
        /// The vertical Y position of the player's ship.
        /// </summary>
        public short YPosition { get; set; }

        /// <summary>
        /// The angle at which the player's ship is turned.
        /// </summary>
        public float Angle { get; set; }

        /// <summary>
        /// The health of the player's ship.
        /// </summary>
        public byte Health { get; set; }

        /// <summary>
        /// The amount of players the current user has killed.
        /// </summary>
        public byte Kills { get; set; }

        /// <summary>
        /// The amount of times the current user has died.
        /// </summary>
        public byte Deaths { get; set; }

        //projectiles fired, direction, velocity

        public GameData()
        {
            Type = 1;
        }

        /// <summary>
        /// A constructor for this class, does not initiate any data.
        /// </summary>
        /// <param name="type">The type of the class, used to cast an object to this class.</param>
        public GameData(byte type) : base(type) { }
    }
}
