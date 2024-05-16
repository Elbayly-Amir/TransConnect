using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public abstract class Personne
    {

        protected string numeroSS;
        protected string nom;
        protected string prenom;
        protected DateTime dateNaissance;
        protected string adresse;
        protected string email;
        protected string telephone;

        public Personne (string numeroSS, string nom, string prenom, DateTime dateNaissance, string adresse, string email, string telephone)
        {
            this.numeroSS = numeroSS;
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.adresse = adresse;
            this.email = email;
            this.telephone = telephone;
        }

        public void ModifierPersonne(string nouveauNom, string nouvelleAdresse, string nouvelEmail, string nouveauTelephone)
        {
            this.nom = nouveauNom;
            this.adresse= nouvelleAdresse;
            this.email = nouvelEmail;
            this.telephone = nouveauTelephone;
        }
        // Ici on définit nos propriétés 
        public string NumeroSS { get { return this.numeroSS; } set { this.numeroSS = value; } }
        public string Nom {  get { return this.nom; } set {   this.nom = value; }  }
        public string Prenom { get { return this.prenom; } set { this.prenom = value; } }
        public DateTime DateNaissance { get { return this.dateNaissance; } set { this.dateNaissance = value; } }
        public string Adresse { get {  return this.adresse; } set { this.adresse = value; } }
        public string Email { get { return this.email; } set { this.email = value; } }
        public string Telephone {  get { return this.telephone; } set { this.telephone = value; } }

    }
}
