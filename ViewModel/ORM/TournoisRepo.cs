using PokerNirvana_MVVM_ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using System.Collections.ObjectModel;

namespace PokerNirvana_MVVM_ORM.ViewModel.ORM
{
    class TournoisRepo : iTournoiRepo
    {
        public void Tronquer()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                session.CreateQuery("delete Tournois").ExecuteUpdate();
                trx.Commit();
            }
        }
        public void Inserer(Tournois j)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                session.Save(j);
                trx.Commit();
            }
        }
        public void MAJ(Tournois j)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                session.Update(j);
                trx.Commit();

            }
        }
        public void Supprimer(Tournois j)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                session.Delete(j);
                trx.Commit();
            }
        }
        public ObservableCollection<Tournois> RecupTousTournois()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                IQuery sel = session.CreateQuery("from Tournois");
                ObservableCollection<Tournois> ListeARetourner = new ObservableCollection<Tournois>(sel.List<Tournois>());
                            
                trx.Commit();
                return ListeARetourner;
            }
        }
    }

    public class ListeTournois : System.Collections.ObjectModel.ObservableCollection<Tournois>
    {
        public ListeTournois()
        { }
    }
}
