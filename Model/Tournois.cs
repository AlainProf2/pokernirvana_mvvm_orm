using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerNirvana_MVVM_ORM.Model
{
    public class Tournois
    {
        virtual public int Numero { get; set; }
        virtual public string Debut { get; set; }
        virtual public string Fin { get; set; }
        virtual public string Gagnant { get; set; }
        virtual public string J0 { get; set; }
        virtual public string J1 { get; set; }
        virtual public string J2 { get; set; }
        virtual public string J3 { get; set; }
        virtual public string J4 { get; set; }
        virtual public string J5 { get; set; }
        virtual public int J0_Vic { get; set; }
        virtual public int J1_Vic { get; set; }
        virtual public int J2_Vic { get; set; }
        virtual public int J3_Vic { get; set; }
        virtual public int J4_Vic { get; set; }
        virtual public int J5_Vic { get; set; }
        virtual public int NbVicRequise { get; set; }
        virtual public int NbJoeurs { get; set; }
        virtual public int NbPartie { get; set; }
        //virtual public IList<Partie> Parties {get; set;} 

        public Tournois()
        { }

        public Tournois(int i, string d, string f, string g, string j0, string j1, string j2, string j3, string j4, string j5, 
                       int j0_Vic, int j1_Vic, int j2_Vic, int j3_Vic, int j4_Vic, int j5_Vic, int nbVR, int nbJ, int nbP)
        {
        Numero = i;
        Debut=d; 
        Fin =f;
        Gagnant=g; 
        J0 =j0;
        J1 =j1;
        J2 =j2;
        J3 =j3;
        J4 =j4;
        J5 =j5;
        J0_Vic=j0_Vic; 
        J1_Vic =j1_Vic;
        J2_Vic =j2_Vic;
        J3_Vic =j3_Vic;
        J4_Vic =j4_Vic;
        J5_Vic =j5_Vic;
        NbVicRequise =nbVR;
        NbJoeurs =nbJ;
        NbPartie =nbP;
        }
    }
}
