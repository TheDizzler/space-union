using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_Structures
{
    /// <summary>
    /// Contains player request information.
    /// RequestType
    /// 0 - RoomList
    /// </summary>
    [Serializable]
    public class PlayerRequest : Data
    {
        // The sender of the mssage.
        public Player Sender { get; set; }

        // The type of request (ie. Room list request = 0).
        public byte RequestType { get; set; }

        /// <summary>
        /// The IP address of the current player.
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Construct a request to the server.
        /// </summary>
        /// <param name="sender">The sender of the request.</param>
        /// <param name="requestType">The type of request.</param>
        public PlayerRequest(Player sender, byte requestType)
        {
            Type        = 9;
            Sender      = sender;
            RequestType = requestType;
        }
    }
}
