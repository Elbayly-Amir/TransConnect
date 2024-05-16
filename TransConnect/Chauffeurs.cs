using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Chauffeur : Salarie
    {
        public bool EstDisponible { get; set; } = true;
        public List<Commande> Commandes { get; set; } = new List<Commande>();
        public Chauffeur(string numeroSS, string nom, string prenom, DateTime dateNaissance, string adresse, string email, string telephone, DateTime dateEntree, string poste, decimal salaire)
            : base(numeroSS, nom, prenom, dateNaissance, adresse, email, telephone, dateEntree, poste, salaire)
        {
        }

       
    }
}
