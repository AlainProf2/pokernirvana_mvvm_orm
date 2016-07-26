using PokerNirvana_MVVM_ORM.Model;
using System.Windows;
using System.Windows.Controls;

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
            eteintJoueursInactifs();
            allumeBouton();
        }

        private void eteintJoueursInactifs()
        {
            int NbJoueurs = TrousseGlobale.Joueurs.Count;
            if (TrousseGlobale.Joueurs[0].Capital == 0)
               J_A.Visibility = Visibility.Collapsed;
            if (TrousseGlobale.Joueurs[1].Capital == 0)
                J_B.Visibility = Visibility.Collapsed;

            if (NbJoueurs > 2)
            {
                if (TrousseGlobale.Joueurs[2].Capital == 0)
                    J_C.Visibility = Visibility.Collapsed;
            }
            else
                J_C.Visibility = Visibility.Collapsed;

            if (NbJoueurs > 3)
            {
                if (TrousseGlobale.Joueurs[3].Capital == 0)
                    J_D.Visibility = Visibility.Collapsed;
            }
            else
                J_D.Visibility = Visibility.Collapsed;

            if (NbJoueurs > 4)
            {
                if (TrousseGlobale.Joueurs[4].Capital == 0)
                    J_E.Visibility = Visibility.Collapsed;
            }
            else
                J_E.Visibility = Visibility.Collapsed;

            if (NbJoueurs > 5)
            {
                if (TrousseGlobale.Joueurs[5].Capital == 0)
                    J_F.Visibility = Visibility.Collapsed;
            }
            else
                J_F.Visibility = Visibility.Collapsed;

          
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