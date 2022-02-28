using System.ServiceModel;

namespace SelfHost.Contracts
{
    [ServiceContract]
    public interface IWetterService
    {
        [OperationContract]
        double GetTemperature(string location);
    }
}
