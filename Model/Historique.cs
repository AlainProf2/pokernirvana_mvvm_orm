using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerNirvana_MVVM_ORM.Model
{
    class Historique
  {
        public Historique() 
        {
        }

        virtual public int Numero_Evenement { get; set; }
        virtual public int NumeroPartie { get; set; }
        virtual public string Description { get; set; }
        virtual public string Date { get; set; }
       
    }
}
