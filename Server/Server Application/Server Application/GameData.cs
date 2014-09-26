using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application
{
    [Serializable]
    class GameData : Data
    {

        int gameroom;
        string ipaddress;
        short port;
        short xposition;
        short yposition;
        float angle;
        byte health;
        //projectiles fired, direction, velocity


        public GameData(byte type) : base(type) { }

        public int GameRoom
        {
            set { gameroom = value; }
            get { return gameroom; }
        }

        public string IP
        {
            set { ipaddress = value; }
            get { return ipaddress; }
        }

        public short Port
        {
            set { port = value; }
            get { return port; }
        }

        public short XPosition
        {
            set { xposition = value; }
            get { return xposition; }
        }

        public short YPosition
        {
            set { yposition = value; }
            get { return yposition; }
        }

        public float Angle
        {
            set { angle = value; }
            get { return angle; }
        }

        public byte Health
        {
            set { health = value; }
            get { return health; }
        }
    }
}
