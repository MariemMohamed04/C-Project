using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Client
    {
        byte[] BtArrIp;
        IPAddress LocalAddress;
        TcpClient tcpClient;
        NetworkStream NStream;
        StreamWriter Writer;
        StreamReader Reader;
        string clientID;
        public string ClientID { get { return clientID; } }

        public event Action<Client, string> MessageReceived;

        public Client() //client_only
        {
            this.tcpClient = new TcpClient();
            BtArrIp = new byte[] { 127, 0, 0, 1 };
            LocalAddress = new IPAddress(BtArrIp);
        }

        public Client(TcpClient client,string ID) // server_only
        {
            this.tcpClient = client;
            NStream = tcpClient.GetStream();

            Writer = new StreamWriter(NStream);
            Writer.AutoFlush = true;

            clientID = ID;

            Reader = new StreamReader(NStream);
            Task.Run(() => { ReceiveMsg(Reader); });
        }

        public void SendMsg(string Msg) //both
        {
            Writer.Write(Msg);
        }

        async void ReceiveMsg(StreamReader Reader) //both
        {
            while (true)
            {
                char[] buffer = new char[100];
                int bytesRead = await Reader.ReadAsync(buffer, 0, buffer.Length);
                string message = new string(buffer, 0, bytesRead);
                if (MessageReceived != null)
                    MessageReceived(this, message);
            }
        }

        public void ConnectToServer() //client_only
        {
            this.tcpClient.Connect(LocalAddress, 9000);
            NStream = this.tcpClient.GetStream();

            Reader = new StreamReader(NStream);
            ReceiveUniqueID(Reader);
            Task.Run(() => { ReceiveMsg(Reader); });

            Writer = new StreamWriter(NStream);
            Writer.AutoFlush = true;
        }

        public void CloseConnections() //both
        {
            Reader.Close();
            Writer.Close();
            NStream.Close();
            tcpClient.Close();
        }

        async private void ReceiveUniqueID(StreamReader Reader) //client
        {
            char[] buffer = new char[32];
            int bytesRead = await Reader.ReadAsync(buffer, 0, buffer.Length);
            clientID = new string(buffer, 0, bytesRead);
        }
    }
}