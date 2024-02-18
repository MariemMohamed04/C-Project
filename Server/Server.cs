using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Server
    {
        byte[] BtArrIp;
        IPAddress LocalAddress;
        TcpListener Listener;
        Dictionary<string, Client> Clients;
        Dictionary<string, Room> Rooms;

        public Server()
        {
            BtArrIp = new byte[] { 127, 0, 0, 1 };
            LocalAddress = new IPAddress(BtArrIp);
            Listener = new TcpListener(LocalAddress, 9000);
            Clients = new Dictionary<string, Client>();
            Rooms = new Dictionary<string, Room>();

            Listener.Start();
            Task.Run(() => { AcceptConnections(); });
        }

        private void AcceptConnections()
        {
            while (true) //use flag here
            {
                string ID = GenerateID();
                Client client = new Client(Listener.AcceptTcpClient(), ID);
                SendMsg(client, client.ClientID);
                client.MessageReceived += Program.ClientMsgReceived;
                Clients.Add(client.ClientID, client);
            }
        }
        public void SendMsg(Client client, string Msg) //////////
        {
            //string Message = String.Concat(client.ClientID, ":",Msg);
            //client.SendMsg(Message);
        }
        private string GenerateID() //server_only
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
