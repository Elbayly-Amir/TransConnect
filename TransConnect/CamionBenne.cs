using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class CamionBenne : Vehicule
    {
        int nombreBenne;

        public CamionBenne(string plaque, double kilometrage, int nombreBennes) : base(plaque, kilometrage) { 
        
            this.nombreBenne = nombreBennes;
        }
    }
}
