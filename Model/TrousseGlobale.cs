using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PokerNirvana_MVVM_ORM.ViewModel;

namespace PokerNirvana_MVVM_ORM.Model
{
    class TrousseGlobale
    {
        public TrousseGlobale()
        {

        }

        public static string NomJoueurLogue;
        public static int    PosJoueurLogue;
        public static string EtatDuJoueur;
        public static string Contexte;
        public static int Bouton;

        public static string DernierRefresh;

        public static int Relance;
        public static string PathImage = "pack://application:,,,/view/images/";

        public static long NumPartie;

        public static BDService MaBD = new BDService();

        /**************************************
         *
        **************************************/
        public static void OuvrirEcran(Window Parent, string NomEcran)
        {
            Type type = Parent.GetType();
            Assembly assembly = type.Assembly;
            Window win = (Window)assembly.CreateInstance(
                type.Namespace + "." + NomEcran);

            // Show the window.
            Parent.Close();
            win.ShowDialog();
        }

        /**************************************
         *
        **************************************/

        public static string GetDernierHistorique()
        {
            string sel = "select max(date) from historique where numeropartie = " + NumPartie;
            //List<string>[] res = MaBD.Select(sel);
            //if (res[0][0]!= "")
            //   return res[0][0];
            //else
               return "1999-12-31 00:59:59";
        }


        /**************************************
         *
        **************************************/
        public static void GetPosition()
        {
            string sel = "select position from joueurPartie where pokerman = '" + NomJoueurLogue + "' and numero_partie = " + NumPartie;
            //List<string>[] res = MaBD.Select(sel);
            PosJoueurLogue = 0;//Convert.ToInt32(res[0][0]);
        }
        /**************************************
         *
        **************************************/
        public static void AjouteHistorique(string Evenement, int Partie)
        {
            int Longueur = Evenement.Length;
            //string MsgHistoriqueFormate = "";

            //if (Longueur > 22)
            //{
            //    while (Longueur > 22)
            //    {
            //        MsgHistoriqueFormate += Evenement.Substring(0, 22) + "\n";
            //        Evenement = Evenement.Substring(22);
            //        Longueur = Evenement.Length;
            //    }
            //    Evenement = MsgHistoriqueFormate + Evenement;
            //}

            DateTime DRef = DateTime.Now;
            DernierRefresh = DRef.ToString();
            string ins = "insert into historique values (" + Partie + ", null, '" + Evenement + "', '" + DernierRefresh + "')";
            //MaBD.Insert(ins);
        }
        /**************************************
         *
        **************************************/
        public static int maxEntre(int v1, int v2)
        {
           if (v1>v2)
              return v1;
           return v2;	  
        }
       /**************************************
       *
       **************************************/
        public static int minEntre(int v1, int v2)
       {
          if (v1<v2)
             return v1;
         return v2;	  
       }

        /**************************************
        *
        **************************************/
        public static void changeEtat(string E)
        {
            string upd = "update joueurPartie set Etat='" + E + "' where pokerman = '" + TrousseGlobale.NomJoueurLogue + "' and numero_partie=" + TrousseGlobale.NumPartie;
            //MaBD.Update(upd);
        }           

        /**************************************
        *
        **************************************/
        public static string recupEtat(string N)
        {
            string sel = "select Etat from JoueurPartie where pokerman = '" + TrousseGlobale.NomJoueurLogue + "' and numero_partie=" + TrousseGlobale.NumPartie;
        //    List<string>[] res = MaBD.Select(sel);
        //    string Etat = res[0][0];
        //    return Etat; 
            return "";
        }

        /**************************************
        *
        **************************************/
        public static void AfficheHistorique(int NumPartie)
        {
           string req = "select description, date from Historique where NumeroPartie = " + NumPartie + " order by numero_evenement desc";
           //List<string>[] res = MaBD.Select(req);
           //int nbLigne = res.Length;
           //// echo "   
           //     <div id='Historique' style='overflow:auto'>";
   //        int compteur = 0;	  
   //        foreach( res as ligne)
   //{
   //   compteur++;
   //   Desc = ligne[0];
   //   Date = ligne[1];

   //   echo "
   //      <article class='ToolTip' data-tooltip='Date'>nbLigne) Desc<hr></article>";
   //   nbLigne--;
   //   if (compteur>100)
   //      break;
      	  
   //}
   //echo "
   //   </div>";
}



    }
}
