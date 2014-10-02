using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server_Application
{
    /// <summary>
    /// This class is responsible for transmitting data to clients.
    /// Whether updating a player's position or the chat log or simply
    /// allowing them to log into the game.
    /// </summary>
    class DataTransmission
    {
        //int port = ((IPEndPoint)socket.Client.LocalEndPoint).Port;
        UdpClient[] UDPClients = new UdpClient[DataControl.NumberOfUdpClients];
        TcpClient[] TCPClients = new TcpClient[DataControl.NumberOfTcpClients];
        List<GameMessage> messages;
        List<GameData> gamedata;
        List<ErrorMessage> errorMessages;
        Server owner;

        public static const int GAME_DATA = 1;

        public DataTransmission(Server owner)
        {
            for (int x = 0; x < DataControl.NumberOfUdpClients; x++)
                UDPClients[x] = new UdpClient(6944 + x);
            for (int x = 0; x < DataControl.NumberOfTcpClients; x++)
                TCPClients[x] = new TcpClient();
            this.owner = owner;
        }

        /// <summary>
        /// Add the given message to the appropriate queue.
        /// </summary>
        /// <param name="message">The message to add to a queue.</param>
        public void addMessageToQueue(Data message)
        {
            if (message == null)
                return;
                
            try
            {
                switch (message.Type)
                {
                    case 1:
                        gamedata.Add((GameData)message);
                        break;
                    case 2:
                        messages.Add((GameMessage)message);
                        break;
                    case 3:
                        errorMessages.Add((ErrorMessage)message);
                        break;
                }
            } catch(InvalidCastException e) { Console.WriteLine(e.ToString()); return; }
        }

        private GameMessage removeMessageFromQueue()
        {
            if (messages.Count == 0)
                return null;
            GameMessage message = messages.ElementAt(0);
            messages.RemoveAt(0);
            return message;
        }

        public void respondToLoginRequest()
        {

        }

        public void sendChatMessages()
        {
            while (true)
            {
                GameMessage message = removeMessageFromQueue();
                if (message == null)
                {
                    Thread.Sleep(5);
                    continue;
                }
                Gameroom room = owner.getGameroom(message.Gameroom);
                foreach (GameData player in room.getPlayerList())
                {
                    if (player.Username != message.Username)
                    {
                        DataControl.sendTCPData(TCPClients[1], message, player.IP, DataControl.TCPMessageClient);
                    }
                }
            }
        }

        public void sendClientData()
        {

        }

        public void sendErrorMessage()
        {

        }
    }
}
