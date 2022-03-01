using System;
using System.Linq;
using System.ServiceModel;

namespace HalloDataContracts
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class PizzaService : IPizzaService
    {
        int counter = 0;

        public Pizza BestellPizza(Pizza pizza)
        {
            counter++;

            if (counter > 3)
                throw new FaultException("Mehr als 3 Bestellungen sind nicht möglich");

            if (pizza.Beläge == null || pizza.Beläge.Count == 0)
                throw new FaultException<PizzaFehler>(new PizzaFehler()
                {
                    Pizza = pizza,
                    Message = "Keine Beläge, keine Pizza!"
                }, "Keine Beläge");


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
