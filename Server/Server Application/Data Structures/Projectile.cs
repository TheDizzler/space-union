using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_Structures
{
    [Serializable]
    public class Projectile : Data
    {
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public string Username { get; set; }
        public float Rotation { get; set; }
        public float TimeActive { get; set; }

        public Projectile()
        {
            Type = 11;
        }
    }
}
