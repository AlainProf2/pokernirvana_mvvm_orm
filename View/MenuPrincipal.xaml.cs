using PokerNirvana_MVVM_ORM.ViewModel.Service;
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

namespace PokerNirvana_MVVM_ORM.View
{
    /// <summary>
    /// Logique d'interaction pour MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : UserControl
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void Reprise(object sender, RoutedEventArgs e)
        {
            ServiceFactory.Instance.GetService<IApplicationService>().ChangerVue(new Reprise());
        }

        private void CreationTournoisParties(object sender, RoutedEventArgs e)
        {
            ServiceFactory.Instance.GetService<IApplicationService>().ChangerVue(new CreerTournoisParties());
        }

        private void Connexion(object sender, RoutedEventArgs e)
        {
            ServiceFactory.Instance.GetService<IApplicationService>().ChangerVue(new Connexion());
        }

        private void Jouer(object sender, RoutedEventArgs e)
        {
            ServiceFactory.Instance.GetService<IApplicationService>().ChangerVue(new TexasTable());
        }

        private void InformationMembre(object sender, RoutedEventArgs e)
        {
            ServiceFactory.Instance.GetService<IApplicationService>().ChangerVue(new InfoMembre());
        }

        private void Statistiques(object sender, RoutedEventArgs e)
        {
            ServiceFactory.Instance.GetService<IApplicationService>().ChangerVue(new Statistiques());
        }
        private void TournoisListe(object sender, RoutedEventArgs e)
        {
            ServiceFactory.Instance.GetService<IApplicationService>().ChangerVue(new TournoisListe());
        }

        private void TournoisParties(object sender, RoutedEventArgs e)
        {
            ServiceFactory.Instance.GetService<IApplicationService>().ChangerVue(new TournoisParties());
        }

     
    }
}
