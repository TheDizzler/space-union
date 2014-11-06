using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_Structures
{
    /// <summary>
    /// Contains player request information.
    /// An appropriate constructor must be called to construct specific messages.
    /// 
    /// RequestType
    /// 0 - Room List
    /// 1 - Room Creation
    /// 2 - Room Join
    /// </summary>
    [Serializable]
    public class PlayerRequest : Data
    {
        /// <summary>
        /// The sender of the mssage. 
        /// </summary>
        public Player Sender { get; set; }

        /// <summary>
        /// The type of request (ie. Room list request = 0). 
        /// </summary>
        public byte RequestType { get; set; }

        public int RoomNumber { get; set; }

        public string RoomName { get; set; }

        /// <summary>
        /// Construct a room list request.
        /// </summary>
        /// <param name="sender">The sender of the request.</param>
        public PlayerRequest(Player sender)
        {
            Type        = 9;
            Sender      = sender;
            RequestType = 0;
        }

        /// <summary>
        /// Construct a room creation request.
        /// </summary>
        /// <param name="sender">The sender of the request.</param>
        /// <param name="roomName">The name of the room.</param>
        public PlayerRequest(Player sender, string roomName)
        {
            Type        = 9;
            Sender      = sender;
            RequestType = 1;
            RoomName    = roomName;
        }

        /// <summary>
        /// Construct a room join request.
        /// </summary>
        /// <param name="sender">The sender of the request.</param>
        /// <param name="requestType">The number of the room to join.</param>
        public PlayerRequest(Player sender, int roomNumber, byte requestType)
        {
            Type        = 9;
            Sender      = sender;
            RequestType = requestType;
            RoomNumber  = roomNumber;
        }
    }
}
