using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
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
        public static void sendUDPData(UdpClient client, object data, string ipaddress, int port)
        {
            byte[] output = objectToBytes(data);
            try
            {
                client.Send(output, output.Length, ipaddress, port);
            }
            catch (ArgumentException e) { Console.WriteLine(e.ToString()); return; }
            catch (ObjectDisposedException e) { Console.WriteLine(e.ToString()); return; }
            catch (InvalidOperationException e) { Console.WriteLine(e.ToString()); return; }
            catch (SocketException e) { Console.WriteLine(e.ToString()); return; }
        }

        /// <summary>
        /// Receives data from a specified port.
        /// </summary>
        /// <param name="port">The port from which to listen from.</param>
        /// <returns>Returns the data received from the specified port.</returns>
        public static object receiveUDPData(UdpClient client)
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                byte[] data = client.Receive(ref ip);
                return bytesToObject(data);
            }
            catch (ObjectDisposedException e) { Console.WriteLine(e.ToString()); return null; }
            catch (SocketException e) { Console.WriteLine(e.ToString()); return null; }
        }

        /// <summary>
        /// Sends data to the specified IP address and port.
        /// </summary>
        /// <param name="client">The TCP client through which to send data.</param>
        /// <param name="input">The object to send through the TCP client.</param>
        /// <param name="ipaddress">The IP address to which to send data.</param>
        /// <param name="port">The port to which to send data.</param>
        public static void sendTCPData(TcpClient client, object input, string ipaddress, int port)
        {
            try
            {
                client.Connect(ipaddress, port);
            }
            catch (ArgumentNullException e) { Console.WriteLine(e.ToString()); return; }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine(e.ToString()); return; }
            catch (SocketException e) { Console.WriteLine(e.ToString()); return; }
            catch (ObjectDisposedException e) { Console.WriteLine(e.ToString()); return; }
            Stream stream = null;
            try
            {
                stream = client.GetStream();
            }
            catch (ObjectDisposedException e) { Console.WriteLine(e.ToString()); return; }
            catch (InvalidOperationException e) { Console.WriteLine(e.ToString()); return; }
            byte[] data = objectToBytes(input);
            try
            {
                stream.Write(data, 0, data.Length);
            }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine(e.ToString()); return; }
            catch (ArgumentNullException e) { Console.WriteLine(e.ToString()); return; }
            catch (ArgumentException e) { Console.WriteLine(e.ToString()); return; }
            catch (IOException e) { Console.WriteLine(e.ToString()); return; }
            catch (NotSupportedException e) { Console.WriteLine(e.ToString()); return; }
            catch (ObjectDisposedException e) { Console.WriteLine(e.ToString()); return; }
        }

        /// <summary>
        /// Receive data through a specified TCP listener.
        /// </summary>
        /// <param name="listener">The TCP listener through which to receive data.</param>
        public static object receiveTCPData(TcpListener listener)
        {
            //should be called after the listener.start() is called
            Socket socket = listener.AcceptSocket();
            byte[] received = new byte[1024];
            int size = socket.Receive(received);
            byte[] input = new byte[size];
            for (int x = 0; x < size; x++)
                input[x] = received[x];
            object output = bytesToObject(input);
            socket.Close();
            return output;
        }

        /// <summary>
        /// Converts an object to an array of bytes.
        /// </summary>
        /// <param name="target">Object to convert.</param>
        /// <returns>An array of bytes of the input.</returns>
        public static byte[] objectToBytes(Object target)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    bf.Serialize(ms, target);
                    return ms.ToArray();
                }
                catch (ArgumentNullException e) { Console.WriteLine(e.ToString()); return null; }
                catch (SerializationException e) { Console.WriteLine(e.ToString()); return null; }
                catch (SecurityException e) { Console.WriteLine(e.ToString()); return null; }
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
                try
                {
                    ms.Write(target, 0, target.Length);
                }
                catch (ArgumentNullException e) { Console.WriteLine(e.ToString()); return null; }
                catch (NotSupportedException e) { Console.WriteLine(e.ToString()); return null; }
                catch (ArgumentOutOfRangeException e) { Console.WriteLine(e.ToString()); return null; }
                catch (ArgumentException e) { Console.WriteLine(e.ToString()); return null; }
                catch (IOException e) { Console.WriteLine(e.ToString()); return null; }
                catch (ObjectDisposedException e) { Console.WriteLine(e.ToString()); return null; }

                try
                {
                    ms.Seek(0, SeekOrigin.Begin);
                }
                catch (IOException e) { Console.WriteLine(e.ToString()); return null; }
                catch (ArgumentOutOfRangeException e) { Console.WriteLine(e.ToString()); return null; }
                catch (ArgumentException e) { Console.WriteLine(e.ToString()); return null; }
                catch (ObjectDisposedException e) { Console.WriteLine(e.ToString()); return null; }

                object data = null;
                try
                {
                    data = (Object)bf.Deserialize(ms);
                }
                catch (ArgumentNullException e) { Console.WriteLine(e.ToString()); return null; }
                catch (SerializationException e) { Console.WriteLine(e.ToString()); return null; }
                catch (SecurityException e) { Console.WriteLine(e.ToString()); return null; }

                return data;
            }
        }
    }
}
