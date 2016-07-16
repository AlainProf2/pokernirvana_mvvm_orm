using NHibernate;
using PokerNirvana_MVVM_ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerNirvana_MVVM_ORM.ViewModel.ORM
{
    class MainRepo : iMainRepo
    {
        public void Inserer(uneMain m)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                session.Save(m);
                trx.Commit();
            }
        }

        public void MAJ(uneMain m)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                session.Update(m);
                trx.Commit();

            }
        }

        public uneMain RecupUneMain(int NumPartie, int NumMain)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                IQuery sel = session.CreateQuery("from uneMain where Num_Partie = " + NumPartie + " and Num_Main = " + NumMain);
                List<uneMain> ListeMains = new List<uneMain>(sel.List<uneMain>());
                return ListeMains[0];
            }
        }
    }
}
