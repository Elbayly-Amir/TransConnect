using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public interface IRemise
    {
        decimal CalculerRemise(List<Commande> commandes);
        bool EstEligible(List<Commande> commandes);
    }
}

