using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerNirvana_MVVM_ORM.Model
{
    public class Membre
    {
        public Membre() {}

        public Membre(string n)
        {
            Nom = n;
            Creation = "1999-12-31 00:00:00";
            Nbr_Logon = 100;
            Dernier_Logon = "1999-12-31 00:00:01";
            Courriel = "patapouf@hotmail.com";
        }
        
        virtual public string Nom { get; set; }
        virtual public string Creation { get; set; }
        virtual public int Nbr_Logon { get; set; }
        virtual public string Dernier_Logon { get; set; }
        virtual public string Courriel { get; set; }
    }
}
