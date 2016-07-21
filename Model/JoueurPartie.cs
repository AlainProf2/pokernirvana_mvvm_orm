using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PokerNirvana_MVVM_ORM.Model
{
    class JoueurPartie
    {
        public JoueurPartie() 
        {
        }

        virtual public string Pokerman { get; set; }
        virtual public int Numero_Partie { get; set; }
        virtual public int Position { get; set; }
        virtual public int Capital { get; set; }
        virtual public string Etat { get; set; }
        virtual public BitmapImage ImagePokerman { get; set; }
        virtual public BitmapImage ImageCarte0 { get; set; }
        virtual public BitmapImage ImageCarte1 { get; set; }
        virtual public BitmapImage ImageFlop0 { get; set; }
        virtual public BitmapImage ImageFlop1 { get; set; }
        virtual public BitmapImage ImageFlop2 { get; set; }
        virtual public BitmapImage ImageTurn { get; set; }
        virtual public BitmapImage ImageRiver { get; set; }
       
        virtual public void InitImage(uneMain laMain)
        {
            string FicNom = TrousseGlobale.PathImage + "joueurs/" + Pokerman + ".jpg";
            try
            {
                ImagePokerman = new BitmapImage(new Uri(FicNom));
            }
            catch
            {
                ImagePokerman = new BitmapImage(new Uri(TrousseGlobale.PathImage + "joueurs/inconnu.jpg"));
            }
            Carte c0;
            Carte c1;

            Carte f0 = new Carte(laMain.F_C0_V, laMain.F_C0_S);
            Carte f1 = new Carte(laMain.F_C1_V, laMain.F_C1_S);
            Carte f2 = new Carte(laMain.F_C2_V, laMain.F_C2_S);
            Carte t = new Carte(laMain.T_V, laMain.T_S);
            Carte r = new Carte(laMain.R_V, laMain.R_S);
            ImageFlop0 = f0.imgCarte;
            ImageFlop1 = f1.imgCarte;
            ImageFlop2 = f2.imgCarte;
            ImageTurn = t.imgCarte;
            ImageRiver = r.imgCarte;

            switch (Position)
            {
                case (0):
                   c0 = new Carte(laMain.J0_C0_V, laMain.J0_C0_S);
                   c1 = new Carte(laMain.J0_C1_V, laMain.J0_C1_S);
                   break;
                case (1):
                   c0 = new Carte(laMain.J1_C0_V, laMain.J1_C0_S);
                   c1 = new Carte(laMain.J1_C1_V, laMain.J1_C1_S);
                   break;
                case (2):
                   c0 = new Carte(laMain.J2_C0_V, laMain.J2_C0_S);
                   c1 = new Carte(laMain.J2_C1_V, laMain.J2_C1_S);
                   break;
                case (3):
                   c0 = new Carte(laMain.J3_C0_V, laMain.J3_C0_S);
                   c1 = new Carte(laMain.J3_C1_V, laMain.J3_C1_S);
                   break;
                case (4):
                   c0 = new Carte(laMain.J4_C0_V, laMain.J4_C0_S);
                   c1 = new Carte(laMain.J4_C1_V, laMain.J4_C1_S);
                   break;
                case (5):
                   c0 = new Carte(laMain.J5_C0_V, laMain.J5_C0_S);
                   c1 = new Carte(laMain.J5_C1_V, laMain.J5_C1_S);
                   break;
                default:
                   c0 = null;
                   c1 = null;
                   break;
            }
            ImageCarte0 = c0.imgCarte;
            ImageCarte1 = c1.imgCarte;
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
