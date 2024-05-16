using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransConnect;

namespace TransConnect
{
    public class GestionClient
    {

        public List<Client> Clients { get; set; } = new List<Client>();

        public GestionClient(List<Client> clients)
        {
            this.Clients = clients;
        }

        public GestionClient()
        {
            
        }

        public void AjouterClient(Client client)
        {
            Clients.Add(client);
        }


        public Client TrouverClientParSS(string numeroSS)
        {
            Console.WriteLine($"Recherche du client avec SS: {numeroSS}");
            foreach (var client in Clients)
            {
                Console.WriteLine($"Client actuel: {client.Nom}");
                if (client.NumeroSS == numeroSS)
                {
                    return client;
                }
            }
            return null;
        }


        public List<Client> ListerClientsParVille(string ville)
        {
            return Clients.Where(c => c.Ville.Contains(ville)).ToList();
        }

        public List<Client> ListerClientsParAchat()
        {
            return Clients.OrderByDescending(c => c.Achats).ToList();
        }

        public void AfficherClients(List<Client> clients)
        {
            foreach (var client in clients)
            {
                Console.WriteLine($"Nom: {client.Nom}, Adresse: {client.Adresse}, Montant des Achats: {client.Achats}");
            }
        }

        public List<Client> GetClients()
        {
            return new List<Client>(Clients);
        }

        public void AfficherMoyenneAchatsClients()
        {
            if (!Clients.Any())
            {
                Console.WriteLine("Aucun client trouvé. Impossible de calculer la moyenne des achats.");
                return;
            }

            var moyenneAchats = Clients.Average(c => c.Achats);
            Console.WriteLine($"Moyenne des achats des clients : {moyenneAchats}€");
        }

        public static GestionClient LoadFromJson(string jsonData)
        {
            try
            {
                var clients = JsonConvert.DeserializeObject<List<Client>>(jsonData) ?? new List<Client>();
                var gestionClient = new GestionClient { Clients = clients };
                Console.WriteLine($"Nombre de clients chargés : {gestionClient.Clients.Count}");
                return gestionClient;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la désérialisation des clients : {ex.Message}");
                return new GestionClient();
            }
        }

        public void SauvegarderClients()
        {
            string jsonData = JsonConvert.SerializeObject(Clients, Formatting.Indented);
            File.WriteAllText("clients.json", jsonData);
        }
    }
}