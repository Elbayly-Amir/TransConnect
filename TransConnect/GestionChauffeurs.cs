using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class GestionChauffeurs
    {

        private List<Chauffeur> chauffeurs = new List<Chauffeur>();
        public Organigramme Organigramme { get; set; }
        public List<Chauffeur> Chauffeurs
        {
            get { return chauffeurs; }
            set { chauffeurs = value; }
        }

        public GestionChauffeurs(List<Chauffeur> chauffeurs)
        {
            Organigramme = ChargerOrganigramme() ?? new Organigramme(null);
            this.chauffeurs = chauffeurs;
        }

        public GestionChauffeurs()
        {
            chauffeurs = ChargerChauffeurs();
        }

        public void AjouterChauffeur(Chauffeur chauffeur, Salarie manager)
        {
            if (!ChauffeurExiste(chauffeur.NumeroSS))
            {
                chauffeurs.Add(chauffeur);
                Console.WriteLine("Chauffeur ajouté avec succès.");

                if (manager != null)
                {
                    Console.WriteLine($"Ajout du chauffeur sous le manager : {manager.Nom}");
                    Organigramme.AjouterSalarieAuManager(chauffeur, manager);
                }
                else
                {
                    Console.WriteLine("Aucun manager spécifié. Tentative d'ajout du chauffeur à la racine.");
                    Organigramme.AjouterSalarieAuManager(chauffeur, null);
                }
                SauvegarderOrganigramme();
            }
            else
            {
                Console.WriteLine("Un chauffeur avec ce numéro SS existe déjà.");
            }
        }


        public void AfficherChauffeurs()
        {
            foreach (var chauff in chauffeurs)
            {
                Console.WriteLine($"ID: {chauff.Id}, Nom: {chauff.Nom}, Disponible: {chauff.EstDisponible}");
            }
        }

        public void SupprimerChauffeur(Chauffeur chauffeur)
        {
            if (chauffeurs.Remove(chauffeur))
            {
                Console.WriteLine("Chauffeur supprimé avec succès.");
            }
            else
            {
                Console.WriteLine("Chauffeur introuvable.");
            }
        }


        public bool ChauffeurExiste(string id)
        {
            return chauffeurs.Any(c => c.NumeroSS == id);
        }

        public Chauffeur TrouverChauffeurParSS(string id)
        {
            return chauffeurs.FirstOrDefault(chauffeur => chauffeur.NumeroSS == id);
        }

        private List<Chauffeur> ChargerChauffeurs()
        {
            if (File.Exists("chauffeurs.json"))
            {
                string jsonData = File.ReadAllText("chauffeurs.json");
                return JsonConvert.DeserializeObject<List<Chauffeur>>(jsonData);
            }
            return new List<Chauffeur>();
        }

        private Organigramme ChargerOrganigramme()
        {
            if (File.Exists("organigramme.json"))
            {
                string jsonData = File.ReadAllText("organigramme.json");
                return JsonConvert.DeserializeObject<Organigramme>(jsonData);
            }
            return new Organigramme(null);
        }

        public void SauvegarderOrganigramme()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            string jsonData = JsonConvert.SerializeObject(Organigramme, settings);
            File.WriteAllText("organigramme.json", jsonData);
        }

        public static GestionChauffeurs LoadFromJson(string jsonData)
        {
            var result = new GestionChauffeurs();
            result.Chauffeurs = JsonConvert.DeserializeObject<List<Chauffeur>>(jsonData) ?? new List<Chauffeur>();
            result.Organigramme = ChargerOrganigrammeDepuisJson(); // Cette méthode doit aussi être définie pour charger l'organigramme.
            return result;
        }

        private static Organigramme ChargerOrganigrammeDepuisJson()
        {
            if (File.Exists("organigramme.json"))
            {
                string jsonData = File.ReadAllText("organigramme.json");
                return JsonConvert.DeserializeObject<Organigramme>(jsonData);
            }
            return new Organigramme(null);
        }

        public void AfficherLivraisonsParChauffeur()
        {
            foreach (var chauffeur in chauffeurs)
            {
                int nombreLivraisons = chauffeur.Commandes.Count;
                Console.WriteLine($"Chauffeur: {chauffeur.Nom}, Nombre de livraisons: {nombreLivraisons}");
            }
        }



    }
}

