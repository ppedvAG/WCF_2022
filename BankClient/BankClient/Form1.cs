using Bank;
using System.ServiceModel;

namespace BankClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private  void button1_Click(object sender, EventArgs e)
        {
            var url = "http://www.thomas-bayer.com/axis2/services/BLZService";
            var client = new BLZServicePortTypeClient(new BasicHttpBinding(), new EndpointAddress(url));

            var result = client.getBank(new getBankRequest(textBox1.Text));

            label1.Text = $"{result.details.bezeichnung}\n{result.details.plz} {result.details.ort}\n{result.details.bic}";
        }
    }
}