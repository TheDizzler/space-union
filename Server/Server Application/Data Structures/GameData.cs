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
        public Player Player { get; set; }

        /// <summary>
        /// The horizontal X position of the player's ship.
        /// </summary>
        public float XPosition { get; set; }

        /// <summary>
        /// The vertical Y position of the player's ship.
        /// </summary>
        public float YPosition { get; set; }

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
        /// <summary>
        /// Indicates whether or not the player
        /// is currently active on the map or not.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// True indicates Team Red, False indicates
        /// Team Blue.
        /// </summary>
        public bool Team { get; set; }

        public Projectile[] Bullets { get; set; }

        //projectiles fired, direction, velocity

        public GameData()
        {
            Type = 1;
        }

        //The initialization of positions
        public byte SpawnPosition { get; set; }
    }
}
