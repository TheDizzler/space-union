using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application
{
    [Serializable]
    class Data
    {
        byte type;

        public Data(byte type)
        {
            this.type = type;
        }

        public byte Type
        {
            set { type = value; }
            get { return type; }
        }
    }
}
