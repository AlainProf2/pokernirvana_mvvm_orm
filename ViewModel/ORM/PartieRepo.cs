using NHibernate;
using PokerNirvana_MVVM_ORM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerNirvana_MVVM_ORM.ViewModel.ORM
{
    class PartieRepo : iPartieRepo
    {
        public void Inserer(Partie p)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                session.Save(p);
                trx.Commit();
            }
        }

        public void MAJ(Partie p)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                session.Update(p);
                trx.Commit();

            }
        }

        public ObservableCollection<Partie> RecupPartiesDunTournoi(int NumTournoi)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                IQuery sel = session.CreateQuery("from Partie where Numero_Tournoi = " + NumTournoi);
                ObservableCollection<Partie> ListeParties = new ObservableCollection<Partie>(sel.List<Partie>());

                trx.Commit();
                return ListeParties;
            }
        }

        public Partie RecupUnePartie(int NumPartie)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                IQuery sel = session.CreateQuery("from Partie where Numero = " + NumPartie);
                List<Partie> ListeParties = new List<Partie>(sel.List<Partie>());
                return ListeParties[0];
            }
        }
    }
}
