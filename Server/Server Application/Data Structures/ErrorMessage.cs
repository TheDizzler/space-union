using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures
{
    /// <summary>
    /// Message containing the error code.
    /// 0 - success
    /// 1 - user blocked
    /// 2 -
    /// 3 - user is already logged in (online)
    /// 4 - non-existent user / incorrect password
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
        /// <summary>
        /// A constructor for this class, does not initiate any data.
        /// </summary>
        /// <param name="type">The type of the class, used to cast an object to this class.</param>
        public ErrorMessage(Player player, int messageCode)
        {
            Player = player;
            MessageCode = messageCode;
            Type = 3;
        }
    }
}
