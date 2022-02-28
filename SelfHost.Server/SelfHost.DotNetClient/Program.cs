using SelfHost.Contracts;
using System;
using System.ServiceModel;
using System.Threading;

namespace SelfHost.DotNetClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** WCF .NET Framework Client ***");

            //var chf = new ChannelFactory<IWetterService>(new NetTcpBinding(), "net.tcp://localhost:1");
            //var chf = new ChannelFactory<IWetterService>(new BasicHttpBinding(), "http://localhost:3");
            //var chf = new ChannelFactory<IWetterService>(new WSHttpBinding(), "http://localhost:4");
            var chf = new ChannelFactory<IWetterService>(new NetNamedPipeBinding(), "net.pipe://localhost/Wetter");

            IWetterService client = chf.CreateChannel();

            Thread.Sleep(500);
            Console.WriteLine($"Temperatur: {client.GetTemperature("Heidelberg")}°C");

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
