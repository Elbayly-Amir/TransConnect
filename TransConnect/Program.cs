using TransConnect;

class Program
{
    static GestionClient gestionClient = new GestionClient();
    static GestionCommande gestionCommande = new GestionCommande();
    static Organigramme organigramme = new Organigramme(null); // Initialisation avec une racine nulle ou un salarié spécifique
    static GestionChauffeurs gestionChauffeurs = new GestionChauffeurs();
    static GestionEvaluations gestionEvaluations = new GestionEvaluations();
    static ConsoleEvaluations consoleEvaluations = new ConsoleEvaluations(gestionEvaluations, gestionClient, gestionChauffeurs);
    static GestionDepenses gestionDepenses = new GestionDepenses();
    static void Main(string[] args)
    {

        bool continuer = true;
        while (continuer)
        {
            Console.Clear();
            Console.WriteLine("Menu Principal de l'Application de Gestion:");
            Console.WriteLine("1. Gestion des salariés");
            Console.WriteLine("2. Gestion des clients");
            Console.WriteLine("3. Gestion des commandes");
            Console.WriteLine("4. Gestion des chauffeurs");
            Console.WriteLine("5. Statistiques");
            Console.WriteLine("6. Gestion des Dépenses");
            Console.WriteLine("7. Quitter");

            Console.Write("Entrez votre choix : ");
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    GestionSalaries();
                    break;
                case "2":
                    GestionClients();
                    break;
                case "3":
                    GestionCommandes();
                    break;
                case "4":
                    GestionChauffeur();
                    break;
                case "5":
                    GestionStatistique();
                    break;
                case "6":
                    GestionDepenses();
                    break;
                case "7":
                    continuer = false;
                    Console.WriteLine("Fermeture de l'application...");
                    break;
                default:
                    Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    break;
            }
        }
    }

    static void GestionSalaries()
    {
        Organigramme organigramme = new Organigramme(null);
        ConsoleOrganigramme organigrammeManager = new ConsoleOrganigramme(organigramme, gestionChauffeurs);
        organigrammeManager.Run();
    }

    static void GestionClients()
    {
        ConsoleClient clientManager = new ConsoleClient();
        clientManager.Run();
    }

    static void GestionCommandes()
    {
        ConsoleCommande commandeManager = new ConsoleCommande(gestionCommande, gestionClient, gestionChauffeurs);
        commandeManager.Run();
    }

    static void GestionChauffeur()
    {
        ConsoleChauffeurs chauffeurManager = new ConsoleChauffeurs(gestionChauffeurs);
        chauffeurManager.Run();
    }

    static void GestionStatistique()
    {
        ConsoleStatistique statsManager = new ConsoleStatistique(gestionChauffeurs, gestionCommande, gestionClient, gestionEvaluations, consoleEvaluations);
        statsManager.Run();
    }

    static void GestionDepenses()
    {
        ConsoleDepenses depensesManager = new ConsoleDepenses(gestionDepenses, organigramme);
        depensesManager.Run();
    }
}
    
