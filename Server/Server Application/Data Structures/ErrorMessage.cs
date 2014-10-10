using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures
{
    /// <summary>
    /// Message containing the error code.
    /// 0 - non-existent user
    /// 1 - incorrect password
    /// </summary>
    [Serializable]
    public class ErrorMessage : Data
    {
        public Player Player { get; set; }
        public string MessageCode { get; set; }

        public ErrorMessage()
        {
            Type = 3;
        }

        /// <summary>
        /// A constructor for this class, does not initiate any data.
        /// </summary>
        /// <param name="type">The type of the class, used to cast an object to this class.</param>
        public ErrorMessage(byte type) : base(type) { }

        public ErrorMessage(byte type, Player player, string messageCode) : base(type)
        {
            Player = player;
            MessageCode = messageCode;
        }
    }
}
