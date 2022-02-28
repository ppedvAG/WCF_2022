using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloWCF.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WCF Client");

            var client = new ServiceReference1.Service1Client();

            var result = client.GetData(8342756);

            Console.WriteLine(result);

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
