using NHibernate;
using NHibernate.Cfg;
using PokerNirvana_MVVM_ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerNirvana_MVVM_ORM.ViewModel.ORM
{
    public sealed class NHibernateHelper
    {
        private static ISessionFactory sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    var cfg = new Configuration();
                    cfg.Configure();
                    //cfg.AddAssembly(typeof(Tournois).Assembly);
                    sessionFactory = cfg.BuildSessionFactory();
                }
                return sessionFactory;
            }
        }

              
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
