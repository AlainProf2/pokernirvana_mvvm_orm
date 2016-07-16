using NHibernate;
using PokerNirvana_MVVM_ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerNirvana_MVVM_ORM.ViewModel.ORM
{
    class JoueurRepo
    {

        void MAJ(Joueur c)
        {}

        public List<JoueurPartie> RecupJoueursDunePartie(int p)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                IQuery sel = session.CreateQuery("from JoueurPartie where Numero_Partie = " + p);
                List<JoueurPartie> ListeJoueurs = new List<JoueurPartie>(sel.List<JoueurPartie>());

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
