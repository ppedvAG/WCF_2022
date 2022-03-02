using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace HalloDataContracts
{

    [ServiceContract]
    public interface IPizzaService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        [FaultContract(typeof(PizzaFehler))]
        Pizza BestellPizza(Pizza pizza);
    }

    [DataContract]
    public class PizzaFehler
    {
        [DataMember]
        public Pizza Pizza { get; set; }

        [DataMember]
        public string Message { get; set; }
    }

    [DataContract(Namespace ="http://ppedv.de/wcfkurs/2022/")]
    public class Pizza
    {
        [DataMember(Name="price")]
        public decimal Preis { get; set; }

        [DataMember]
        public List<string> Beläge { get; set; } = new List<string>();

        [DataMember]
        public bool Geschnitten { get; set; }

        [DataMember]
        public bool Geschnitten2222 { get; set; }


        public int Geheim { get; set; }

    }

}
