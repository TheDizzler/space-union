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
    /// Here are the different possible type values:
    /// 0 = Player class
    /// 1 = GameData class
    /// 2 = GameMessage class
    /// 3 = ErrorMessage class
    /// </summary>
    [Serializable]
    class Data
    {
        /// <summary>
        /// The type of this object.
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// Constructs a Data object.
        /// </summary>
        /// <param name="type">A variable used to define what class this is.</param>
        public Data(byte type)
        {
            this.Type = type;
        }
    }
}
