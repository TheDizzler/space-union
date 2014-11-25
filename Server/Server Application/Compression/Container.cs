using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compression
{
    [Serializable]
    public class Container
    {
        public byte[] Data { get; set; }
        public Container(byte[] data)
        {
            Data = data;
        }
    }
}
