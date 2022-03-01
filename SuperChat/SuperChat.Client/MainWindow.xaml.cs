using SuperChat.Contracts;
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
        public MainWindow()
        {
            InitializeComponent();
            LogoutResult(true, "");
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            var tcp = new NetTcpBinding();
            var tcpAdr = "net.tcp://localhost:1";

            var chf = new DuplexChannelFactory<IServer>(this, tcp, tcpAdr);
            server = chf.CreateChannel();
            server.Login(nameTb.Text);
        }

        IServer server = null;

        private void Logout(object sender, RoutedEventArgs e)
        {

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
            }
            else
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


    }
}
