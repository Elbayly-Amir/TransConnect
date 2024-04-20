using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Commande
    {

        int id_commande;
        Client client;
        Salarie chauffeur;
        Vehicule vehicule;
        DateTime dateCommande;
        string ville_depart;
        string ville_arrivee;
        decimal prix;


        public Commande(int id_commande, Client client, Salarie chauffeur, Vehicule vehicule, DateTime dateCommande, string ville_depart, string ville_arrivee, decimal prix)
        {
            this.id_commande = id_commande;
            this.client = client;
            this.chauffeur = chauffeur;
            this.vehicule = vehicule;
            this.dateCommande = dateCommande;
            this.ville_depart = ville_depart;
            this.ville_arrivee = ville_arrivee;
            this.prix = prix;
        }

        public void ModifierCommande( string depart, string arrivee,  decimal nouveauPrix)
        {
            ville_depart = depart;
            ville_arrivee=arrivee;
            prix = nouveauPrix;
        }

        public override string ToString()
        {
            return $"Commande ID: {IdCommande}, Client: {Client.Nom}, Chauffeur: {Chauffeur.Nom}, Date: {DateCommande.ToShortDateString()}, De {VilleDepart} à {VilleArrivee}, Prix: {Prix}€";
        }

        public int IdCommande {  get { return id_commande; } set { id_commande = value; } }
        public Client Client { get { return client; } }
        public Salarie Chauffeur { get {  return chauffeur; } set {  chauffeur = value; } }
        public Vehicule Vehicule { get { return vehicule; } set { vehicule = value; } }
        public DateTime DateCommande { get { return dateCommande; } set {  dateCommande = value; } }
        public string VilleDepart { get {  return ville_depart; } set {  ville_depart = value; } }
        public string VilleArrivee { get { return ville_arrivee; } set { ville_arrivee = value; } }
        public decimal Prix { get { return prix; } set { prix = value; } }
    }
}
