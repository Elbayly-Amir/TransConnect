using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class GestionClient
    {

        private List<Client> clients = new List<Client>();

        public void AjouterClient(Client client)
        {
            clients.Add(client);
        }

        public void SupprimerClient(Client client)
        {
            clients.Remove(client);
        }

        public void ModifierClient(Client client, string nouveauNom, string nouvelleAdresse, string nouvelEmail, string nouveauTelephone)
        {
            client.ModifierInformations(nouveauNom, nouvelleAdresse, nouvelEmail, nouveauTelephone);
        }

        public Client TrouverClientParNom(string nom)
        {
            return clients.FirstOrDefault(c => c.Nom == nom);
        }

        public List<Client> ListerClientsParVille(string ville)
        {
            return clients.Where(c => c.Adresse.Contains(ville)).ToList();
        }

        public List<Client> ListerClientsParAchat()
        {
            return clients.OrderByDescending(c => c.Achats).ToList();
        }

        public void AfficherClients(List<Client> clients)
        {
            foreach (var client in clients)
            {
                Console.WriteLine($"Nom: {client.Nom}, Adresse: {client.Adresse}, Montant des Achats: {client.Achats}");
            }
        }
    }
}
