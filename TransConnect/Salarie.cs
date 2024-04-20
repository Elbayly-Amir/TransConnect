using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Salarie :  Personne, IModifiable
    {

        public DateTime dateEntre;
        public string poste;
        public decimal salaire;
        public Salarie manager;
        public List<Salarie> employe { get; set; } = new List<Salarie>();

        public Salarie(string numeroSS, string nom, string prenom, DateTime dateNaissance, string adresse, string email, string telephone, DateTime dateEntre, string poste, decimal salaire) : base(numeroSS, nom, prenom, dateNaissance, adresse, email, telephone)
        {
            this.dateEntre = dateEntre;
            this.poste = poste;
            this.salaire = salaire;
            this.manager = manager;
        }

        public void AjouterSalarie(Salarie salarie)
        {

            employe.Add(salarie);
        }

        public void SupprimerSalarie(Salarie salarie)
        {
            employe.Remove(salarie);
        }

        public void ModifierSalarie(string nouveauPoste, decimal nouveauSalaire)
        {
            this.poste = nouveauPoste;
            this.salaire = nouveauSalaire;
        }

        public void ModifierInformations(string nouveauNom, string nouvelleAdresse, string nouvelEmail, string nouveauTelephone)
        {
            this.nom = nouveauNom;
            this.adresse = nouvelleAdresse;
            this.email = nouvelEmail;
            this.telephone = nouveauTelephone;
        }

        public DateTime DateEntre { get { return dateEntre; } set {  dateEntre = value; } }
        public string Poste { get { return poste; } set { poste = value; } }
        public decimal Salaire { get {  return salaire; } set {  salaire = value; } }
        public Salarie Manager { get { return manager; } set {  manager = value; } }


    }
}
