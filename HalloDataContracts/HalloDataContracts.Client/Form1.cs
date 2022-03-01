using PizzaService;

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
            var lieferung = client.BestellPizza(new BestellPizzaRequest(pizza)).BestellPizzaResult;
            

            listBox1.Items.Clear();
            listBox1.Items.Add($"Preis: {lieferung.Preis:c}");
            listBox1.Items.Add($"Geschnitten: {(lieferung.Geschnitten ? "Ja" : "Nein")}");
            listBox1.Items.Add("Beläge:");
            lieferung.Beläge.ForEach(x => listBox1.Items.Add(x));
        }
    }
}