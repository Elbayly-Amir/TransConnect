using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class ConsoleReclamations
    {
        private GestionReclamations gestionReclamations;
        private GestionClient gestionClient;

        public ConsoleReclamations(GestionReclamations gestionReclamations)
        {
            this.gestionReclamations = gestionReclamations;
            this.gestionClient = ChargerClients();
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

        public void Run()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.Clear();
                Console.WriteLine("Gestion des Réclamations:");
                Console.WriteLine("1. Ajouter une réclamation");
                Console.WriteLine("2. Afficher les réclamations");
                Console.WriteLine("3. Retour");

                string choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        AjouterReclamation();
                        break;
                    case "2":
                        AfficherReclamations();
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

        private void AjouterReclamation()
        {
            Console.WriteLine("Entrez le numéro de sécurité sociale du client :");
            string numeroSSClient = Console.ReadLine();
            Client client = gestionClient.TrouverClientParSS(numeroSSClient);
            if (client == null)
            {
                Console.WriteLine("Client non trouvé.");
                return;
            }

            Console.WriteLine("Entrez la description de la réclamation :");
            string description = Console.ReadLine();
            DateTime dateReclamation = DateTime.Now;

            Reclamation reclamation = new Reclamation(numeroSSClient, description, dateReclamation);
            gestionReclamations.AjouterReclamation(reclamation);
            gestionReclamations.SauvegarderReclamations();
            Console.WriteLine("Réclamation ajoutée avec succès.");
        }

        private void AfficherReclamations()
        {
            LinkedList<Reclamation> reclamations = gestionReclamations.ListerReclamations();
            foreach (var reclamation in reclamations)
            {
                Client client = gestionClient.TrouverClientParSS(reclamation.NumeroSSClient);
                string nomClient = client != null ? $"{client.Nom} {client.Prenom}" : "Nom inconnu";
                Console.WriteLine($"Client: {nomClient}, Description: {reclamation.Description}, Date: {reclamation.DateReclamation}");
            }
        }
    }
}
