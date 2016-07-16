using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PokerNirvana_MVVM_ORM.Model
{
    //---------------------------------------------------
    //
    //---------------------------------------------------
    class Club
    {
       private List<Joueur> listeDesJoueurs;
       public List<Joueur> ListeDesJoueurs
       {
            get { return listeDesJoueurs; }
            set
            {
                listeDesJoueurs = value;
            }
       }
       //---------------------------------------------------
       //
       //---------------------------------------------------

       public Club()
       {

           string sel = "select pokerman from Joueur where Pokerman<>'" + TrousseGlobale.NomJoueurLogue + "'";
           List<string>[] res;
           res = TrousseGlobale.MaBD.Select(sel);

           List<Joueur> lmTmp = new List<Joueur>();
           int i = 0;
           foreach (List<string> J in res )
           {
               lmTmp.Add(new Joueur(res[i][0],0));
               i++;
           }
           ListeDesJoueurs = lmTmp;
       }
       //---------------------------------------------------
       //
       //---------------------------------------------------

       public void Affiche()
       {
           int NbInvites = CompteInvites();
           
           string strLstInv = "";
           for (int i = 0; i < ListeDesJoueurs.Count; i++)
           {
               //if (ListeDesJoueurs[i].Inviter)
               //{
               //    strLstInv += ListeDesJoueurs[i].Pokerman + " et ";
               //}
           }
       }

       //---------------------------------------------------
       //
       //---------------------------------------------------
 
       private int CompteInvites()
       {
           int NbInv = 0;
           for (int i = 0; i < ListeDesJoueurs.Count; i++ )
           {
               //if (ListeDesJoueurs[i].Inviter)
               //{
               //    NbInv++;
               //}
           }
           return NbInv;
       }
        //---------------------------------------------------
        //
        //---------------------------------------------------
       public List<string> ConstruitListeJoueurPartie()
       {
           List<string> lstJoueurInvites = new List<string>();
           //lstJoueurInvites.Add(TrousseGlobale.PokermanJoueurLogue);
           for (int i = 0; i < ListeDesJoueurs.Count; i++)
           {
               //if (ListeDesJoueurs[i].Inviter)
               //{
               //    lstJoueurInvites.Add(ListeDesJoueurs[i].Pokerman);
               //}
           }
           return lstJoueurInvites;
       }
    }
}
