using System.Collections.Generic;
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
        void ShowUsers(IEnumerable<string> users);
    }
}
