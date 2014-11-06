using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_Structures
{
    /// <summary>
    /// Contains the information of a room.
    /// </summary>
    [Serializable]
    public class RoomInfo : Data
    {
        public ConcurrentDictionary<string, GameData> Players { get; set; }
        public int RoomNumber { get; set; }
        public string RoomName { get; set; }
        public Player Host { get; set; }
        public bool InGame { get; set; }
        public Player Requester { get; set; }

        public RoomInfo(ConcurrentDictionary<string, GameData> players, 
                        int roomNumber, 
                        string roomName, 
                        Player host, 
                        bool inGame)
        {
            Type = 10;
            Players    = players;
            RoomNumber = roomNumber;
            RoomName   = roomName;
            Host       = host;
            InGame     = inGame;
        }

        public RoomInfo(ConcurrentDictionary<string, GameData> players,
                        int roomNumber,
                        string roomName,
                        Player host,
                        bool inGame,
                        Player requester)
        {
            Type = 10;
            Players = players;
            RoomNumber = roomNumber;
            RoomName = roomName;
            Host = host;
            InGame = inGame;
            Requester = requester;
        }
    }
}
