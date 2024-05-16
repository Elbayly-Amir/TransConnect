using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class EvaluationChauffeur
    {
        public Client Client { get; set; }
        public Chauffeur Chauffeur { get; set; }
        public int Note { get; set; }
        public string Commentaire { get; set; }
        public string NumeroSSClient { get; set; }
        public string NumeroSSChauffeur { get; set; }

        public EvaluationChauffeur(Client client, Chauffeur chauffeur, int note, string commentaire)
        {
            Client = client;
            Chauffeur = chauffeur;
            Note = note;
            Commentaire = commentaire;
            NumeroSSClient = client.NumeroSS;
            NumeroSSChauffeur = chauffeur.NumeroSS;
        }
    }

}
