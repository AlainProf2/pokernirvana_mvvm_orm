using NHibernate;
using PokerNirvana_MVVM_ORM.Model;
using System;
using System.Collections.Generic;

namespace PokerNirvana_MVVM_ORM.ViewModel.ORM
{
    class JoueurRepo
    {

        void MAJ(Joueur c)
        {}

        public List<JoueurPartie> RecupJoueursDunePartie(Partie p)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                IQuery sel = session.CreateQuery("from JoueurPartie where Numero_Partie = " + p.Numero + " order by position");
                List<JoueurPartie> ListeJoueurs = new List<JoueurPartie>(sel.List<JoueurPartie>());
                TG.NumMain = p.Numero_Main;

                try
                {
                    sel = session.CreateQuery("from Etape where Num_Partie = " + p.Numero + " and num_main= " + p.Numero_Main);
                    List<Etape> ListeEtapes = new List<Etape>(sel.List<Etape>());
                    TG.ProchainJoueur = ListeEtapes[0].ProchainJoueur;
                    TG.Etape = ListeEtapes[0].NomEtape;

                    sel = session.CreateQuery("from ToursParole where Num_Partie = " + p.Numero + " and num_main= " + p.Numero_Main + " and NomEtape = '" + ListeEtapes[0].NomEtape + "' and Num_Tour = " + ListeEtapes[0].Num_Tour);
                    List<ToursParole> EngJoueur = new List<ToursParole>(sel.List<ToursParole>());
                    TG.NumTour = EngJoueur[0].Num_Tour;

                    if (ListeJoueurs[0].Pokerman == TG.NomJoueurLogue)
                        TG.PosJoueurLogue = 0;
                    ListeJoueurs[0].Engagement = EngJoueur[0].Eng_J0;
                    ListeJoueurs[0].Decision = EngJoueur[0].Dec_J0;

                    if (ListeJoueurs[1].Pokerman == TG.NomJoueurLogue)
                        TG.PosJoueurLogue = 1;
                    ListeJoueurs[1].Engagement = EngJoueur[0].Eng_J1;
                    ListeJoueurs[1].Decision = EngJoueur[0].Dec_J1;
                    if (ListeJoueurs.Count > 2)
                    {
                        if (ListeJoueurs[2].Pokerman == TG.NomJoueurLogue)
                            TG.PosJoueurLogue = 2;
                        ListeJoueurs[2].Engagement = EngJoueur[0].Eng_J2;
                        ListeJoueurs[2].Decision = EngJoueur[0].Dec_J2;
                    }
                    if (ListeJoueurs.Count > 3)
                    {
                        if (ListeJoueurs[3].Pokerman == TG.NomJoueurLogue)
                            TG.PosJoueurLogue = 3;

                        ListeJoueurs[3].Engagement = EngJoueur[0].Eng_J3;
                        ListeJoueurs[3].Decision = EngJoueur[0].Dec_J3;
                    }
                    if (ListeJoueurs.Count > 4)
                    {
                        if (ListeJoueurs[4].Pokerman == TG.NomJoueurLogue)
                            TG.PosJoueurLogue = 4;

                        ListeJoueurs[4].Engagement = EngJoueur[0].Eng_J4;
                        ListeJoueurs[4].Decision = EngJoueur[0].Dec_J4;
                    }
                    if (ListeJoueurs.Count > 5)
                    {
                        if (ListeJoueurs[5].Pokerman == TG.NomJoueurLogue)
                            TG.PosJoueurLogue = 5;
                        ListeJoueurs[5].Engagement = EngJoueur[0].Eng_J5;
                        ListeJoueurs[5].Decision = EngJoueur[0].Dec_J5;
                    }
                }
                catch (Exception e)
                {
                    string toto = e.ToString();
                }
              
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
