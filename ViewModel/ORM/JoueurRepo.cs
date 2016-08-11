using NHibernate;
using PokerNirvana_MVVM_ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PokerNirvana_MVVM_ORM.ViewModel.ORM
{
    class JoueurRepo
    {

        void MAJ(Joueur c)
        {}

        public List<JoueurPartie> RecupJoueursDunePartie()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                IQuery sel = session.CreateQuery("from JoueurPartie where Numero_Partie = " + TG.PA.Numero + " order by position");
                List<JoueurPartie> ListeJoueurs = new List<JoueurPartie>(sel.List<JoueurPartie>());

                sel = session.CreateQuery("from Etape where Num_Partie = " + TG.PA.Numero + " and num_main= " + TG.PA.Numero_Main);
                    List<Etape> ListeEtapes = new List<Etape>(sel.List<Etape>());
                    TG.PA.ProchainJoueur = ListeEtapes[0].ProchainJoueur;
                    TG.PA.Etape = ListeEtapes[0].NomEtape;
                    TG.PA.Num_Tour = ListeEtapes[0].Num_Tour;

                    sel = session.CreateQuery("from ToursParole where Num_Partie = " + TG.PA.Numero + " and num_main= " + TG.PA.Numero_Main + " and NomEtape = '" + TG.PA.Etape + "' and Num_Tour = " + TG.PA.Num_Tour);
                    List<ToursParole> EngJoueur = new List<ToursParole>(sel.List<ToursParole>());

                    if (ListeJoueurs[0].Pokerman == TG.NomJoueurLogue)
                        TG.JoueurLogue = 0;
                    ListeJoueurs[0].Engagement = EngJoueur[0].Eng_J0;
                    ListeJoueurs[0].Decision = EngJoueur[0].Dec_J0;
                    ListeJoueurs[0].DateDec = EngJoueur[0].Date_J0;

                    if (ListeJoueurs[1].Pokerman == TG.NomJoueurLogue)
                        TG.JoueurLogue = 1;
                    ListeJoueurs[1].Engagement = EngJoueur[0].Eng_J1;
                    ListeJoueurs[1].Decision = EngJoueur[0].Dec_J1;
                    ListeJoueurs[1].DateDec = EngJoueur[0].Date_J1;

                    if (ListeJoueurs.Count > 2)
                    {
                        if (ListeJoueurs[2].Pokerman == TG.NomJoueurLogue)
                            TG.JoueurLogue = 2;
                        ListeJoueurs[2].Engagement = EngJoueur[0].Eng_J2;
                        ListeJoueurs[2].Decision = EngJoueur[0].Dec_J2;
                        ListeJoueurs[2].DateDec = EngJoueur[0].Date_J2;
                    }
                    if (ListeJoueurs.Count > 3)
                    {
                        if (ListeJoueurs[3].Pokerman == TG.NomJoueurLogue)
                            TG.JoueurLogue = 3;

                        ListeJoueurs[3].Engagement = EngJoueur[0].Eng_J3;
                        ListeJoueurs[3].Decision = EngJoueur[0].Dec_J3;
                        ListeJoueurs[3].DateDec = EngJoueur[0].Date_J3;
                    }
                    if (ListeJoueurs.Count > 4)
                    {
                        if (ListeJoueurs[4].Pokerman == TG.NomJoueurLogue)
                            TG.JoueurLogue = 4;

                        ListeJoueurs[4].Engagement = EngJoueur[0].Eng_J4;
                        ListeJoueurs[4].Decision = EngJoueur[0].Dec_J4;
                        //ListeJoueurs[4].DateDec = EngJoueur[0].Date_J4;
                    }
                    if (ListeJoueurs.Count > 5)
                    {
                        if (ListeJoueurs[5].Pokerman == TG.NomJoueurLogue)
                            TG.JoueurLogue = 5;
                        ListeJoueurs[5].Engagement = EngJoueur[0].Eng_J5;
                        ListeJoueurs[5].Decision = EngJoueur[0].Dec_J5;
                        //ListeJoueurs[5].DateDec = EngJoueur[0].Date_J5;
                    }
               
                //catch (Exception e)
                //{
                //    string toto = e.ToString();
                //    MessageBox.Show(toto);
                //}

                trx.Commit();
                return ListeJoueurs;
            }
        }


        Joueur RecupUnJoueur(string Nom)
        {
            return null;
        }
    }
}
