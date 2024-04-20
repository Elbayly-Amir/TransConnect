using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class CamionCiterne : Vehicule
    {
        string typeFluide;

        public CamionCiterne(string plaque, double kilometrage, string typeFluide) : base(plaque, kilometrage)
        {
            this.typeFluide = typeFluide;
        }
    }
}
