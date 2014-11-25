using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_Structures
{
    [Serializable]
    public class GameFrame : Data
    {
        public GameData[] Data { get; set; }
        public GameFrame(GameData[] data)
        {
            Type = 6;
            Data = data;
        }

        public object[] ipAddresses(int port)
        {
            ArrayList list = new ArrayList();
            foreach(GameData data in Data)
                if (data.Player.PortReceive == port)
                    list.Add(data.Player.IPAddress);
            return list.ToArray();
        }
    }
}
