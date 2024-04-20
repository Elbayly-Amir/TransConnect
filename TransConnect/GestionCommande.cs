using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class GestionCommande
    {

        List<Commande> commandes = new List<Commande>();

        int id = 0;

        public void AjouterCommande(Client client, Salarie chauffeur, Vehicule vehicule, DateTime dateCommande, string villeDepart, string villeArrivee, decimal prix)
        {

            if (!EstChauffeurDisponible(chauffeur, dateCommande))
            {
                Console.WriteLine("Erreur: Le chauffeur n'est pas disponible à cette date.");
                return;
            }

            Commande nouvelleCommande = new Commande(id, client, chauffeur, vehicule, dateCommande, villeDepart, villeArrivee, prix);
            commandes.Add(nouvelleCommande);
        }

        public void SupprimmerCommande(Commande commande)
        {
            commandes.Remove(commande);
        }

        public void ModifierCommande(Commande commande, string depart, string arrivee, decimal nouveauPrix)
        {

            if (!EstChauffeurDisponible(commande.Chauffeur, commande.DateCommande))
            {
                Console.WriteLine("Erreur: Le chauffeur n'est pas disponible à cette date pour les modifications.");
                return;
            }

            commande.ModifierCommande(depart, arrivee, nouveauPrix);
        }

        public Commande CommandeParId(int id)
        {
            return commandes.FirstOrDefault(c => c.IdCommande == id);
        }

        public void AfficherCommandes()
        {
            foreach (var commande in commandes)
            {
                Console.WriteLine(commande.ToString());
            }
        }

        private bool EstChauffeurDisponible(Salarie chauffeur, DateTime date)
        {
            return !commandes.Any(c => c.Chauffeur == chauffeur && c.DateCommande.Date == date.Date);
        }
    }

}
