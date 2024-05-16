using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Organigramme
    {

        public Salarie racine;

        public Salarie Racine { get {  return racine; } set { racine = value; } }

        public Organigramme(Salarie racine)
        {
            this.racine = racine;
        }


        public void AjouterSalarieAuManager(Salarie n, Salarie chef)
        {
            if (chef != null)
            {
                if (chef.Employes == null)
                    chef.Employes = new List<Salarie>();
                    chef.Employes.Add(n);
                    n.Manager = chef;
                    Console.WriteLine($"{n.Prenom} ajouté sous {chef.Prenom}");
            }
            else if (this.racine == null)
            {
                racine = n;
                Console.WriteLine("Ajouté comme racine de l'organigramme.");
            }
            else
            {
                Console.WriteLine("Opération non autorisée : un manager doit être spécifié ou l'organigramme doit être vide pour définir une nouvelle racine.");
            }
        }



        public void SupprimerSalarieOrganigramme(Salarie salarie)
        {
            if (salarie == null) return;

            // Supprimer le salarié de la liste des employés de son manager, si applicable
            if (salarie.Manager != null)
            {
                salarie.Manager.Employes.Remove(salarie);
            }

            // S'assurer que tous les employés de ce salarié soient réaffectés ou traités correctement
            foreach (Salarie subordonne in salarie.Employes.ToList())
            {
                // Option 1: Réaffecter les subordonnés à un autre manager ou les rendre indépendants
                subordonne.Manager = salarie.Manager; // Réaffecter au manager du salarié supprimé
                if (salarie.Manager != null)
                {
                    salarie.Manager.Employes.Add(subordonne); // Ajouter les subordonnés au manager du salarié supprimé
                }
            }

            // Réinitialiser les liens de ce salarié
            salarie.Employes.Clear();
            salarie.Manager = null;

            // Gestion spéciale si le salarié est la racine
            if (salarie == this.racine)
            {
                this.racine = null; // Mettre à jour la racine si nécessaire
            }
        }



        public void AfficherOrganigramme(Salarie salarie, int niveau = 0)
        {
            if (niveau < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(niveau), "Le niveau ne peut pas être négatif.");
            }

            string indent = new String(' ', niveau * 4);
            string line = (niveau > 0) ? "├── " : "";
            string link = (niveau > 0) ? "|" : "";

            // Change la couleur de la console pour le directeur général
            if (niveau == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            // Affichage du salarié
            Console.WriteLine($"{indent}{link}");
            Console.WriteLine($"{indent}{line}{salarie.Prenom} {salarie.Nom} / {salarie.Poste}");
            Console.ResetColor(); // Reset la couleur pour les autres éléments

            // Préparez le lien vertical pour les enfants si nécessaire
            string spaceForLink = indent.Length > 0 ? new String(' ', indent.Length - 1) : "";
            for (int i = 0; i < salarie.Employes.Count; i++)
            {
                var subordonne = salarie.Employes[i];
                // Affiche un lien vertical pour relier les enfants au parent
                Console.WriteLine($"{spaceForLink}{(i == salarie.Employes.Count - 1 ? " " : "|")}");
                AfficherOrganigramme(subordonne, niveau + 1);
            }
        }

        public Salarie TrouverSalarieParSS(string numeroSS)
        {
            return TrouverSalarieParSSRecursive(this.racine, numeroSS);
        }

        private Salarie TrouverSalarieParSSRecursive(Salarie current, string numeroSS)
        {
            if (current == null) return null;
            if (current.NumeroSS == numeroSS) return current;

            foreach (var subordonne in current.Employes)
            {
                Salarie found = TrouverSalarieParSSRecursive(subordonne, numeroSS);
                if (found != null) return found;
            }
            return null;
        }


    }
}
