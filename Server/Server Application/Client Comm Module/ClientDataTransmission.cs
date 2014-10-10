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
    class ClientDataTransmission : IDisposable
    {
        private bool disposed = false;

        private List<GameData> dataQueue;

        private UdpClient UDPClient;

        private int assignedUDPPort_Send;

        public ClientDataTransmission(int assignedPort)
        {
            UDPClient = new UdpClient(assignedUDPPort_Send);
            assignedUDPPort_Send = assignedPort;
            dataQueue = new List<GameData>();

            try
            {
                new Thread(sendGameData).Start();
            }
            catch (ThreadStateException e) { Console.WriteLine("Client has crashed." + e.ToString()); return; }
            catch (OutOfMemoryException e) { Console.WriteLine("Client has crashed." + e.ToString()); return; }
            catch (InvalidOperationException e) { Console.WriteLine("Client has crashed." + e.ToString()); return; }
        }

        /// <summary>
        /// Assign a UDP port to send the data to.
        /// </summary>
        /// <param name="UDPPort">The UDP port to assign.</param>
        public void assignUDPPort_Send(int UDPPort)
        {
            assignedUDPPort_Send = UDPPort;
        }

        /// <summary>
        /// Add the given message to the appropriate queue.
        /// </summary>
        /// <param name="message">The message to add to a queue.</param>
        public void addDataToQueue(Data message)
        {
            if (message == null)
                return;
            try
            {
                dataQueue.Add((GameData)message);
            }
            catch (InvalidCastException e) { Console.WriteLine(e.ToString()); return; }
        }

        /// <summary>
        /// Sends a game data to the server.
        /// </summary>
        private void sendGameData()
        {
            while (true)
            {
                GameData data = removeDataFromQueue();
                if (data == null)
                {
                    Thread.Sleep(ClientConstants.DATA_SEND_INTERVAL);
                    continue;
                }
                DataControl.sendUDPData(UDPClient, data, ClientConstants.SERVER_IPADDRESS, assignedUDPPort_Send);
            }
        }

        /// <summary>
        /// Returns the oldest game data in the queue.
        /// </summary>
        /// <returns>The oldest message awaiting transfer.</returns>
        private GameData removeDataFromQueue()
        {
            if (dataQueue.Count == 0)
                return null;
            GameData data = dataQueue.ElementAt(0);
            dataQueue.RemoveAt(0);
            return data;
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
