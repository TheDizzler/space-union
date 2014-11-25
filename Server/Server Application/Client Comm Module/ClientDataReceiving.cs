using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data_Structures;
using Data_Manipulation;

namespace Client_Comm_Module
{
    class ClientDataReceiving : IDisposable
    {
        /// <summary>
        /// True if this class has been disposed.
        /// </summary>
        private bool Disposed { get; set; }
        /// <summary>
        ///  UDP client used to listen to the server.
        /// </summary>
        private UdpClient UDPListener { get; set; }
        /// <summary>
        /// List used to store received data.
        /// </summary>
        public GameFrame Data { get; private set; }
        /// <summary>
        /// UDP port to listen to; assigned by the server.
        /// </summary>
        private int assignedUDPPort_Listen;

        private Thread dataReceiverThread;

        public bool gameRunning { get; set; }

        /// <summary>
        /// Initiate the data receiver client.
        /// Initialize the data receiver client.
        /// </summary>
        /// <param name="assignedPort">UDP port to listen to; assigned by the server.</param>
        public ClientDataReceiving(int assignedPort)
        {
            UDPListener = new UdpClient(assignedPort);
            assignedUDPPort_Listen = assignedPort;
            dataReceiverThread = new Thread(receiveData);
            dataReceiverThread.Start();
        }

        /// <summary>
        /// Begin receiving game data from the server.
        /// </summary>
        public void receiveData()
        {
            while (true)
            {
                if (!gameRunning)
                    continue;
                Data = (GameFrame)DataControl.receiveUDPData(UDPListener);
                if(Data.Data != null)
                    foreach (GameData data in Data.Data)
                        if(data.Bullets != null)
                            Console.WriteLine(data.Bullets.Length);
            }
        }

        /// <summary>
        /// Dispose the receiver client.
        /// External use.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the receiver client.
        /// </summary>
        /// <param name="disposing">True if the client is to be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                // clean up here
                if (dataReceiverThread != null)
                {
                    dataReceiverThread.Abort();
                }
            }

            Disposed = true;
        }
    }
}
