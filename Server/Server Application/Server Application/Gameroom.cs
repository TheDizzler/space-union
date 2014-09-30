using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application
{
    /// <summary>
    /// An actual gameroom containing a list of players and any other necessary data.
    /// </summary>
    class Gameroom
    {
        List<GameData> list;
        int gameroom;
        public Gameroom()
        {
            list = new List<GameData>();
        }
    }
}
