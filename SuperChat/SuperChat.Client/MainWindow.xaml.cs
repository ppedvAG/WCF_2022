using SuperChat.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;

namespace SuperChat.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IClient
    {
        IServer server = null;

        public MainWindow()
        {
            InitializeComponent();
            LogoutResult(true, "");
            nameTb.Text = $"Fred {Guid.NewGuid().ToString().Substring(0, 4)}";
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            var tcp = new NetTcpBinding();
            var tcpAdr = "net.tcp://localhost:1";

            var chf = new DuplexChannelFactory<IServer>(this, tcp, tcpAdr);
            server = chf.CreateChannel();
            server.Login(nameTb.Text);
        }


        private void Logout(object sender, RoutedEventArgs e)
        {
            server?.Logout();
        }

        public void LoginResult(bool ok, string msg)
        {
            if (ok)
            {
                nameTb.IsEnabled = false;
                loginBtn.IsEnabled = false;
                logoutBtn.IsEnabled = true;
                msgTb.IsEnabled = true;
                sendBtn.IsEnabled = true;
            }
            else
                MessageBox.Show(msg);
        }

        public void LogoutResult(bool ok, string msg)
        {
            if (ok)
            {
                nameTb.IsEnabled = !false;
                loginBtn.IsEnabled = !false;
                logoutBtn.IsEnabled = !true;
                msgTb.IsEnabled = !true;
                sendBtn.IsEnabled = !true;

                usersLb.ItemsSource = null;
                chatLb.Items.Clear();
            }

            if (!string.IsNullOrEmpty(msg))
                MessageBox.Show(msg);
        }

        public void ShowMsg(string msg)
        {
            chatLb.Items.Add(msg);
        }

        public void ShowUsers(IEnumerable<string> users)
        {
            usersLb.ItemsSource = users;
        }

        private void SendText(object sender, RoutedEventArgs e)
        {
            server.SendMsg(msgTb.Text);
            msgTb.Clear();
        }
    }
}
