using SuperChat.Contracts;
using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace SuperChat.Server
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("*** Super Chat Server v0.1 ***");

            var tcp = new NetTcpBinding();
            tcp.Security.Mode = SecurityMode.Transport;
            tcp.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            tcp.MaxReceivedMessageSize = int.MaxValue;
            var tcpAdr = "net.tcp://localhost:1";

            var http = new WSDualHttpBinding();
            http.Security.Mode = WSDualHttpSecurityMode.Message;
            http.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
            http.MaxReceivedMessageSize = int.MaxValue;
            var httpAdr = "http://localhost:2/chat";

            var netHttp = new NetHttpsBinding();
            netHttp.Security.Mode = BasicHttpsSecurityMode.Transport;
            netHttp.MaxReceivedMessageSize = int.MaxValue;
            var netHttpAdr = "https://localhost:3/chat";

            var host = new ServiceHost(typeof(ChatServer));
            host.AddServiceEndpoint(typeof(IServer), tcp, tcpAdr);
            host.AddServiceEndpoint(typeof(IServer), http, httpAdr);
            //host.AddServiceEndpoint(typeof(IServer), netHttp, netHttpAdr);


            host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
            //host.Credentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.Root, X509FindType.FindByThumbprint, "2758d963dfbdcf0ed3cf638f4f8f3db6f6da7acb");
            host.Credentials.ServiceCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.Root, X509FindType.FindByThumbprint, "2758d963dfbdcf0ed3cf638f4f8f3db6f6da7acb");

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
