using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace BierRESTService
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var tcp = new NetTcpBinding();

            var host = new ServiceHost(typeof(BierService));
            host.AddServiceEndpoint(typeof(IBierService), tcp, "net.tcp://localhost:1");

            var web = new WebHttpBinding();
            var ep = host.AddServiceEndpoint(typeof(IBierService), web, "http://localhost:2");
            ep.EndpointBehaviors.Add(new WebHttpBehavior() { AutomaticFormatSelectionEnabled = true });


            host.Open();
            Console.WriteLine("Bier service säuft");
            Console.Read();

            Console.WriteLine("Ende");
            Console.Read();

        }
    }
}
