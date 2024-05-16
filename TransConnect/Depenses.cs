using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Depenses
    {

        public string Id { get; private set; }
        public string NumeroSSSalarie { get; private set; }
        public string Description { get; private set; }
        public decimal Montant { get; private set; }
        public DateTime DateDepense { get; private set; }

        public Depenses(string id, string numeroSSSalarie, string description, decimal montant, DateTime dateDepense)
        {
            Id = id;
            NumeroSSSalarie = numeroSSSalarie;
            Description = description;
            Montant = montant;
            DateDepense = dateDepense;
        }
    }
}
