using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class GestionDepenses
    {
        private LinkedList<Depenses> depenses = new LinkedList<Depenses>();

        public LinkedList<Depenses> Depenses
        {
            get { return depenses; }
            set { depenses = value; }
        }

        public GestionDepenses(List<Depenses> depenses)
        {
            this.depenses = new LinkedList<Depenses>(depenses);
        }

        public GestionDepenses()
        {
            depenses = new LinkedList<Depenses>(ChargerDepenses());
        }

        public void AjouterDepense(Depenses depense)
        {
            depenses.AddLast(depense);
            SauvegarderDepenses();
        }

        private List<Depenses> ChargerDepenses()
        {
            if (File.Exists("depenses.json"))
            {
                string jsonData = File.ReadAllText("depenses.json");
                return JsonConvert.DeserializeObject<List<Depenses>>(jsonData) ?? new List<Depenses>();
            }
            return new List<Depenses>();
        }

        public void SauvegarderDepenses()
        {
            string jsonData = JsonConvert.SerializeObject(depenses, Formatting.Indented);
            File.WriteAllText("depenses.json", jsonData);
        }

        public static GestionDepenses LoadFromJson(string jsonData)
        {
            var depenses = JsonConvert.DeserializeObject<List<Depenses>>(jsonData) ?? new List<Depenses>();
            return new GestionDepenses(depenses);
        }
    }
}
