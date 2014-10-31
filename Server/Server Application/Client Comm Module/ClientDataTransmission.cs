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
        /// <summary>
        /// Whether this transmission client is disposed.
        /// </summary>
        private bool Disposed { get; set; }

        /// <summary>
        /// Used to store data to send.
        /// </summary>
        private GameData Data { get; set; }

        /// <summary>
        /// UDP client used to send data.
        /// </summary>
        private UdpClient UDPClient { get; set; }

        /// <summary>
        /// The UDP port to use when sending; assigned by the server.
        /// </summary>
        private int UDPPort { get; set; }

        private Object Locker { get; set; }

        /// <summary>
        /// Initiate the data transmission client.
        /// </summary>
        /// <param name="assignedPort">The port assigned by the server.</param>
        public ClientDataTransmission(int assignedPort)
        {
            UDPPort = assignedPort;
            UDPClient = new UdpClient(UDPPort);
            
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
        public void updateUDPPort(int UDPPort)
        {
            this.UDPPort = UDPPort;
        }

        public void updateData(GameData data)
        {
            lock (Locker)
                Data = data;
        }

        /// <summary>
        /// Sends a game data to the server.
        /// </summary>
        private void sendGameData()
        {
            while (true)
            {
                GameData data;
                lock (Locker)
                    data = Data;                
                if (data == null)
                {
                    Thread.Sleep(ClientConstants.DATA_SEND_INTERVAL);
                    continue;
                }
                DataControl.sendUDPData(UDPClient, data, ClientConstants.SERVER_IPADDRESS, UDPPort);
            }
        }

        /// <summary>
        /// Dispose the transmission client.
        /// For external use.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the transmission client.
        /// </summary>
        /// <param name="disposing">True if the client is to be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                // clean up here
            }

            Disposed = true;
        }
    }
}
