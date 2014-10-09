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

        public ErrorMessage(Player player, string messageCode)
        {
            Player = player;
            MessageCode = messageCode;
            Type = 3;
        }
    }
}
