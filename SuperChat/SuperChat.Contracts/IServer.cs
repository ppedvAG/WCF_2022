using System;
using System.IO;
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


        [OperationContract(IsOneWay = true)]
        void SendImage(Stream image);
    }
}
