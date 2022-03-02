using SuperChat.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;

namespace SuperChat.Server
{
    internal class ChatServer : IServer
    {
        static Dictionary<string, IClient> users = new Dictionary<string, IClient>();

        public void Login(string name)
        {
            Console.WriteLine($"Login: {name}");
            IClient client = OperationContext.Current.GetCallbackChannel<IClient>();
            if (users.ContainsKey(name))
                client.LoginResult(false, $"{name} ist bereits angemeldet");
            else
            {
                users.Add(name, client);

                client.LoginResult(true, "");
                client.ShowMsg($"Hallo {name}");
                SendToAllClients(x => x.ShowUsers(users.Select(y => y.Key)));
            }
        }

        public void SendMsg(string msg)
        {
            IClient client = OperationContext.Current.GetCallbackChannel<IClient>();
            var sender = users.FirstOrDefault(x => x.Value == client);

            if (sender.Key != null)
            {
                var niceMsg = $"[{DateTime.Now:T}] {sender.Key}: {msg}";
                Console.WriteLine($"SendMsg: {niceMsg}");

                SendToAllClients(x => x.ShowMsg(niceMsg));
            }
        }

        public void SendToAllClients(Action<IClient> clientAction, [CallerMemberName] string cmn = "")
        {
            foreach (var item in users.ToList())
            {
                try
                {
                    clientAction.Invoke(item.Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR ({cmn}): {ex.Message}");
                    Logout(item.Key);
                }
            }
        }


        private void Logout(string name)
        {
            users.Remove(name);

            SendToAllClients(x => x.ShowUsers(users.Select(y => y.Key)));
        }

        public void Logout()
        {
            IClient client = OperationContext.Current.GetCallbackChannel<IClient>();
            var sender = users.FirstOrDefault(x => x.Value == client);

            if (sender.Key != null)
            {
                Console.WriteLine($"Logout: {sender.Key}");
                Logout(sender.Key);
                client.LogoutResult(true, "");
            }
            else
                client.LogoutResult(true, "War gar nicht in der Liste");

        }

        public void SendImage(Stream image)
        {
            IClient client = OperationContext.Current.GetCallbackChannel<IClient>();
            var sender = users.FirstOrDefault(x => x.Value == client);

            if (sender.Key != null)
            {
                var ms = new MemoryStream();
                image.CopyTo(ms);

                Console.WriteLine($"SendImage: ...🖼");
                SendToAllClients(x =>
                {
                    ms.Position = 0;
                    x.ShowImage(ms);
                });
            }
        }
    }
}
