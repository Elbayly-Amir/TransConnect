using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class GestionReclamations
    {
        private LinkedList<Reclamation> reclamations = new LinkedList<Reclamation>();

        public void AjouterReclamation(Reclamation reclamation)
        {
            reclamations.AddLast(reclamation);
        }

        public LinkedList<Reclamation> ListerReclamations()
        {
            return reclamations;
        }

        public void SauvegarderReclamations()
        {
            string jsonData = JsonConvert.SerializeObject(reclamations, Formatting.Indented);
            File.WriteAllText("reclamations.json", jsonData);
        }

        public void ChargerReclamations()
        {
            if (File.Exists("reclamations.json"))
            {
                string jsonData = File.ReadAllText("reclamations.json");
                reclamations = JsonConvert.DeserializeObject<LinkedList<Reclamation>>(jsonData) ?? new LinkedList<Reclamation>();
            }
        }
    }
}
