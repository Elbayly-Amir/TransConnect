using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public  class Client : Personne, IModifiable
    {
        string ville;
        public string Ville { get { return ville; } set {  ville = value; } }
        decimal achats;
        public decimal Achats { get { return achats; } set {  achats = value; } }
        public Client(string ville, decimal achats, string numeroSS, string nom, string prenom, DateTime dateNaissance, string adresse, string email, string telephone): base(numeroSS, nom, prenom, dateNaissance, adresse, email, telephone)
        {
            this.achats = achats;
            this.ville = ville;
        }

        public void AjouterAchats(decimal total)
        {
            achats += total;
        }

        public void ModifierInformations(string nouveauNom, string nouvelleAdresse, string nouvelEmail, string nouveauTelephone)
        {
            Nom = nouveauNom;
            Adresse = nouvelleAdresse;
            Email = nouvelEmail;
            Telephone = nouveauTelephone;
        }
    }
}
