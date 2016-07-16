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
    /// Logique d'interaction pour InfoMembre.xaml
    /// </summary>
    public partial class InfoMembre : UserControl
    {
        public InfoMembre()
        {
            InitializeComponent();
            DataContext = new PokerNirvana_MVVM_ORM.ViewModel.MembresListeViewModel();
        }
    }
}
