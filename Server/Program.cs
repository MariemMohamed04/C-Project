namespace Server
{
    //public delegate EventManager()
    internal class Program
    {
        public static void ClientMsgReceived(Client client, string Message)
        {
            Console.WriteLine(Message);
        }

        static void Main(string[] args)
        {
            Server server = new Server();

            while (true)
            {
                string msg = Console.ReadLine();
                //server.SendMessage(msg);
            }
        }
    }
}
