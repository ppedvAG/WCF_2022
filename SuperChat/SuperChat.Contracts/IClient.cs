using System.Collections.Generic;
using System.IO;
using System.ServiceModel;

namespace SuperChat.Contracts
{
    [ServiceContract]
    public interface IClient
    {
        [OperationContract(IsOneWay = true)]
        void LoginResult(bool ok, string msg);

        [OperationContract(IsOneWay = true)]
        void LogoutResult(bool ok, string msg);

        [OperationContract(IsOneWay = true)]
        void ShowMsg(string msg);

        [OperationContract(IsOneWay = true)]
        void ShowImage(Stream image);

        [OperationContract(IsOneWay = true)]
        void ShowUsers(IEnumerable<string> users);
    }
}
