using System.Collections.Generic;
using System.ServiceModel;

namespace HalloDataContracts
{

    [ServiceContract]
    public interface IPizzaService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        Pizza BestellPizza(Pizza pizza);
    }

    public class Pizza
    {
        public decimal Preis { get; set; }

        public List<string> Beläge { get; set; } = new List<string>();

        public bool Geschnitten { get; set; }

        public int MyProperty { get; set; }
    }

}
