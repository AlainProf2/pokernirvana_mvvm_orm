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
    class HistoriqueRepo : iHistoriqueRepo
    {
       
        public void Inserer(Historique p)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                session.Save(p);
                trx.Commit();
            }
        }

        public void MAJ(Historique p)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                session.Update(p);
                trx.Commit();

            }
        }

        public string RecupHistoriqueDunePartie(int NumPartie)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                IQuery sel = session.CreateQuery("from Historique where NumeroPartie = " + NumPartie);
                List<Historique>ListeHisto = new List<Historique>(sel.List<Historique>());

                trx.Commit();

                string Desc = "";
                foreach(Historique H in ListeHisto)
                {
                    Desc += H.Description + "\n-------\n";
                }
                return Desc;
            }
        }
    }
}
