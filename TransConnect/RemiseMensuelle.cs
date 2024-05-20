using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class RemiseMensuelle : IRemise
    {
        private const decimal REMISE = 0.1m;

        public decimal CalculerRemise(List<Commande> commandes)
        {
            if (EstEligible(commandes))
            {
                decimal total = commandes.Sum(c => c.Prix);
                return total * REMISE;
            }
            return 0;
        }

        public bool EstEligible(List<Commande> commandes)
        {
            var commandesDuMois = commandes.Where(c => c.DateCommande >= DateTime.Now.AddMonths(-1)).ToList();
            return commandesDuMois.Count >= 3;
        }
    }

}
