using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class ConsoleEvaluations
    {
        private GestionEvaluations gestionEvaluations;
        private GestionClient gestionClient;
        private GestionChauffeurs gestionChauffeurs;

        public ConsoleEvaluations(GestionEvaluations gestionEvaluations, GestionClient gestionClient, GestionChauffeurs gestionChauffeurs)
        {
            this.gestionEvaluations = gestionEvaluations;
            this.gestionClient = ChargerClients();
            this.gestionChauffeurs = ChargerChauffeurs();
        }
        private GestionClient ChargerClients()
        {
            if (File.Exists("clients.json"))
            {
                string jsonData = File.ReadAllText("clients.json");
                var clients = JsonConvert.DeserializeObject<List<Client>>(jsonData) ?? new List<Client>();
                return new GestionClient(clients);
            }
            return new GestionClient();
        }

        private GestionChauffeurs ChargerChauffeurs()
        {
            if (File.Exists("chauffeurs.json"))
            {
                string jsonData = File.ReadAllText("chauffeurs.json");
                var chauffeurs = JsonConvert.DeserializeObject<List<Chauffeur>>(jsonData) ?? new List<Chauffeur>();
                return new GestionChauffeurs(chauffeurs);
            }
            return new GestionChauffeurs();
        }
        public void Run()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.Clear();
                Console.WriteLine("Gestion des Évaluations des Chauffeurs:");
                Console.WriteLine("1. Ajouter une évaluation");
                Console.WriteLine("2. Afficher les évaluations d'un chauffeur");
                Console.WriteLine("3. Retour");

                string choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        AjouterEvaluation();
                        break;
                    case "2":
                        AfficherEvaluations();
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

        private void AjouterEvaluation()
        {
            Console.WriteLine("Entrez le numéro de sécurité sociale du client :");
            string numeroSSClient = Console.ReadLine();
            Client client = gestionClient.TrouverClientParSS(numeroSSClient);
            if (client == null)
            {
                Console.WriteLine("Client non trouvé.");
                return;
            }

            Console.WriteLine("Entrez le numéro de sécurité sociale du chauffeur :");
            string numeroSSChauffeur = Console.ReadLine();
            Chauffeur chauffeur = gestionChauffeurs.TrouverChauffeurParSS(numeroSSChauffeur);
            if (chauffeur == null)
            {
                Console.WriteLine("Chauffeur non trouvé.");
                return;
            }

            Console.WriteLine("Entrez la note (1-5) :");
            int note = int.Parse(Console.ReadLine());
            Console.WriteLine("Entrez le commentaire :");
            string commentaire = Console.ReadLine();
            EvaluationChauffeur evaluation = new EvaluationChauffeur(client, chauffeur, note, commentaire);
            gestionEvaluations.AjouterEvaluation(evaluation);
        }

        private void AfficherEvaluations()
        {
            List<EvaluationChauffeur> evaluations = gestionEvaluations.ChargerEvaluations();
            evaluations.ForEach(evaluation =>
            {
                Console.WriteLine($"Client: {evaluation.Client.Nom} {evaluation.Client.Prenom}, " +
                                  $"Chauffeur: {evaluation.Chauffeur.Nom} {evaluation.Chauffeur.Prenom}, " +
                                  $"Note: {evaluation.Note}, Commentaire: {evaluation.Commentaire}");
            });
        }

        
    }

}
