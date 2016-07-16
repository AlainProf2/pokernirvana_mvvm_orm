using PokerNirvana_MVVM_ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PokerNirvana_MVVM_ORM.View
{
    public partial class TexasTable : UserControl
    {
        //-------------------------------------------
        //	  
        //-------------------------------------------
        public TexasTable()
        {
            InitializeComponent();
            TrousseGlobale.Contexte = "RECHARGE_PARTIE_EN_COURS";
            DataContext = new PokerNirvana_MVVM_ORM.ViewModel.TexasTableViewModel();
        }
    }
}