using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_Structures
{
    [Serializable]
    public class RoomList : Data
    {
        /// <summary>
        /// The player to receive this message.
        /// </summary>
        public Player Receiver { get; set; }
        public List<RoomInfo> RoomInfoList { get; set; }

        public RoomList(Player receiver, List<RoomInfo> list)
        {
            Type = 7;
            Receiver = receiver;
            RoomInfoList = list;
        }
    }
}
