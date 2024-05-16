using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public  class Vehicule
    {

        protected string plaque;
        public double Kilometrage { get; set; }

        public Vehicule(string plaque, double kilometrage)
        {
            this.plaque = plaque;
            this.Kilometrage = kilometrage;
        }

        
    }
}
