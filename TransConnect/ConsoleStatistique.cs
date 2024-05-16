using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class ConsoleStatistique
    {

        private GestionChauffeurs gestionChauffeurs;
        private GestionCommande gestionCommande;
        private GestionClient gestionClient;
        private GestionEvaluations gestionEvaluations;
        private ConsoleEvaluations consoleEvaluations;

        public ConsoleStatistique(GestionChauffeurs gestionChauffeurs, GestionCommande gestionCommande, GestionClient gestionClient, GestionEvaluations gestionEvaluations, ConsoleEvaluations consoleEvaluations)
        {
            this.gestionChauffeurs = ChargerChauffeurs() ?? new GestionChauffeurs();
            this.gestionCommande = ChargerCommandes() ?? new GestionCommande();
            ChargerClients();
            this.gestionEvaluations = gestionEvaluations;
            this.consoleEvaluations = consoleEvaluations;
        }
            public void Run()
        {

            bool continuer = true;
            while (continuer)
            {
                Console.Clear();
                Console.WriteLine("Menu Statistiques:");
                Console.WriteLine("1. Afficher par chauffeur le nombre de livraisons effectuées");
                Console.WriteLine("2. Afficher les commandes selon une période de temps");
                Console.WriteLine("3. Afficher la moyenne des prix des commandes");
                Console.WriteLine("4. Evaluation des chauffeurs");
                Console.WriteLine("5. Afficher la liste des commandes pour un client");
                Console.WriteLine("6. Retour au menu principal");

                Console.Write("Entrez votre choix : ");
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        gestionChauffeurs.AfficherLivraisonsParChauffeur();
                        break;
                    case "2":
                        AfficherCommandesParPeriode();
                        break;
                    case "3":
                        gestionCommande.AfficherMoyennePrixCommandes();
                        break;
                    case "4":
                        consoleEvaluations.Run();
                        break;
                    case "5":
                        AfficherCommandesParClient();
                        break;
                    case "6":
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

        private void AfficherCommandesParPeriode()
        {
            Console.WriteLine("Entrez la date de début (yyyy-MM-dd) :");
            DateTime debut;
            while (!DateTime.TryParse(Console.ReadLine(), out debut))
            {
                Console.WriteLine("Format de date invalide. Veuillez réessayer (yyyy-MM-dd) :");
            }

            Console.WriteLine("Entrez la date de fin (yyyy-MM-dd) :");
            DateTime fin;
            while (!DateTime.TryParse(Console.ReadLine(), out fin))
            {
                Console.WriteLine("Format de date invalide. Veuillez réessayer (yyyy-MM-dd) :");
            }

            gestionCommande.AfficherCommandesParPeriode(debut, fin);
        }


        private void AfficherCommandesParClient()
        {
            Console.WriteLine("Entrez le numéro SS du client :");
            string numeroSS = Console.ReadLine();
            gestionCommande.AfficherCommandesParClient(numeroSS);
        }

        private GestionCommande ChargerCommandes()
        {
            if (File.Exists("commandes.json"))
            {
                string jsonData = File.ReadAllText("commandes.json");
                return GestionCommande.LoadFromJson(jsonData);
            }
            return new GestionCommande();
        }

        private List<Client> ChargerClients()
        {
            if (File.Exists("clients.json"))
            {
                string jsonData = File.ReadAllText("clients.json");
                return JsonConvert.DeserializeObject<List<Client>>(jsonData) ?? new List<Client>();
            }
            return new List<Client>();
        }

        private GestionChauffeurs ChargerChauffeurs()
        {
            if (File.Exists("chauffeurs.json"))
            {
                string jsonData = File.ReadAllText("chauffeurs.json");
                return GestionChauffeurs.LoadFromJson(jsonData);
            }
            return new GestionChauffeurs();
        }
    }
}
