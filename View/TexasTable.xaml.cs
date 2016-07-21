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
            allumeBouton();
        }

        private void allumeBouton()
        {
            Bouton_A.Visibility = Visibility.Collapsed;
            Bouton_B.Visibility = Visibility.Collapsed;
            Bouton_C.Visibility = Visibility.Collapsed;
            Bouton_D.Visibility = Visibility.Collapsed;
            Bouton_E.Visibility = Visibility.Collapsed;
            Bouton_F.Visibility = Visibility.Collapsed;

            switch (TrousseGlobale.Bouton)
            {
                case (0) : 
                Bouton_A.Visibility = Visibility.Visible;
                break;
                case (1) : 
                Bouton_B.Visibility = Visibility.Visible;
                break;
                case (2) : 
                Bouton_C.Visibility = Visibility.Visible;
                break;
                case (3) : 
                Bouton_D.Visibility = Visibility.Visible;
                break;
                case (4) : 
                Bouton_E.Visibility = Visibility.Visible;
                break;
                case (5) : 
                Bouton_F.Visibility = Visibility.Visible;
                break;
            }

        }
    }
}