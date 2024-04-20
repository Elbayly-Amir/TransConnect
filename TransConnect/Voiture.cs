using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Voiture : Vehicule
    {

        int nombrePassagers;

        public Voiture(string plaque, double kilometrage, int nombrePassagers)
        : base(plaque, kilometrage)
        {

            this.nombrePassagers = nombrePassagers;
        }

    }
}
