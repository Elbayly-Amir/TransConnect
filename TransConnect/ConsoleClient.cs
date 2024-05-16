using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class ConsoleClient
    {
        private GestionClient gestionClient = new GestionClient();

        public ConsoleClient()
        {
            ChargerClients();
        }
        public void Run()
        {
            // Boucle de menu pour les actions liées au client
            bool continuer = true;
            while (continuer)
            {
                Console.WriteLine("1. Ajouter un client\n2. Modifier un client\n3. Afficher les clients\n4. Quitter");
                string choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        AjouterClientConsole();
                        SauvegarderClients();
                        break;
                    case "2":
                        ModifierClientConsole(gestionClient);
                        SauvegarderClients();
                        break;
                    case "3":
                        AfficherClients();
                        SauvegarderClients();
                        break;
                    case "4":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Option invalide.");
                        break;
                }
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
            }
        }

        private void SauvegarderClients()
        {
            gestionClient.SauvegarderClients();
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


        public void AjouterClientConsole()
        {
            Console.WriteLine("Entrez le numéro de sécurité sociale du client :");
            string numeroSS = Console.ReadLine();
            Console.WriteLine("Entrez le nom du client :");
            string nom = Console.ReadLine();
            Console.WriteLine("Entrez le prénom du client :");
            string prenom = Console.ReadLine();
            Console.WriteLine("Entrez la date de naissance du client (YYYY-MM-DD) :");
            DateTime dateNaissance = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Entrez l'adresse du client :");
            string adresse = Console.ReadLine();
            Console.WriteLine("Entrez l'email du client :");
            string email = Console.ReadLine();
            Console.WriteLine("Entrez le téléphone du client :");
            string telephone = Console.ReadLine();
            Console.WriteLine("Entrez le montant initial des achats du client :");
            decimal achats = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Entrez la ville du client :");
            string ville = Console.ReadLine();

            Client client = new Client(ville, achats, numeroSS, nom, prenom, dateNaissance, adresse, email, telephone);
            gestionClient.AjouterClient(client);
            Console.WriteLine("Client ajouté avec succès.");
        }

        public static void ModifierClientConsole(GestionClient gestionClient)
        {
            Console.WriteLine("Entrez le numéro de sécurité sociale du client à modifier :");
            string numeroSS = Console.ReadLine();
            Client client = gestionClient.TrouverClientParSS(numeroSS);  // Utilisation de numéro SS pour une identification plus précise

            if (client != null)
            {
                Console.WriteLine("Entrez le nouveau nom (laissez vide pour ne pas changer) :");
                string nouveauNom = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nouveauNom))
                    client.Nom = nouveauNom;

                Console.WriteLine("Entrez la nouvelle adresse (laissez vide pour ne pas changer) :");
                string nouvelleAdresse = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nouvelleAdresse))
                    client.Adresse = nouvelleAdresse;

                Console.WriteLine("Entrez le nouvel email (laissez vide pour ne pas changer) :");
                string nouvelEmail = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nouvelEmail))
                    client.Email = nouvelEmail;

                Console.WriteLine("Entrez le nouveau téléphone (laissez vide pour ne pas changer) :");
                string nouveauTelephone = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nouveauTelephone))
                    client.Telephone = nouveauTelephone;

                Console.WriteLine("Client modifié avec succès.");
            }
            else
            {
                Console.WriteLine("Client non trouvé.");
            }
        }


        public void AfficherClients()
        {
            Console.WriteLine("Choisissez un critère d'affichage :\n1. Alphabétique\n2. Par ville\n3. Par montant des achats");
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    var clientsAlpha = gestionClient.GetClients().OrderBy(c => c.Nom).ToList();
                    gestionClient.AfficherClients(clientsAlpha);
                    break;
                case "2":
                    Console.WriteLine("Entrez la ville :");
                    string ville = Console.ReadLine();
                    var clientsVille = gestionClient.ListerClientsParVille(ville);
                    gestionClient.AfficherClients(clientsVille);
                    break;
                case "3":
                    var clientsAchats = gestionClient.ListerClientsParAchat();
                    gestionClient.AfficherClients(clientsAchats);
                    break;
            }
        }

    }
}