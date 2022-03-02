using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace BierRESTService
{
    [ServiceContract]
    internal interface IBierService
    {
        [OperationContract]
        [WebGet(UriTemplate = "Bier")]
        IEnumerable<Bier> GetAllBier();

        [OperationContract]
        [WebGet(UriTemplate = "Bier?id={id}")]
        Bier GetBierById(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Bier")]
        void AddNewBier(Bier bier);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Bier")]
        void UpdateBier(Bier bier);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Bier")]
        void DeleteBier(Bier bier);
    }

    public class Bier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hersteller { get; set; }
        public double Alk { get; set; }
    }
}
