using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class ConsoleDepenses
    {
        private GestionDepenses gestionDepenses;
        private Organigramme organigramme;

        public ConsoleDepenses(GestionDepenses gestionDepenses, Organigramme organigramme)
        {
            this.gestionDepenses = gestionDepenses;
            this.organigramme = ChargerOrganigramme();
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

        public void Run()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.Clear();
                Console.WriteLine("Gestion des Dépenses:");
                Console.WriteLine("1. Ajouter une dépense");
                Console.WriteLine("2. Afficher les dépenses");
                Console.WriteLine("3. Retour");

                string choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        AjouterDepense();
                        break;
                    case "2":
                        AfficherDepenses(gestionDepenses.Depenses.ToList(), organigramme);
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

        private void AjouterDepense()
        {
            Console.WriteLine("Entrez le numéro de sécurité sociale du salarié :");
            string numeroSSSalarie = Console.ReadLine();
            Salarie salarie = organigramme.TrouverSalarieParSS(numeroSSSalarie);
            if (salarie == null)
            {
                Console.WriteLine("Salarié non trouvé.");
                return;
            }

            Console.WriteLine("Entrez la description de la dépense :");
            string description = Console.ReadLine();
            Console.WriteLine("Entrez le montant de la dépense :");
            decimal montant = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Entrez la date de la dépense (yyyy-MM-dd) :");
            DateTime dateDepense = DateTime.Parse(Console.ReadLine());

            Depenses depense = new Depenses(Guid.NewGuid().ToString(), numeroSSSalarie, description, montant, dateDepense);
            gestionDepenses.AjouterDepense(depense);
            Console.WriteLine("Dépense ajoutée avec succès.");
        }

        private static void AfficherDepenses(List<Depenses> depenses, Organigramme organigramme)
        {
            depenses.ForEach(s => {
            Salarie salarie = organigramme.TrouverSalarieParSS(s.NumeroSSSalarie);
            string nomSalarie = salarie != null ? $"{salarie.Nom} {salarie.Prenom}" : "Nom inconnu";
            Console.WriteLine($"Salarié: {s.NumeroSSSalarie}->{nomSalarie}, Description: {s.Description}, Montant: {s.Montant}, Date: {s.DateDepense}");

                });
        }
        }
    }

