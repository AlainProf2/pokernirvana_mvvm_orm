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
            TG.Contexte = "RECHARGE_PARTIE_EN_COURS";
            DataContext = new PokerNirvana_MVVM_ORM.ViewModel.TexasTableViewModel();
            allumeBouton();
            eteintJoueursInactifs();
            attenteOuAction();
        }
        private void attenteOuAction()
        {
            if (TG.PA.ProchainJoueur == -1 ||
                TG.PA.ProchainJoueur == -2 ||
                TG.PA.ProchainJoueur == -3)
            {
                MessageBox.Show("Etape : " + TG.PA.ProchainJoueur);
                return;
            }

            if (TG.NomJoueurLogue == TG.PA.Joueurs[TG.PA.ProchainJoueur].Pokerman)
            {
                // MessageBox.Show("Afficher les options possibles");
            }
            else
            {
                //                MessageBox.Show("
                bout_Distribuer.Visibility = Visibility.Collapsed;
                bout_Suivre.Visibility = Visibility.Collapsed;
                bout_Abandonner.Visibility = Visibility.Collapsed;
                bout_Relancer.Visibility = Visibility.Collapsed;
                CB_ValRelance.Visibility = Visibility.Collapsed;
                bout_Gestion.Visibility = Visibility.Collapsed;
                bout_Distribuer.Visibility = Visibility.Collapsed;
                TB_MsgAttente.Text = "On attend la décision de " + TG.PA.Joueurs[TG.PA.ProchainJoueur].Pokerman;
            }
        }

        private void eteintJoueursInactifs()
        {
            int NbJoueurs = TG.PA.Joueurs.Count;
            if (TG.PA.Joueurs[0].Decision == "MORT")
                J_A.Visibility = Visibility.Collapsed;
            if (TG.PA.Joueurs[1].Decision == "MORT")
                J_B.Visibility = Visibility.Collapsed;

            if (NbJoueurs > 2)
            {
                if (TG.PA.Joueurs[2].Decision == "MORT")
                    J_C.Visibility = Visibility.Collapsed;
            }
            else
                J_C.Visibility = Visibility.Collapsed;

            if (NbJoueurs > 3)
            {
                if (TG.PA.Joueurs[3].Decision == "MORT")
                    J_D.Visibility = Visibility.Collapsed;
            }
            else
                J_D.Visibility = Visibility.Collapsed;

            if (NbJoueurs > 4)
            {
                if (TG.PA.Joueurs[4].Decision == "MORT")
                    J_E.Visibility = Visibility.Collapsed;
            }
            else
                J_E.Visibility = Visibility.Collapsed;

            if (NbJoueurs > 5)
            {
                if (TG.PA.Joueurs[5].Decision == "MORT")
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

            switch (TG.PA.Bouton)
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