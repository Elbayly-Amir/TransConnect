using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class GestionCommande
    {

        private List<Commande> commandes = new List<Commande>();
        private int id = 0;

        public List<Commande> Commandes
        {
            get { return commandes; }
            set { commandes = value; }
        }

        public GestionCommande(List<Commande> commandes)
        {
            Commandes = new List<Commande>();
        }

        public GestionCommande() { }

        public void AjouterCommande(Client client, Salarie chauffeur, Vehicule vehicule, DateTime dateCommande, string villeDepart, string villeArrivee, decimal prix)
        {
            Chauffeur chauff = chauffeur as Chauffeur;  // Tentative de conversion
            if (chauff != null && !chauff.EstDisponible)
            {
                Console.WriteLine("Erreur: Le chauffeur n'est pas disponible à cette date.");
                return;
            }
            chauff.EstDisponible = false;
            Commande nouvelleCommande = new Commande(id++, client, chauffeur, vehicule, dateCommande, villeDepart, villeArrivee, prix);
            commandes.Add(nouvelleCommande);

            if (chauff != null)
            {
                chauff.Commandes.Add(nouvelleCommande);
            }
        }

        public void SupprimerCommande(Commande commande)
        {
            if (commandes.Remove(commande))
            {
                // Rétablir la disponibilité si nécessaire
                Chauffeur chauff = commande.Chauffeur as Chauffeur;
                if (chauff != null)
                {
                    chauff.EstDisponible = true;
                }
                Console.WriteLine("Commande supprimée avec succès.");
                
            }
        }

        public void ModifierCommande(Commande commande, string depart, string arrivee, decimal nouveauPrix)
        {
            commande.ModifierCommande(depart, arrivee, nouveauPrix);
            Console.WriteLine("Commande modifiée avec succès.");
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

        

        public static GestionCommande LoadFromJson(string jsonData)
        {
            var commandes = JsonConvert.DeserializeObject<List<Commande>>(jsonData) ?? new List<Commande>(); 
            var gestionCommande = new GestionCommande { Commandes = commandes };
            gestionCommande.id = commandes.Any() ? commandes.Max(c => c.IdCommande) + 1 : 0;
            return gestionCommande;
        }

        public void AfficherCommandesParPeriode(DateTime debut, DateTime fin)
        {
            var commandesParPeriode = commandes.Where(c => c.DateCommande >= debut && c.DateCommande <= fin).ToList();
            if (commandesParPeriode.Count == 0)
            {
                Console.WriteLine("Aucune commande trouvée pour cette période.");
            }
            foreach (var commande in commandesParPeriode)
            {
                Console.WriteLine(commande.ToString());
            }
        }


        public void AfficherMoyennePrixCommandes()
        {
            if (commandes.Count == 0)
            {
                Console.WriteLine("Aucune commande disponible pour calculer la moyenne des prix.");
                return;
            }

            var moyennePrix = commandes.Average(c => c.Prix);
            Console.WriteLine($"Moyenne des prix des commandes : {moyennePrix}€");
        }



        public void AfficherCommandesParClient(string numeroSS)
        {
            var commandesClient = commandes.Where(c => c.Client.NumeroSS == numeroSS).ToList();
            if (commandesClient.Count == 0)
            {
                Console.WriteLine("Aucune commande trouvée pour ce client.");
            }
            else
            {
                Console.WriteLine($"Nombre de commandes trouvées pour le client {numeroSS} : {commandesClient.Count}");
                foreach (var commande in commandesClient)
                {
                    Console.WriteLine(commande.ToString());
                }
            }
        }



    }

}
