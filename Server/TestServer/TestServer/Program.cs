﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestServer
{
    class Program
    {
        static string receiverip;
        static void Main(string[] args)
        {
            
        }

        private static byte[] objectToBytes(Object target)
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

        private static Object bytesToObject(byte[] target)
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

        private static byte[] decompress(byte[] data)
        {
            using (GZipStream gzip = new GZipStream(new MemoryStream(data), CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream ms = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = gzip.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            ms.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return ms.ToArray();
                }
            }
        }

        private static byte[] compress(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    gzip.Write(data, 0, data.Length);
                }
                return ms.ToArray();
            }
        }

        private static IPAddress GetLocalIPv4Address()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ipv4 in host.AddressList)
            {
                if (ipv4.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipv4;
                }
            }
            return null;
        }
    }
}