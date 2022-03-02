using ServiceReference1;
using System.ServiceModel;

namespace HalloDataContracts.Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void Button1_Click(object sender, EventArgs e)
        {
            Pizza pizza = new Pizza()
            {
                Geschnitten = checkBox1.Checked,
                Beläge = new List<string>()
            };

            foreach (var item in checkedListBox1.CheckedItems)
            {
                pizza.Beläge.Add(item.ToString());
            }

            var client = new PizzaServiceClient();

            try
            {
                var lieferung = client.BestellPizza(pizza);

                listBox1.Items.Clear();
                listBox1.Items.Add($"Preis: {lieferung.price:c}");
                listBox1.Items.Add($"Geschnitten: {(lieferung.Geschnitten ? "Ja" : "Nein")}");
                listBox1.Items.Add("Beläge:");
                lieferung.Beläge.ForEach(x => listBox1.Items.Add(x));
            }
            catch(FaultException<PizzaFehler> ex)
            {
                MessageBox.Show($"Pizza Bestell Fehler:{ex.Detail.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler {ex.Message}");
            }



        }
    }
}