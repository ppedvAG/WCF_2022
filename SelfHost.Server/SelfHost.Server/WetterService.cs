using SelfHost.Contracts;
using System;

namespace SelfHost.Server
{
    class WetterService : IWetterService
    {
        static Random ran = new Random(7);   
        public double GetTemperature(string location)
        {
            return ran.NextDouble();
        }
    }
}
