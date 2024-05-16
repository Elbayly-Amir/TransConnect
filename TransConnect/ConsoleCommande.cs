using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class ConsoleCommande
    {

        private GestionCommande gestionCommande;
        private GestionClient gestionClient;
        private GestionChauffeurs gestionChauffeurs;
        private Graph cityGraph;

        public ConsoleCommande(GestionCommande gestionCommande, GestionClient gestionClient, GestionChauffeurs gestionChauffeurs)
        {
            this.gestionCommande =  ChargerCommande() ?? new GestionCommande();
            this.gestionClient = ChargerClient() ?? new GestionClient();
            this.gestionChauffeurs = ChargerChauffeurs() ?? new GestionChauffeurs();
            this.cityGraph = new Graph();
            this.cityGraph.LoadFromExcel(@"C:\Users\elbay\source\repos\TransConnect\Distances.xlsx");
        }


        public void Run()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.WriteLine("Gestion des Commandes:");
                Console.WriteLine("1. Ajouter une commande");
                Console.WriteLine("2. Modifier une commande");
                Console.WriteLine("3. Supprimer une commande");
                Console.WriteLine("4. Afficher toutes les commandes");
                Console.WriteLine("5. Quitter");

                string choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        AjouterCommandeConsole();
                        SauvegarderCommande();
                        break;
                    case "2":
                        ModifierCommandeConsole();
                        SauvegarderCommande();
                        break;
                    case "3":
                        SupprimerCommandeConsole();
                        SauvegarderCommande();
                        break;
                    case "4":
                        AfficherCommandesConsole();
                        break;
                    case "5":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Option invalide. Veuillez réessayer.");
                        break;
                }
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
            }
        }

        private void SauvegarderCommande()
        {
            string jsonData = JsonConvert.SerializeObject(gestionCommande.Commandes, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                {
                    IgnoreSerializableInterface = true,
                    IgnoreSerializableAttribute = true
                },
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            File.WriteAllText("commandes.json", jsonData);
        }


        private GestionCommande ChargerCommande()
        {
            if (File.Exists("commandes.json"))
            {
                string jsonData = File.ReadAllText("commandes.json");
                return GestionCommande.LoadFromJson(jsonData);
            }
            return new GestionCommande();
        }


        private GestionClient ChargerClient()
        {


            if (File.Exists("clients.json"))
            {
                string jsonData = File.ReadAllText("clients.json");
                return JsonConvert.DeserializeObject<GestionClient>(jsonData);
            }

            return null;
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

        private void SauvegarderChauffeurs()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                {
                    IgnoreSerializableInterface = true,
                    IgnoreSerializableAttribute = true
                },
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            string jsonData = JsonConvert.SerializeObject(gestionChauffeurs.Chauffeurs, settings);
            File.WriteAllText("chauffeurs.json", jsonData);
        }

        private void AfficherVillesDisponibles()
        {
            Console.WriteLine("Villes disponibles :");
            foreach (var city in cityGraph.GetCities())
            {
                Console.WriteLine(city);
            }
        }

        private void AjouterCommandeConsole()
        {
            AfficherVillesDisponibles();

            Console.WriteLine("Entrez le numéro SS du client :");
            string numeroSS = Console.ReadLine();
            Client client = gestionClient.TrouverClientParSS(numeroSS);
            if (client == null)
            {
                Console.WriteLine("Client non trouvé.");
                return;
            }

            Console.WriteLine("Entrez la ville de départ :");
            string villeDepart = Console.ReadLine().Trim().ToUpper();
            Console.WriteLine($"Ville de départ normalisée : {villeDepart}");

            Console.WriteLine("Entrez la ville d'arrivée :");
            string villeArrivee = Console.ReadLine().Trim().ToUpper();
            Console.WriteLine($"Ville d'arrivée normalisée : {villeArrivee}");

            if (!cityGraph.ContainsCity(villeDepart) || !cityGraph.ContainsCity(villeArrivee))
            {
                Console.WriteLine("Une ou les deux villes ne sont pas dans la liste des villes connues.");
                return;
            }

            var result = cityGraph.Dijkstra(villeDepart);
            if (result.ContainsKey(villeArrivee))
            {
                double distance = result[villeArrivee];
                Console.WriteLine($"Distance trouvée par Dijkstra : {distance} km");

                if (distance < 0 || distance > 10000)
                {
                    Console.WriteLine("Erreur: Distance calculée non réaliste.");
                    return;
                }

                decimal prix = CalculatePrice(distance);
                Console.WriteLine($"Prix calculé: {prix}€");

                Chauffeur chauffeur = SelectAvailableChauffeur();
                if (chauffeur == null) return;

                Console.WriteLine("Entrez le type de véhicule :");
                string typeVehicule = Console.ReadLine();

                Console.WriteLine("Entrez la date de la commande (yyyy-MM-dd) :");
                DateTime dateCommande = DateTime.Parse(Console.ReadLine());

                Vehicule vehicule = new Voiture(typeVehicule, distance);

                // Obtenir et afficher le chemin le plus court
                var chemin = cityGraph.GetShortestPath(villeDepart, villeArrivee);
                Console.WriteLine("Chemin le plus court : " + string.Join(" -> ", chemin));

                gestionCommande.AjouterCommande(client, chauffeur, vehicule, dateCommande, villeDepart, villeArrivee, prix);
                SauvegarderCommande();
                SauvegarderChauffeurs();
                Console.WriteLine("Commande ajoutée avec succès.");
            }
            else
            {
                Console.WriteLine("Aucun chemin trouvé ou erreur dans le calcul de la distance.");
            }
        }



        private Chauffeur SelectAvailableChauffeur()
        {
            Chauffeur chauffeur = null;
            while (chauffeur == null)
            {
                Console.WriteLine("Entrez l'ID du chauffeur :");
                string chauffeurId = Console.ReadLine();
                chauffeur = gestionChauffeurs.TrouverChauffeurParSS(chauffeurId) as Chauffeur;
                if (chauffeur == null || !chauffeur.EstDisponible)
                {
                    Console.WriteLine("Chauffeur non trouvé ou non disponible, veuillez réessayer.");
                    chauffeur = null; // Continue à demander un chauffeur valide
                }
            }
            return chauffeur;
        }


        private decimal CalculatePrice(double distance)
        {
            double tarifParKm = 1.5; // Define pricing per kilometer
            Console.WriteLine($"Calcul de prix pour la distance: {distance} km avec un tarif de {tarifParKm} par km");

            decimal prix;
            try
            {
                prix = (decimal)(distance * tarifParKm);
            }
            catch (OverflowException)
            {
                Console.WriteLine("Erreur de dépassement lors du calcul du prix.");
                throw;
            }

            return prix;
        }



        private void ModifierCommandeConsole()
        {
            Console.WriteLine("Entrez l'ID de la commande à modifier :");
            int id = int.Parse(Console.ReadLine());
            Commande commande = gestionCommande.CommandeParId(id);

            if (commande == null)
            {
                Console.WriteLine("Commande non trouvée.");
                return;
            }

            Console.WriteLine("Entrez la nouvelle ville de départ (appuyez sur Entrée pour ignorer) :");
            string depart = Console.ReadLine().Trim().ToUpper();
            depart = string.IsNullOrEmpty(depart) ? commande.VilleDepart : depart;
            Console.WriteLine($"Nouvelle ville de départ normalisée : {depart}");

            Console.WriteLine("Entrez la nouvelle ville d'arrivée (appuyez sur Entrée pour ignorer) :");
            string arrivee = Console.ReadLine().Trim().ToUpper();
            arrivee = string.IsNullOrEmpty(arrivee) ? commande.VilleArrivee : arrivee;
            Console.WriteLine($"Nouvelle ville d'arrivée normalisée : {arrivee}");

            if (!cityGraph.ContainsCity(depart) || !cityGraph.ContainsCity(arrivee))
            {
                Console.WriteLine("Une ou les deux villes ne sont pas dans la liste des villes connues.");
                return;
            }

            var result = cityGraph.Dijkstra(depart);
            if (result.ContainsKey(arrivee))
            {
                double distance = result[arrivee];
                Console.WriteLine($"Nouvelle distance trouvée par Dijkstra : {distance} km");

                if (distance < 0 || distance > 10000) // Ajustez cette valeur selon votre contexte
                {
                    Console.WriteLine("Erreur: Distance calculée non réaliste.");
                    return;
                }

                decimal nouveauPrix = CalculatePrice(distance);
                Console.WriteLine($"Nouveau prix calculé: {nouveauPrix}€");

                // Obtenir et afficher le nouveau chemin le plus court
                var chemin = cityGraph.GetShortestPath(depart, arrivee);
                Console.WriteLine("Nouveau chemin le plus court : " + string.Join(" -> ", chemin));

                // Mettre à jour la commande avec les nouvelles valeurs
                commande.VilleDepart = depart;
                commande.VilleArrivee = arrivee;
                commande.Prix = nouveauPrix;
                commande.Vehicule.Kilometrage = distance;
                gestionCommande.ModifierCommande(commande, depart, arrivee, nouveauPrix);
                Console.WriteLine("Commande modifiée avec succès.");
            }
            else
            {
                Console.WriteLine("Aucun chemin trouvé ou erreur dans le calcul de la distance.");
            }
        }



        private void SupprimerCommandeConsole()
        {
            Console.WriteLine("Entrez l'ID de la commande à supprimer :");
            int id = int.Parse(Console.ReadLine());
            Commande commande = gestionCommande.CommandeParId(id);

            if (commande != null)
            {
                gestionCommande.SupprimerCommande(commande);
                Console.WriteLine("Commande supprimée avec succès.");
            }
            else
            {
                Console.WriteLine("Commande non trouvée.");
            }
            SauvegarderChauffeurs();
        }

        private void AfficherCommandesConsole()
        {
            Console.WriteLine("Liste des commandes enregistrées :");
            gestionCommande.AfficherCommandes();
        }

       
        }
    }

