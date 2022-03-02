using System;
using System.Collections.Generic;
using System.Linq;

namespace BierRESTService
{
    internal class BierService : IBierService
    {
        static List<Bier> bierDb = new List<Bier>();

        static BierService()
        {
            bierDb.Add(new Bier() { Id = 1, Name = "Jever", Hersteller = "Jever", Alk = 5.0 });
            bierDb.Add(new Bier() { Id = 2, Name = "Tegernseer", Hersteller = "Herzog bla bla", Alk = 5.0 });
        }

        public void AddNewBier(Bier bier)
        {
            bierDb.Add(bier);
        }

        public void DeleteBier(Bier bier)
        {
            bierDb.Remove(bierDb.ToList().FirstOrDefault(x => x.Id == bier.Id));
        }

        public IEnumerable<Bier> GetAllBier()
        {
            return bierDb;
        }

        public Bier GetBierById(int id)
        {
            return bierDb.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateBier(Bier bier)
        {
            DeleteBier(bier);
            AddNewBier(bier);
        }
    }
}
