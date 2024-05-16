using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class ConsoleChauffeurs
    {
        private GestionChauffeurs gestionChauffeurs;
        private Organigramme organigramme;
        public ConsoleChauffeurs(GestionChauffeurs gestionChauffeurs)
        {
            this.gestionChauffeurs = ChargerChauffeurs();
            this.organigramme = gestionChauffeurs.Organigramme;
        }

        public void Run()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.WriteLine("\nGestion des Chauffeurs:");
                Console.WriteLine("1. Ajouter un chauffeur");
                Console.WriteLine("2. Modifier un chauffeur");
                Console.WriteLine("3. Supprimer un chauffeur");
                Console.WriteLine("4. Afficher tous les chauffeurs");
                Console.WriteLine("5. Quitter");

                string choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        AjouterChauffeurConsole();
                        
                        break;
                    case "2":
                        ModifierChauffeurConsole();
                       
                        break;
                    case "3":
                        SupprimerChauffeurConsole();
                       
                        break;
                    case "4":
                        AfficherChauffeursConsole();
                        break;
                    case "5":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Option invalide. Veuillez réessayer.");
                        break;
                }
                SauvegarderChauffeurs();               
                SauvegarderOrganigramme();
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
            }
        }

        private void AjouterChauffeurConsole()
        {
            Console.WriteLine("Entrez le numéro de sécurité sociale du chauffeur :");
            string numeroSS = Console.ReadLine();
            Console.WriteLine("Entrez le nom du chauffeur :");
            string nom = Console.ReadLine();
            Console.WriteLine("Entrez le prénom du chauffeur :");
            string prenom = Console.ReadLine();
            Console.WriteLine("Entrez la date de naissance du chauffeur (yyyy-MM-dd) :");
            DateTime dateNaissance = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Entrez l'adresse du chauffeur :");
            string adresse = Console.ReadLine();
            Console.WriteLine("Entrez l'email du chauffeur :");
            string email = Console.ReadLine();
            Console.WriteLine("Entrez le numéro de téléphone du chauffeur :");
            string telephone = Console.ReadLine();
            Console.WriteLine("Entrez la date d'entrée du chauffeur (yyyy-MM-dd) :");
            DateTime dateEntre = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Entrez le poste du chauffeur :");
            string poste = Console.ReadLine();
            Console.WriteLine("Entrez le salaire du chauffeur :");
            decimal salaire = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Entrez le numéro SS du manager du chauffeur (laissez vide si aucun) :");
            string managerSS = Console.ReadLine();
            Salarie manager = null;
            if (!string.IsNullOrEmpty(managerSS))
            {
                manager = organigramme.TrouverSalarieParSS(managerSS);
                if (manager == null)
                {
                    Console.WriteLine("Manager non trouvé, veuillez vérifier le numéro SS ou continuer sans manager.");
                }
            }

            Chauffeur nouveauChauffeur = new Chauffeur(numeroSS, nom, prenom, dateNaissance, adresse, email, telephone, dateEntre, poste, salaire);
            gestionChauffeurs.AjouterChauffeur(nouveauChauffeur, manager);

           
        }


        private void ModifierChauffeurConsole()
        {
            Console.WriteLine("Entrez le numéro SS du chauffeur à modifier :");
            string id = Console.ReadLine();
            Chauffeur chauffeur = gestionChauffeurs.TrouverChauffeurParSS(id);
            if (chauffeur != null)
            {
                Console.WriteLine("Entrez le nouveau nom du chauffeur (laissez vide pour ne pas changer) :");
                string nom = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nom))
                {
                    chauffeur.Nom = nom;
                }

                Console.WriteLine("Entrez le nouveau numéro de téléphone du chauffeur (laissez vide pour ne pas changer) :");
                string telephone = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(telephone))
                {
                    chauffeur.Telephone = telephone;
                }

                Console.WriteLine("Chauffeur modifié avec succès.");
            }
            else
            {
                Console.WriteLine("Chauffeur non trouvé.");
            }
        }

        private void SupprimerChauffeurConsole()
        {
            Console.WriteLine("Entrez le numéro SS du chauffeur à supprimer :");
            string id = Console.ReadLine();
            Chauffeur chauffeur = gestionChauffeurs.TrouverChauffeurParSS(id);
            if (chauffeur != null)
            {
                gestionChauffeurs.SupprimerChauffeur(chauffeur);
                Console.WriteLine("Chauffeur supprimé avec succès.");
            }
            else
            {
                Console.WriteLine("Chauffeur non trouvé.");
            }
        }

        private void AfficherChauffeursConsole()
        {
            gestionChauffeurs.AfficherChauffeurs();
        }

        public void SauvegarderChauffeurs()
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

        private GestionChauffeurs ChargerChauffeurs()
        {
            if (File.Exists("chauffeurs.json"))
            {
                string jsonData = File.ReadAllText("chauffeurs.json");
                return GestionChauffeurs.LoadFromJson(jsonData);
            }
            return new GestionChauffeurs();
        }


        public void SauvegarderOrganigramme()
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

            string jsonData = JsonConvert.SerializeObject(gestionChauffeurs.Organigramme, settings);
            File.WriteAllText("organigramme.json", jsonData);
        }
    }
}
