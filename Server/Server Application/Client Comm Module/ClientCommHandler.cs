using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Structures;

namespace Client_Comm_Module
{
    public class ClientCommHandler
    {
        private ClientDataTransmission sender;
        private ClientDataReceiving receiver;

        public ClientCommHandler()
        {
            sender = new ClientDataTransmission();
            receiver = new ClientDataReceiving();
        }

        public void sendLoginRequest()
        {

        }
    }
}
