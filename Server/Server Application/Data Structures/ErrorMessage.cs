using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures
{
    /// <summary>
    /// Message containing the error code.
    /// 0 - non-existent user / incorrect password
    /// 1- user blocked
    /// </summary>
    [Serializable]
    public class ErrorMessage : Data
    {
        public Player Player { get; set; }
        public int MessageCode { get; set; }

        public ErrorMessage()
        {
            Type = 3;
        }

<<<<<<< HEAD
        /// <summary>
        /// A constructor for this class, does not initiate any data.
        /// </summary>
        /// <param name="type">The type of the class, used to cast an object to this class.</param>
        public ErrorMessage(byte type) : base(type) { }

        public ErrorMessage(byte type, Player player, int messageCode) : base(type)
=======
        public ErrorMessage(Player player, string messageCode)
>>>>>>> c97c36691afa2561947508443dc0f218a9411c96
        {
            Player = player;
            MessageCode = messageCode;
            Type = 3;
        }
    }
}
