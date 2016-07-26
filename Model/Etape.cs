using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PokerNirvana_MVVM_ORM.Model
{
    class Etape
    {
        public Etape()
        {
        }

        virtual public int Num_Partie { get; set; }
        virtual public int Num_Main { get; set; }
        virtual public string NomEtape { get; set; }
        virtual public int ProchainJoueur { get; set; }
        virtual public int Num_Tour { get; set; }
        virtual public DateTime Date_Evenement { get; set; }

        public override bool Equals(object o)
        {
            return (object)this == o;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
