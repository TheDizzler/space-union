using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures
{
    [Serializable]
    public class RegistrationData : Data
    {
        /// <summary>
        /// The username of the current player.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password of the current player.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The IP address of the current player.
        /// </summary>
        public string IPAddress { get; set; }

        public RegistrationData()
        {
            Type = 5;
        }

        public RegistrationData(byte type) : base(type) {}
    }
}
