using System;
using System.ServiceModel;

namespace SuperChat.Contracts
{
    [ServiceContract(CallbackContract = typeof(IClient))]
    public interface IServer
    {
        [OperationContract(IsOneWay = true)]
        void Login(string name);

        [OperationContract(IsOneWay = true)]
        void Logout();

        [OperationContract(IsOneWay = true)]
        void SendMsg(string msg);
    }
}
