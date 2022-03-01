using System.Linq;

namespace HalloDataContracts
{
    public class PizzaService : IPizzaService
    {
        public Pizza BestellPizza(Pizza pizza)
        {

            foreach (var item in pizza.Beläge
                                .Where(x => x.ToLower().Contains("ananas"))
                                .ToList())
            {
                pizza.Beläge.Remove(item);
            }
            pizza.Preis = 8m + 1.5m * pizza.Beläge.Count;

            return pizza;
        }

        public string GetData(int value)
        {
            return $"You entered: {value}";
        }


    }
}
