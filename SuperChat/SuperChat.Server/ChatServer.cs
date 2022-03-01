using SuperChat.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                UpdateUserlistForAllUsers();
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

                SendMsgToAllUsers(niceMsg);
            }
        }

        private void SendMsgToAllUsers(string msg)
        {
            foreach (var item in users.ToList())
            {
                try
                {
                    item.Value.ShowMsg(msg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR SendMsgToAllUsers: {ex.Message}");
                    Logout(item.Key);
                }
            }
        }

        void UpdateUserlistForAllUsers()
        {
            foreach (var item in users.ToList())
            {
                try
                {
                    item.Value.ShowUsers(users.Select(x => x.Key));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR UpdateUserlistForAllUsers: {ex.Message}");
                    Logout(item.Key);
                }
            }

        }

        private void Logout(string name)
        {
            users.Remove(name);
            UpdateUserlistForAllUsers();
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
                SendImageToAllUsers(ms);
            }
        }

        private void SendImageToAllUsers(Stream image)
        {
            foreach (var item in users.ToList())
            {
                try
                {
                    image.Position = 0;
                    item.Value.ShowImage(image);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR SendImageToAllUsers: {ex.Message}");
                    Logout(item.Key);
                }
            }
        }
    }
}
