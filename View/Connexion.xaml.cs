using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PokerNirvana_MVVM_ORM.Model;
using PokerNirvana_MVVM_ORM.ViewModel.Service;
using PokerNirvana_MVVM_ORM.View;

namespace PokerNirvana_MVVM_ADO.View
{
    /// <summary>
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : UserControl
    {
        public Connexion()
        {
            InitializeComponent();
        }
        private void ConnecteP(object sender, RoutedEventArgs e)
        {
            Aller335("pough", 0);
        }

        private void Aller335(string pm, int pos)
        {
            TG.NomJoueurLogue = pm;
            TG.JoueurLogue = pos;
            TG.PA = new PartieActive(335);
            
            ServiceFactory.Instance.GetService<IApplicationService>().ChangerVue(new TexasTable());
        }

        private void ConnecteCh(object sender, RoutedEventArgs e)
        {
            Aller335("cheen", 1);
        }
        private void ConnecteCe(object sender, RoutedEventArgs e)
        {
            Aller335("certs", 2);
        }
        private void ConnecteG(object sender, RoutedEventArgs e)
        {
            Aller335("gos", 3);
        }
        private void ConnecteK(object sender, RoutedEventArgs e)
        {
            Aller335("k", 4);
        }
        private void ConnecteS(object sender, RoutedEventArgs e)
        {
            Aller335("speed", 5);
        }
    }
}

