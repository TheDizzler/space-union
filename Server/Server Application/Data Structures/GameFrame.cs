using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_Structures
{
    class GameFrame : Data
    {
        GameData[] data;
        string[] ip;
        public GameFrame(GameData[] data)
        {
            Type = 6;
            this.data = data;
            ip = new string[data.Length];
        }
    }
}
