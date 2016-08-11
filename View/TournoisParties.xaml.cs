using PokerNirvana_MVVM_ORM.Model;
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
    /// Logique d'interaction pour TournoisParties.xaml
    /// </summary>
    public partial class TournoisParties : UserControl
    {
        public TournoisParties()
        {
            InitializeComponent();
            DataContext = new PokerNirvana_MVVM_ORM.ViewModel.TournoisPartiesViewModel();
        }

        private void OuvrirPartie(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)e.OriginalSource;
            TG.PA.Numero = Convert.ToInt32(cmd.Content);

            ServiceFactory.Instance.GetService<IApplicationService>().ChangerVue(new TexasTable());
        }
    }
}
