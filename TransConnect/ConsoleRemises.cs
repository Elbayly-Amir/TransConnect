using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class ConsoleRemises
    {
        private GestionCommande gestionCommande;
        private GestionClient gestionClient;
        private IRemise remise;

        public ConsoleRemises(GestionCommande gestionCommande, GestionClient gestionClient, IRemise remise)
        {
            this.gestionCommande = gestionCommande;
            this.gestionClient = gestionClient;
            this.remise = remise;
        }

        public void Run()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.Clear();
                Console.WriteLine("Gestion des Remises:");
                Console.WriteLine("1. Vérifier l'éligibilité d'un client à une remise");
                Console.WriteLine("2. Appliquer une remise");
                Console.WriteLine("3. Retour");

                string choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        VerifierEligibiliteRemise();
                        break;
                    case "2":
                        AppliquerRemise();
                        break;
                    case "3":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;
                }
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
            }
        }

        private void VerifierEligibiliteRemise()
        {
            Console.WriteLine("Entrez le numéro de sécurité sociale du client :");
            string numeroSSClient = Console.ReadLine();
            Client client = gestionClient.TrouverClientParSS(numeroSSClient);
            if (client == null)
            {
                Console.WriteLine("Client non trouvé.");
                return;
            }

            List<Commande> commandesClient = gestionCommande.TrouverCommandesParClient(numeroSSClient);
            if (remise.EstEligible(commandesClient))
            {
                Console.WriteLine("Le client est éligible à une remise.");
            }
            else
            {
                Console.WriteLine("Le client n'est pas éligible à une remise.");
            }
        }

        private void AppliquerRemise()
        {
            Console.WriteLine("Entrez le numéro de sécurité sociale du client :");
            string numeroSSClient = Console.ReadLine();
            Client client = gestionClient.TrouverClientParSS(numeroSSClient);
            if (client == null)
            {
                Console.WriteLine("Client non trouvé.");
                return;
            }

            List<Commande> commandesClient = gestionCommande.TrouverCommandesParClient(numeroSSClient);
            decimal montantRemise = remise.CalculerRemise(commandesClient);

            if (montantRemise > 0)
            {
                Console.WriteLine($"Une remise de {montantRemise}€ a été appliquée.");
                // Logique pour appliquer la remise (ex: mise à jour des commandes, etc.)
            }
            else
            {
                Console.WriteLine("Le client n'est pas éligible à une remise.");
            }
        }
    }
}
