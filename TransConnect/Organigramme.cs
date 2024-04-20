using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Organigramme
    {

        Salarie racine;

        public Salarie Racine { get {  return racine; } set { racine = value; } }

        public Organigramme(Salarie racine)
        {
            this.racine = racine;
        }

        public void AjouterSalarieOrganigrame(Salarie salarie, int niveau = 0)
        {
            Console.WriteLine(salarie.Prenom, salarie.Nom, salarie.Poste);
            foreach(var s in salarie.employe)
            {
                AjouterSalarieOrganigrame(salarie, niveau + 1);
            }
        }

        public void AjouterSalarieAuManager( Salarie n, Salarie chef)
        {
            if(chef != null)
            {
                chef.AjouterSalarie(n);
                n.Manager = chef;
            }
        }

        public void SupprimerSalarieOrganigramme(Salarie s)
        {
            if(s.Manager != null)
            {
                s.Manager.SupprimerSalarie(s);
            }
        }
    }
}
