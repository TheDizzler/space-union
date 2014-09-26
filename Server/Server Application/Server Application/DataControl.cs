using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application
{
    /// <summary>
    /// This class primarily houses methods used in the
    /// transmission and reception of data from different
    /// clients.
    /// </summary>
    class DataControl
    {
        /// <summary>
        /// Sends data to the specified IP address and port.
        /// </summary>
        /// <param name="data">The data to transmit.</param>
        /// <param name="ipaddress">The IP address of the target client.</param>
        /// <param name="port">The port number for the data to be received at.</param>
        public static void sendData(object data, string ipaddress, int port)
        {
            UdpClient client = new UdpClient();
            byte[] output = objectToBytes(data);
            client.Send(output, output.Length, ipaddress, port);
        }

        /// <summary>
        /// Converts an object to an array of bytes.
        /// </summary>
        /// <param name="target">Object to convert.</param>
        /// <returns>An array of bytes of the input.</returns>
        public static byte[] objectToBytes(Object target)
        {
            if (target == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, target);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Converts an array of bytse to an object.
        /// </summary>
        /// <param name="target">The array of bytes to convert.</param>
        /// <returns>An object converted from the input.</returns>
        public static Object bytesToObject(byte[] target)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(target, 0, target.Length);
                ms.Seek(0, SeekOrigin.Begin);
                Object data = (Object)bf.Deserialize(ms);
                return data;
            }
        }
    }
}
