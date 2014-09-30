using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application
{
    /// <summary>
    /// This is a class which contains the main data about
    /// the current player. It is converted to and from a byte
    /// array in order to transmit it or perform calculations
    /// with it.
    /// </summary>
    [Serializable]
    class GameData : Data
    {
        /// <summary>
        /// The game room in which the current player is located.
        /// </summary>
        int gameroom;
        /// <summary>
        /// The IP address of the player sending this transmission.
        /// </summary>
        string ipaddress;
        /// <summary>
        /// The port from which the player is sending this transmission.
        /// </summary>
        short port;
        /// <summary>
        /// The horizontal X position of the player's ship.
        /// </summary>
        short xposition;
        /// <summary>
        /// The vertical Y position of the player's ship.
        /// </summary>
        short yposition;
        /// <summary>
        /// The angle at which the player's ship is turned.
        /// </summary>
        float angle;
        /// <summary>
        /// The health of the player's ship.
        /// </summary>
        byte health;
        /// <summary>
        /// The amount of players the current user has killed.
        /// </summary>
        byte kills;
        /// <summary>
        /// The amount of times the current user has died.
        /// </summary>
        byte deaths;

        //projectiles fired, direction, velocity

        /// <summary>
        /// A constructor for this class, does not initiate any data.
        /// </summary>
        /// <param name="type">The type of the class, used to cast an object to this class.</param>
        public GameData(byte type) : base(type) { }

        /// <summary>
        /// The game room in which the current player is located.
        /// </summary>
        public int GameRoom
        {
            set { gameroom = value; }
            get { return gameroom; }
        }
        /// <summary>
        /// The IP address of the player sending this transmission.
        /// </summary>
        public string IP
        {
            set { ipaddress = value; }
            get { return ipaddress; }
        }
        /// <summary>
        /// The port from which the player is sending this transmission.
        /// </summary>
        public short Port
        {
            set { port = value; }
            get { return port; }
        }
        /// <summary>
        /// The horizontal X position of the player's ship.
        /// </summary>
        public short XPosition
        {
            set { xposition = value; }
            get { return xposition; }
        }
        /// <summary>
        /// The vertical Y position of the player's ship.
        /// </summary>
        public short YPosition
        {
            set { yposition = value; }
            get { return yposition; }
        }
        /// <summary>
        /// The angle at which the player's ship is turned.
        /// </summary>
        public float Angle
        {
            set { angle = value; }
            get { return angle; }
        }
        /// <summary>
        /// The health of the player's ship.
        /// </summary>
        public byte Health
        {
            set { health = value; }
            get { return health; }
        }
        /// <summary>
        /// The amount of players the current user has killed.
        /// </summary>
        public byte Kills
        {
            set { kills = value; }
            get { return kills; }
        }
        /// <summary>
        /// The amount of times the current user has died.
        /// </summary>
        public byte Deaths
        {
            set { deaths = value; }
            get { return deaths; }
        }
    }
}
