using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerNirvana_MVVM_ORM.Model
{
    public class Partie
    {
        virtual public int Numero { get; set; }
        virtual public int Numero_Tournoi { get; set; }
        virtual public int Numero_Main { get; set; }
        virtual public DateTime Debut { get; set; }
        virtual public DateTime Fin { get; set; }
        virtual public string Gagnant { get; set; }
        virtual public string Perdant_5 { get; set; }
        virtual public string Perdant_4 { get; set; }
        virtual public string Perdant_3 { get; set; }
        virtual public string Perdant_2 { get; set; }
        virtual public string Perdant_1 { get; set; }
        virtual public DateTime Perdant_5_Date { get; set; }
        virtual public DateTime Perdant_4_Date { get; set; }
        virtual public DateTime Perdant_3_Date { get; set; }
        virtual public DateTime Perdant_2_Date { get; set; }
        virtual public DateTime Perdant_1_Date { get; set; }
        virtual public string Nom_J0 { get; set; }
        virtual public string Nom_J1 { get; set; }
        virtual public string Nom_J2 { get; set; }
        virtual public string Nom_J3 { get; set; }
        virtual public string Nom_J4 { get; set; }
        virtual public string Nom_J5 { get; set; }
        
        public Partie()
        {

        }

        public Partie(int n)
        {
            Numero = n;
            Numero_Tournoi = 999;
            Numero_Main = 18;
        }
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
