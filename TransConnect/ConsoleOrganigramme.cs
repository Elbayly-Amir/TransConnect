using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class ConsoleOrganigramme
    {
        private Organigramme organigramme;
        private GestionChauffeurs gestionChauffeurs;
        public ConsoleOrganigramme(Organigramme organigramme, GestionChauffeurs gestionChauffeurs)
        {
            this.organigramme = ChargerOrganigramme() ?? new Organigramme(null);
            this.gestionChauffeurs = gestionChauffeurs ?? new GestionChauffeurs();
        }

        public void Run()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.WriteLine("Gestion de l'Organigramme:");
                Console.WriteLine("1. Ajouter un salarié");
                Console.WriteLine("2. Supprimer un salarié");
                Console.WriteLine("3. Modifier un salarié");
                Console.WriteLine("4. Afficher l'organigramme");
                Console.WriteLine("5. Quitter");

                string choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        AjouterSalarieConsole();
                        SauvegarderOrganigramme();
                        break;
                    case "2":
                        SupprimerSalarieConsole();
                        SauvegarderOrganigramme();
                        break;
                    case "3":
                        ModifierSalarieConsole();
                        SauvegarderOrganigramme();
                        break;
                    case "4":
                        AfficherOrganigrammeConsole();
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

        private void SauvegarderOrganigramme()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            string jsonData = JsonConvert.SerializeObject(this.organigramme, settings);
            File.WriteAllText("organigramme.json", jsonData);
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

        private Organigramme ChargerOrganigramme()
        {
            if (File.Exists("organigramme.json"))
            {
                string jsonData = File.ReadAllText("organigramme.json");
                return JsonConvert.DeserializeObject<Organigramme>(jsonData);
            }
            return null;
        }

        private void AjouterSalarieConsole()
        {
            Console.WriteLine("Entrez le numéro de sécurité sociale du salarié :");
            string numeroSS = Console.ReadLine();
            Console.WriteLine("Entrez le nom du salarié :");
            string nom = Console.ReadLine();
            Console.WriteLine("Entrez le prénom du salarié :");
            string prenom = Console.ReadLine();
            Console.WriteLine("Entrez la date de naissance du salarié (yyyy-MM-dd) :");
            DateTime dateNaissance = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Entrez l'adresse du salarié :");
            string adresse = Console.ReadLine();
            Console.WriteLine("Entrez l'email du salarié :");
            string email = Console.ReadLine();
            Console.WriteLine("Entrez le téléphone du salarié :");
            string telephone = Console.ReadLine();
            Console.WriteLine("Entrez la date d'entrée du salarié (yyyy-MM-dd) :");
            DateTime dateEntre = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Entrez le poste du salarié :");
            string poste = Console.ReadLine();
            Console.WriteLine("Entrez le salaire du salarié :");
            decimal salaire = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Entrez le numéro SS du manager (laissez vide si aucun) :");
            string managerSS = Console.ReadLine();

            Salarie manager = string.IsNullOrEmpty(managerSS) ? null : organigramme.TrouverSalarieParSS(managerSS);
            Salarie nouveauSalarie = new Salarie(numeroSS, nom, prenom, dateNaissance, adresse, email, telephone, dateEntre, poste, salaire);
            organigramme.AjouterSalarieAuManager(nouveauSalarie, manager);
            Console.WriteLine("Salarie ajouté avec succès à l'organigramme.");
            SauvegarderOrganigramme();
        }


        private void SupprimerSalarieConsole()
        {
            Console.WriteLine("Entrez le numéro de sécurité sociale du salarié à supprimer :");
            string numeroSS = Console.ReadLine();
            Salarie salarie = organigramme.TrouverSalarieParSS(numeroSS);
            if (salarie != null)
            {
                organigramme.SupprimerSalarieOrganigramme(salarie);
                Console.WriteLine("Salarie supprimé avec succès.");
            }
            else
            {
                Console.WriteLine("Salarie non trouvé.");
            }
        }

        private void ModifierSalarieConsole()
        {
            Console.WriteLine("Entrez le numéro de sécurité sociale du salarié à modifier :");
            string numeroSS = Console.ReadLine();
            Salarie salarie = organigramme.TrouverSalarieParSS(numeroSS);
            if (salarie != null)
            {
                Console.WriteLine("Entrez le nom (laissez vide pour ne pas changer) :");
                string nouveauNom = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nouveauNom))
                    salarie.Nom = nouveauNom;

                Console.WriteLine("Entrez le prénom (laissez vide pour ne pas changer) :");
                string nouveauPrenom = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nouveauPrenom))
                    salarie.Prenom = nouveauPrenom;

                Console.WriteLine("Entrez le nouveau salaire (laissez vide pour ne pas changer) :");
                string salaire = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(salaire))
                    salarie.Salaire = decimal.Parse(salaire);

                Console.WriteLine("Salarie modifié avec succès.");
            }
            else
            {
                Console.WriteLine("Salarie non trouvé.");
            }
        }

        private void AfficherOrganigrammeConsole()
        {
            if (organigramme.Racine != null)
            {
                organigramme.AfficherOrganigramme(organigramme.Racine);
            }
            else
            {
                Console.WriteLine("L'organigramme est vide.");
            }
        }

    }
}
