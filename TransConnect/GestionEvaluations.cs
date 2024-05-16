using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class GestionEvaluations
    {
        private List<EvaluationChauffeur> evaluations = new List<EvaluationChauffeur>();

        public GestionEvaluations()
        {
            evaluations = ChargerEvaluations();
        }

        public void AjouterEvaluation(EvaluationChauffeur evaluation)
        {
            evaluations.Add(evaluation);
            SauvegarderEvaluations();
        }

        public void SauvegarderEvaluations()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            string jsonData = JsonConvert.SerializeObject(evaluations, settings);
            File.WriteAllText("evaluations.json", jsonData);
        }

        public List<EvaluationChauffeur> ChargerEvaluations()
        {
            if (File.Exists("evaluations.json"))
            {
                string jsonData = File.ReadAllText("evaluations.json");
                return JsonConvert.DeserializeObject<List<EvaluationChauffeur>>(jsonData) ?? new List<EvaluationChauffeur>();
            }
            return new List<EvaluationChauffeur>();
        }
    }

}
