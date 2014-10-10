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
        public ErrorMessage(Player player, int messageCode)
        {
            Player = player;
            MessageCode = messageCode;
            Type = 3;
        }
    }
}
