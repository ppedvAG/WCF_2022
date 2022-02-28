using SelfHost.Contracts;
using System.ServiceModel;
using System.Windows;

namespace SelfHost.CoreClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var chf = new ChannelFactory<IWetterService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:3"));
            //var chf = new ChannelFactory<IWetterService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:1"));

            //no
            //var chf = new ChannelFactory<IWetterService>(new WSHttpBinding(), new EndpointAddress("http://localhost:4"));
            //var chf = new ChannelFactory<IWetterService>(new NetNamedPipeBinding(), "net.pipe://localhost/Wetter");

            lb.Items.Add(chf.CreateChannel().GetTemperature("Heidelberg"));

        }
    }
}
