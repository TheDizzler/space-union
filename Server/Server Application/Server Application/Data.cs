using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application
{
    /// <summary>
    /// This is a class used to encapsulate all different types
    /// of data to be sent between clients. It is necessary because
    /// upon the receiving of the object as a byte array it must be
    /// converted to an object, but it is uknown which object it is before
    /// it is cast to one. The type variable allows us to switch on it
    /// and cast the newly converted object to the right class.
    /// </summary>
    [Serializable]
    class Data
    {
        /// <summary>
        /// The type of this object.
        /// </summary>
        byte type;

        public Data(byte type)
        {
            this.type = type;
        }

        /// <summary>
        /// The type of this object.
        /// </summary>
        public byte Type
        {
            set { type = value; }
            get { return type; }
        }
    }
}
