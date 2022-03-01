using SuperChat.Contracts;
using System;
using System.ServiceModel;

namespace SuperChat.Server
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("*** Super Chat Server v0.1 ***");

            var tcp = new NetTcpBinding();
            var tcpAdr = "net.tcp://localhost:1";

            var host = new ServiceHost(typeof(ChatServer));
            host.AddServiceEndpoint(typeof(IServer), tcp, tcpAdr);

            host.Open();
            Console.WriteLine("Server wurde gestartet");

            Console.ReadLine();
            host.Close();
            Console.WriteLine("Server wurde beendet");


            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
