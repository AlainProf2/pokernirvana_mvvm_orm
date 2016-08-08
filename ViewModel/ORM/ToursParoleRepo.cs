using NHibernate;
using PokerNirvana_MVVM_ORM.Model;

namespace PokerNirvana_MVVM_ORM.ViewModel.ORM
{
    class ToursParoleRepo
    {
        public void Inserer(ToursParole p)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                session.Save(p);
                trx.Commit();
            }
        }

        public void MAJ(PartieActive pa)
        {
            ToursParole p = new ToursParole(pa);
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                session.Update(p);
                trx.Commit();
            }
        }
    }
}
