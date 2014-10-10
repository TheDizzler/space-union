using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures
{
    /// <summary>
    /// An initial message indicating the game has been
    /// created and provides the game client with the
    /// necessary connection information.
    /// </summary>
    [Serializable]
    public class GameSetupMessage : Data
    {
        public int RoomNumber { get; set; }
        public short IncomingPort { get; set; }
        public int OutgoingPort { get; set; }

        public GameSetupMessage()
        {
            Type = 4;
        }
    }
}
