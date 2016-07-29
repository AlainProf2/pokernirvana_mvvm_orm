using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;
using PokerNirvana_MVVM_ORM.ViewModel.Service;
using PokerNirvana_MVVM_ORM.View;
using PokerNirvana_MVVM_ORM.Model;
using System.Windows;
using System.Windows.Controls;
using PokerNirvana_MVVM_ORM.View;

 namespace PokerNirvana_MVVM_ORM
{
	/// <summary>
	/// Logique d'interaction pour Principal.xaml
	/// </summary>
	public partial class Principale : Window, IApplicationService
	{
        public Principale()
        {
            InitializeComponent();
            Configurer();

            presenteurContenu.Content = new MenuPrincipal();
            TG.NumPartie = 335;

            ServiceFactory.Instance.GetService<IApplicationService>().ChangerVue(new TexasTable());


        }

        public void ChangerVue<T>(T vue)
        {
            presenteurContenu.Content = vue as UserControl;
        }		


		/// <summary>
		/// Méthode appellé en asyncrone qui boucle toute les demi-secondes pour verifier s'il y a des changements dans la BD
		/// </summary>
		

		/// <summary>
		/// Charge toutes les ressources du service factory
		/// </summary>
		public void Configurer()
		{
			// Inscription des différents services de l'application dans le ServiceFactory.
            //ServiceFactory.Instance.Register<IRestrictionAlimentaireService, MySqlRestrictionAlimentaireService>(new MySqlRestrictionAlimentaireService());
            //ServiceFactory.Instance.Register<IObjectifService, MySqlObjectifService>(new MySqlObjectifService());
            //ServiceFactory.Instance.Register<IPreferenceService, MySqlPreferenceService>(new MySqlPreferenceService());
            //ServiceFactory.Instance.Register<IAlimentService, MySqlAlimentService>(new MySqlAlimentService());
            //ServiceFactory.Instance.Register<IPlatService, MySqlPlatService>(new MySqlPlatService());
            //ServiceFactory.Instance.Register<ISuiviPlatService, MySqlSuiviPlatService>(new MySqlSuiviPlatService());
            //ServiceFactory.Instance.Register<IMenuService, MySqlMenuService>(new MySqlMenuService());
            //ServiceFactory.Instance.Register<IVersionLogicielService, MySqlVersionLogicielService>(new MySqlVersionLogicielService());
			ServiceFactory.Instance.Register<IApplicationService,  Principale>(this);
            //ServiceFactory.Instance.Register<IMembreService, MySqlMembreService>(new MySqlMembreService());
        }

		private void btnRetour_Click(object sender, RoutedEventArgs e)
		{
			App.Current.MainWindow.ResizeMode = ResizeMode.CanMinimize;
			//App.Current.MainWindow.Width = App.APP_WIDTH;
			//App.Current.MainWindow.Height = App.APP_HEIGHT;
			App.Current.MainWindow.WindowState = WindowState.Normal;
            ServiceFactory.Instance.GetService<IApplicationService>().ChangerVue(new MenuPrincipal());
		}

		/// <summary>
		/// Ouvre la fenêtre des paramètres en modal
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnParam_Click(object sender, RoutedEventArgs e)
		{
			//(new FenetreParametres()).ShowDialog();
		}

		/// <summary>
		/// Ouvre la fenêtre d'aide de l'application en modeless
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAide_Click(object sender, RoutedEventArgs e)
		{
		//	FenetreAide fenetreAide = new FenetreAide(presenteurContenu.Content.GetType().Name);
		//	fenetreAide.Show();
		}

		/// <summary>
		/// Ouvre la fenêtre des infos sur l'application en modal
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAPropos_Click(object sender, RoutedEventArgs e)
		{
			//(new FenetreAPropos()).ShowDialog();
			//ServiceFactory.Instance.GetService<IApplicationService>().ChangerVue(new FenetreAPropos());
		}

		/// <summary>
		/// Lorsque l'application de ferme, il faut fermer le Thread de notif.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_Closing(object sender, CancelEventArgs e)
		{
			//ThreadVerifChangement.Abort();
		}
	}
}
