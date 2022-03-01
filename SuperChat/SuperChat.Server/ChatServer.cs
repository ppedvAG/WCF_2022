using SuperChat.Contracts;
using System;
using System.ServiceModel;

namespace SuperChat.Server
{
    internal class ChatServer : IServer
    {
        public void Login(string name)
        {
            Console.WriteLine($"Login: {name}");
            IClient client = OperationContext.Current.GetCallbackChannel<IClient>();
            client.LoginResult(true, "");
            client.ShowMsg($"Hallo {name}");
        }

        public void Logout()
        {
            Console.WriteLine($"Logout: ??");
        }

        public void SendMsg(string msg)
        {
            Console.WriteLine($"SendMsg: [???] {msg}");
        }
    }
}
