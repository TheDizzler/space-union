using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_Structures
{
    /// <summary>
    /// Contains the information of a room.
    /// </summary>
    public class RoomInfo
    {
        public Dictionary<string, GameData> Players { get; set; }
        public int RoomNumber { get; set; }
        public string RoomName { get; set; }
        public Player Host { get; set; }
        public bool InGame { get; set; }

        public RoomInfo(Dictionary<string, GameData> players, 
                        int roomNumber, 
                        string roomName, 
                        Player host, 
                        bool inGame)
        {
            Players    = players;
            RoomNumber = roomNumber;
            RoomName   = roomName;
            Host       = host;
            InGame     = inGame;
        }
    }
}
