using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PokerNirvana_MVVM_ORM.Model
{
   public class uneMain 
   {
      public uneMain() {        }

      virtual public int Num_Partie { get; set; }
      virtual public int Num_Main { get; set; }
      virtual public int Bouton { get; set; }
      virtual public string Etape { get; set; }
      virtual public string Debut { get; set; }
      virtual public string Fin { get; set; }
      virtual public int NiveauPourSuivre { get; set; }
      virtual public int J0_C0_V { get; set; }
      virtual public int J0_C0_S { get; set; }
      virtual public int J0_C1_V { get; set; }
      virtual public int J0_C1_S { get; set; }
      virtual public int J1_C0_V { get; set; }
      virtual public int J1_C0_S { get; set; }
      virtual public int J1_C1_V { get; set; }
      virtual public int J1_C1_S { get; set; }
      virtual public int J2_C0_V { get; set; }
      virtual public int J2_C0_S { get; set; }
      virtual public int J2_C1_V { get; set; }
      virtual public int J2_C1_S { get; set; }
      virtual public int J3_C0_V { get; set; }
      virtual public int J3_C0_S { get; set; }
      virtual public int J3_C1_V { get; set; }
      virtual public int J3_C1_S { get; set; }
      virtual public int J4_C0_V { get; set; }
      virtual public int J4_C0_S { get; set; }
      virtual public int J4_C1_V { get; set; }
      virtual public int J4_C1_S { get; set; }
      virtual public int J5_C0_V { get; set; }
      virtual public int J5_C0_S { get; set; }
      virtual public int J5_C1_V { get; set; }
      virtual public int J5_C1_S { get; set; }
      virtual public int F_C0_V { get; set; }
      virtual public int F_C0_S { get; set; }
      virtual public int F_C1_V { get; set; }
      virtual public int F_C1_S { get; set; }
      virtual public int F_C2_V { get; set; }
      virtual public int F_C2_S { get; set; }
      virtual public int T_V { get; set; }
      virtual public int T_S { get; set; }
      virtual public int R_V { get; set; }
      virtual public int R_S { get; set; }
      virtual public int Valeur_J0 { get; set; }
      virtual public int Valeur_J1 { get; set; }
      virtual public int Valeur_J2 { get; set; }
      virtual public int Valeur_J3 { get; set; }
      virtual public int Valeur_J4 { get; set; }
      virtual public int Valeur_J5 { get; set; }
      virtual public string Gagnant { get; set; }

       
       public override bool Equals(object o)
       {
           return (object)this == o;
       }

       public override int GetHashCode()
       {
           return 0;
       }
       virtual public void InitImage()
       {
           //string FicNom = TrousseGlobale.PathImage + "Cartes/carreau_as.gif";
           string FicNom = "C:\\Users\\alain\\Dropbox\\Aut2016\\5B6\\application\\PokerNirvana_MVVM_ORM\\View\\Images\\Cartes\\carreau_as.gif";
           ImageCarte0 = new BitmapImage(new Uri(FicNom));

           FicNom = TrousseGlobale.PathImage + "Cartes/carreau_as.gif";
           ImageCarte1 = new BitmapImage(new Uri(FicNom));

           ImageFlop0 = new BitmapImage(new Uri(FicNom));
           ImageFlop1 = new BitmapImage(new Uri(FicNom));
           ImageFlop2 = new BitmapImage(new Uri(FicNom));
           ImageTurn = new BitmapImage(new Uri(FicNom));
           ImageRiver = new BitmapImage(new Uri(FicNom));
       }
       virtual public Carte[] mainOrigine { get; set; }
       virtual public Carte[] mainTriee { get; set; }
       virtual public Evaluateur Eval { get; set; }

       public uneMain(Carte[] lamain)
       {
           mainTriee = new Carte[7];
           for (int i = 0; i < 7; i++)
           {
               mainOrigine[i] = lamain[i];
           }
           Trier();
           Eval = new Evaluateur();
       }

      
       public uneMain(double duree)
       {
           mainOrigine = new Carte[7];
           for (int i = 0; i < 7; i++)
           {
               mainOrigine[i] = new Carte();
           }
           Eval = new Evaluateur();
       }

       uneMain(Carte c0, Carte c1, Carte c2, Carte c3, Carte c4)
       {
           mainOrigine[0] = c0;
           mainOrigine[1] = c1;
           mainOrigine[2] = c2;
           mainOrigine[3] = c3;
           mainOrigine[4] = c4;
       }


       virtual public BitmapImage ImageCarte0{ get; set;}
          
       virtual public BitmapImage ImageCarte1{ get; set;}
          
     
       virtual public BitmapImage ImageFlop0{ get; set;}
      
       virtual public BitmapImage ImageFlop1{ get; set;}
      
       virtual public BitmapImage ImageFlop2{ get; set;}
      
       virtual public BitmapImage ImageTurn{ get; set;}
      
       virtual public BitmapImage ImageRiver{ get; set;}
       

       virtual public int DonneValeur()
       {
           int[] v = new int[5];
           int[] s = new int[5];
           int res = -1;
           for (int i = 0; i < 5; i++)
           {
               v[i] = mainTriee[i].Valeur;
               s[i] = mainTriee[i].Sorte;
           }
           res = Eval.Eval_Seq_Flush(v, s); if (res > 0) { return res; }
           res = Eval.Eval_Carre(v, s); if (res > 0) { return res; }
           res = Eval.Eval_Full(v, s); if (res > 0) { return res; }
           res = Eval.Eval_Flush(v, s); if (res > 0) { return res; }
           res = Eval.Eval_Seq(v, s); if (res > 0) { return res; }
           res = Eval.Eval_Brelan(v, s); if (res > 0) { return res; }
           res = Eval.Eval_Deux_Paires(v, s); if (res > 0) { return res; }
           res = Eval.Eval_Paire(v, s); if (res > 0) { return res; }
           res = Eval.Eval_Rien(v, s); if (res > 0) { return res; }
           return res;
       }

       virtual public void Trier()
       {
           mainTriee = new Carte[5];
           int LaPlusGrosse = -2;
           int IndicePlusGrosse = -1;
           int[] ValeurUtilisée = new int[52];

           for (int i = 0; i < 5; i++) { ValeurUtilisée[i] = -1; }

           for (int i = 0; i < 5; i++)
           {
               for (int j = 0; j < 5; j++)
               {
                   int val = mainOrigine[j].Valeur;
                   if (mainOrigine[j].Valeur > LaPlusGrosse)
                   {
                       if (ValeurUtilisée[j] == -1)
                       {
                           LaPlusGrosse = mainOrigine[j].Valeur;
                           IndicePlusGrosse = j;
                       }
                   }
               }
               ValeurUtilisée[IndicePlusGrosse] = 0;
               mainTriee[i] = mainOrigine[IndicePlusGrosse];
               LaPlusGrosse = -2;
           }
       }

   }
}
