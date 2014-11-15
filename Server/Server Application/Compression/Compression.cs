using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Threading;

namespace Compression
{
    public class Compression
    {
        private static int MainBlockSize;
        private static int SecondBlockSize;
        private static int Threads = 20;
        /*public static Container Compress(byte[] data)
        {
            Thread[] threads = new Thread[Threads];
            byte[][] divided = SplitData(data);
            for (int x = 0; x < Threads; x++)
            {
                int y = x;
                threads[y] = new Thread(() => CompressData(ref divided[y]));
                threads[y].Start();
            }
            for (int x = 0; x < Threads; x++)
                threads[x].Join();
            return new Container(divided);
        }*/

        public static byte[] Compress(byte[] raw)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
                {
                    gzip.Write(raw, 0, raw.Length);
                }
                return memory.ToArray();
            }
        }

        public static byte[] Decompress(byte[] gzip)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                const int size = 32768;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }
    }
}
