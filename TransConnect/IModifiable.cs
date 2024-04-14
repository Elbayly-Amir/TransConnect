using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public interface IModifiable
    {

        void ModifierInformations(string nouveauNom, string nouvelleAdresse, string nouvelEmail, string nouveauTelephone);
    }
}
