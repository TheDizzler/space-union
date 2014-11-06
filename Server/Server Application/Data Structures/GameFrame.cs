using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_Structures
{
    [Serializable]
    public class GameFrame : Data
    {
        public GameData[] Data { get; set; }
        public string[] IPList { get; set; }
        public GameFrame(GameData[] data)
        {
            Type = 6;
            Data = data;
            IPList = new string[data.Length];
            for (int x = 0; x < data.Length; x++ )
            {
                IPList[x] = Data[x].Player.IPAddress;
            }
        }
    }
}
