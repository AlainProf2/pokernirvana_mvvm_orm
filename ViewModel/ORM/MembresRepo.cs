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
    class MembreRepo : iMembreRepo
    {
        public void Tronquer()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                session.CreateQuery("delete Membre").ExecuteUpdate();
                trx.Commit();
            }
        }
        public void Inserer(Membre m)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                session.Save(m);
                trx.Commit();
            }
        }
        public void MAJ(Membre m)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                session.Update(m);
                trx.Commit();

            }
        }
        public void Supprimer(Membre m)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                session.Delete(m);
                trx.Commit();
            }
        }
        public ObservableCollection<Membre> RecupTousMembres()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                IQuery sel = session.CreateQuery("from Membre");
                ObservableCollection<Membre> ListeARetourner = new ObservableCollection<Membre>(sel.List<Membre>());
                            
                trx.Commit();
                return ListeARetourner;
            }
        }

        public Membre RecupUn(string n)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction trx = session.BeginTransaction())
            {
                IQuery sel = session.CreateQuery("from Membre where Nom = '" + n + "'");
                List<Membre> ListeMbr = new List<Membre>(sel.List<Membre>());
                
                Membre MembreARetourner = ListeMbr[0];
                            
                trx.Commit();
                return MembreARetourner;
            }
        }

        
    }
}
