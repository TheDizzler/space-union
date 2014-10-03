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
        short health;
        byte energy;

        public short Health
        {
            get { return health; }
            set { health = value; }
        }

    }
}
