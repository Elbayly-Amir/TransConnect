using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public abstract class Vehicule
    {

        protected string plaque;
        public double kilometrage;

        public Vehicule(string plaque, double kilometrage)
        {
            this.plaque = plaque;
            this.kilometrage = kilometrage;
        }

        
    }
}
