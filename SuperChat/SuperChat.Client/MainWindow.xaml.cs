using Microsoft.Win32;
using SuperChat.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
            tcp.MaxReceivedMessageSize = int.MaxValue;
            tcp.Security.Mode = SecurityMode.Transport;
            tcp.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            var tcpAdr = "net.tcp://DESKTOP-VCPG150:1"; //WICHTIG Hier PC Name und nicht nur IP !! 

            EndpointIdentity identity = EndpointIdentity.CreateDnsIdentity("SuperChatCA");
            EndpointAddress address = new EndpointAddress(new Uri(tcpAdr), identity);

            var http = new WSDualHttpBinding();
            http.Security.Mode = WSDualHttpSecurityMode.Message;
            http.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
            http.MaxReceivedMessageSize = int.MaxValue;
            var httpAdr = "http://192.168.178.122:2/chat";
            EndpointAddress httpAddress = new EndpointAddress(new Uri(httpAdr), identity);

            var netHttp = new NetHttpsBinding();
            //netHttp.Security.Mode = BasicHttpsSecurityMode.Transport;
            netHttp.MaxReceivedMessageSize = int.MaxValue;
            var netHttpAdr = "https://localhost:3/chat";


            var chf = new DuplexChannelFactory<IServer>(this, tcp, address);
            //var chf = new DuplexChannelFactory<IServer>(this, http, httpAddress);
            chf.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
            chf.Credentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.Root, X509FindType.FindByThumbprint, "2758d963dfbdcf0ed3cf638f4f8f3db6f6da7acb");
            //var chf = new DuplexChannelFactory<IServer>(this, netHttp, netHttpAdr);

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

        public void ShowImage(Stream image)
        {
            var ms = new MemoryStream();
            image.CopyTo(ms);
            ms.Position = 0;
            var img = new Image();
            img.BeginInit();
            img.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            img.Stretch = System.Windows.Media.Stretch.None;
            img.EndInit();
            chatLb.Items.Add(img);
        }

        private void SendImage(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog() { Title = "Wähle ein Bild", Filter = "Bild|*.png;*.jpg;*.gif|Alle Dateien|*.*" };

            if (dlg.ShowDialog().Value)
            {
                using (var stream = File.OpenRead(dlg.FileName))
                {
                    server.SendImage(stream);
                }
            }
        }
    }
}
