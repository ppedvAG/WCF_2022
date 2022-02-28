using SelfHost.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace SelfHost.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** WCF Server ***");

            var tcpBind = new NetTcpBinding();
            var tcpAdr = "net.tcp://localhost:1";

            var basicHttp = new BasicHttpBinding();
            var bHttpAdr = "http://localhost:3";

            var wsHttp = new WSHttpBinding();
            var wsHttpAdr = "http://localhost:4";

            var netPipe = new NetNamedPipeBinding();
            var netPipeAdr = "net.pipe://localhost/Wetter";

            //var msMq = new NetMsmqBinding();
            //var msMqAdr = "net.msmq://localhost/Wetter";

            var host = new ServiceHost(typeof(WetterService));
            host.AddServiceEndpoint(typeof(IWetterService), tcpBind, tcpAdr);
            host.AddServiceEndpoint(typeof(IWetterService), basicHttp, bHttpAdr);
            host.AddServiceEndpoint(typeof(IWetterService), wsHttp, wsHttpAdr);
            host.AddServiceEndpoint(typeof(IWetterService), netPipe, netPipeAdr);
            //host.AddServiceEndpoint(typeof(IWetterService), msMq, msMqAdr);

            var smb = new ServiceMetadataBehavior()
            {
                HttpGetUrl = new Uri("http://localhost:2"),
                HttpGetEnabled = true
            };
            host.Description.Behaviors.Add(smb);

            host.Open();
            Console.WriteLine("Service wurde gestartet");



            Console.ReadLine();
            Console.WriteLine("Ende");
        }
    }
}
