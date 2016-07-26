using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PokerNirvana_MVVM_ORM.Model
{
    class ToursParole
    {
        public ToursParole()
        {
        }

        virtual public int Num_Partie { get; set; }
        virtual public int Num_Main { get; set; }
        virtual public string NomEtape { get; set; }
        virtual public int Num_Tour { get; set; }

        virtual public string Dec_J0 { get; set; }
        virtual public int Eng_J0 { get; set; }
        virtual public DateTime Date_J0 { get; set; }
        virtual public string Dec_J1 { get; set; }
        virtual public int Eng_J1 { get; set; }
        virtual public DateTime Date_J1 { get; set; }
        virtual public string Dec_J2 { get; set; }
        virtual public int Eng_J2 { get; set; }
        virtual public DateTime Date_J2 { get; set; }
        virtual public string Dec_J3 { get; set; }
        virtual public int Eng_J3 { get; set; }
        virtual public DateTime Date_J3 { get; set; }
        virtual public string Dec_J4 { get; set; }
        virtual public int Eng_J4 { get; set; }
        virtual public DateTime Date_J4 { get; set; }
        virtual public string Dec_J5 { get; set; }
        virtual public int Eng_J5 { get; set; }
        virtual public DateTime Date_J5 { get; set; }

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
