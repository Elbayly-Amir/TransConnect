using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Camionette : Vehicule
    {

        string usage;

        public Camionette(string plaque, double kilometrage, string usage)
       : base(plaque, kilometrage)
        {
            this.usage = usage;
        }
    }
}
