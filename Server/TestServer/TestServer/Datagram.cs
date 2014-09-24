using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestServer
{
    [Serializable]
    class Datagram
    {
        string username;
        short xlocation;
        short ylocation;
        short xnoselocation;
        short ynoselocation;
        byte health;
        byte energy;

        public byte Health
        {
            get { return health; }
            set { health = value; }
        }

    }
}
