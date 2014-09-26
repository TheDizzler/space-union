using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application
{
    class DataWrapper
    {
        byte type;
        object data;

        public DataWrapper(byte type, object data)
        {
            this.type = type;
            this.data = data;
        }

        public byte Type
        {
            set { type = value; }
            get { return type; }
        }

        public object Data
        {
            set { data = value; }
            get { return type; }
        }
    }
}
