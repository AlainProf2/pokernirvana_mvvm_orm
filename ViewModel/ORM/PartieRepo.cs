using NHibernate;
using PokerNirvana_MVVM_ORM.Model;
using System;
using System.Windows;
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

        public void RecupUnePartie(int NumPartie)
        {
            ISession session = NHibernateHelper.OpenSession();
            try
            {
                ITransaction trx = session.BeginTransaction();
                IQuery sel = session.CreateQuery("from Partie where Numero = " + NumPartie);
                List<Partie> ListeParties = new List<Partie>(sel.List<Partie>());
                ActiverPartie(ListeParties[0]);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ActiverPartie(Partie p)
        {
            TG.PA.Numero_Tournoi = p.Numero_Tournoi;
            TG.PA.Debut = p.Debut;
            TG.PA.Numero = p.Numero;
            TG.PA.Numero_Main = p.Numero_Main;
            TG.PA.Nom_J0 = p.Nom_J0;
            TG.PA.Nom_J1 = p.Nom_J1;
            TG.PA.Nom_J2 = p.Nom_J2;
            TG.PA.Nom_J3 = p.Nom_J3;
            TG.PA.Nom_J4 = p.Nom_J4;
            TG.PA.Nom_J5 = p.Nom_J5;
            TG.PA.Perdant_1 = p.Perdant_1;
            TG.PA.Perdant_2 = p.Perdant_2;
            TG.PA.Perdant_3 = p.Perdant_3;
            TG.PA.Perdant_4 = p.Perdant_4;
            TG.PA.Perdant_5 = p.Perdant_5;
            TG.PA.Perdant_1_Date = p.Perdant_1_Date;
            TG.PA.Perdant_2_Date = p.Perdant_2_Date;
            TG.PA.Perdant_3_Date = p.Perdant_3_Date;
            TG.PA.Perdant_4_Date = p.Perdant_4_Date;
            TG.PA.Perdant_5_Date = p.Perdant_5_Date;

      

        }
    }
}
