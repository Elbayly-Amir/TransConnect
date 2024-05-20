using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Reclamation
    {
        public string NumeroSSClient { get; set; }
        public string Description { get; set; }
        public DateTime DateReclamation { get; set; }

        public Reclamation(string numeroSSClient, string description, DateTime dateReclamation)
        {
            NumeroSSClient = numeroSSClient;
            Description = description;
            DateReclamation = dateReclamation;
        }

        public void AfficherDetails()
        {
            Console.WriteLine($"Client: {NumeroSSClient}, Description: {Description}, Date: {DateReclamation}");
        }
    }
}
