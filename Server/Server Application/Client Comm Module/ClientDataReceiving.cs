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
        private bool disposed = false;

        /// <summary>
        ///  UDP client used to listen to the server.
        /// </summary>
        private UdpClient UDPListener;

        /// <summary>
        /// List used to store received data.
        /// </summary>
        private List<GameData> dataQueue;

        /// <summary>
        /// UDP port to listen to; assigned by the server.
        /// </summary>
        private int assignedUDPPort_Listen;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignedPort">UDP port to listen to; assigned by the server.</param>
        public ClientDataReceiving(int assignedPort)
        {
            Console.WriteLine("------------RECEIVER IS INITIALIZED--------" + assignedPort);
            dataQueue = new List<GameData>();
            UDPListener = new UdpClient(assignedPort);
            assignedUDPPort_Listen = assignedPort;
            new Thread(receiveData).Start();
        }


        public Player receiveLoginConfirmation()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, Constants.TCPLoginClient);
            listener.Start();
            return (Player)DataControl.receiveTCPData(listener);
        }

        /// <summary>
        /// Begin receiving game data from the server.
        /// </summary>
        public void receiveData()
        {
            while (true)
            {
                Console.WriteLine("Waiting for data from the server");
                GameData clientData = (GameData)DataControl.receiveUDPData(UDPListener);
                //Console.WriteLine("null Message Received: ");
                if (clientData != null)
                {
                    Console.WriteLine("Message Received: " + clientData.XPosition + ", " + clientData.YPosition + ", " + clientData.Player.Username);
                    Console.WriteLine(getGameDataQueueSize());
                    dataQueue.Add(clientData);
                }
            }
        }

        /// <summary>
        /// Gets the oldest data from the data queue.
        /// </summary>
        /// <returns>The oldest data in the queue.</returns>
        public GameData getGameData()
        {
            if (dataQueue.Count == 0)
                return null;
            GameData data = dataQueue.ElementAt(0);
            dataQueue.RemoveAt(0);
            return data;
        }

        /// <summary>
        /// Gets the size of the GameData queue.
        /// </summary>
        /// <returns>Size of the GameData queue.</returns>
        public int getGameDataQueueSize()
        {
            return dataQueue.Count;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // clean up here
            }

            disposed = true;
        }
    }
}
