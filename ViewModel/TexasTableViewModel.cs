using PokerNirvana_MVVM_ORM.Model;
using PokerNirvana_MVVM_ORM.View;
using PokerNirvana_MVVM_ORM.ViewModel.ORM;
using PokerNirvana_MVVM_ORM.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace PokerNirvana_MVVM_ORM.ViewModel
{
    class TexasTableViewModel : INotifyPropertyChanged
    {
        public ICommand cmdSuivre { get; set; }
        public ICommand cmdRelancer { get; set; }
        public ICommand cmdAbandonner { get; set; }


        private string titre;
        public string Titre
        {
            get { return titre; }
            set { titre = value; }
        }

        private JoueurPartie joueurA;
        public JoueurPartie JoueurA
        {
            get { return joueurA; }
            set { joueurA = value; }
        }

        private uneMain mainA;
        private uneMain MainA
        {
            get { return mainA; }
            set { mainA = value; }
        }

        private JoueurPartie joueurB;
        public JoueurPartie JoueurB
        {
            get { return joueurB; }
            set { joueurB = value; }
        }
        private JoueurPartie joueurC;
        public JoueurPartie JoueurC
        {
            get { return joueurC; }
            set { joueurC = value; }
        }
        private JoueurPartie joueurD;
        public JoueurPartie JoueurD
        {
            get { return joueurD; }
            set { joueurD = value; }
        }
        private JoueurPartie joueurE;
        public JoueurPartie JoueurE
        {
            get { return joueurE; }
            set { joueurE = value; }
        }
        private JoueurPartie joueurF;
        public JoueurPartie JoueurF
        {
            get { return joueurF; }
            set { joueurF = value; }
        }

        private string msghistorique;
        public string MsgHistorique
        {
            get { return msghistorique; }
            set { msghistorique = value; }
        }

        

        //private List<JoueurPartie> lesJoueurs;
        //public List<JoueurPartie> LesJoueurs
        //{
        //    get { return lesJoueurs; }
        //    set
        //    {
        //        lesJoueurs = value;
        //    }
        //}
   
        static private PartieActive partieCourante;

        private DispatcherTimer delai = new DispatcherTimer();
        bool NouvellePartie = true;


        private void Abandonner(object param)
        {
            TG.EtatDuJoueur = "";
            changeEtat();

            partieCourante.Joueurs[TG.PosJoueurLogue].Decision = "ABANDONNER";
            ToursParoleRepo TPR = new ToursParoleRepo();
            TPR.MAJ(partieCourante);
            int[] TabEng = new int[6];
            string[] TabDec = new string[6];
            for (int i = 0; i < partieCourante.Joueurs.Count; i++)
                TabDec[i] = partieCourante.Joueurs[i].Decision;
            for (int i = 0; i < partieCourante.Joueurs.Count; i++)
                TabEng[i] = partieCourante.Joueurs[i].Engagement;
            partieCourante.croupier = new Croupier(TabDec, TabEng, partieCourante.NiveauPourSuivre, TG.PosJoueurLogue, partieCourante.Etape, partieCourante.Bouton);

            partieCourante.croupier.DetermineProchainJoueur("");
      
            ServiceFactory.Instance.GetService<IApplicationService>().ChangerVue(new TexasTable());
            //TG.Relance = Convert.ToInt32(CB_ValRelance.Text);

            // partieCourante.AppliqueDecision(TG.NomJoueurLogue,
            //                                 TG.PosJoueurLogue,
            //                                 Decision);
            //partieCourante.ConsequenceDeLaDecision("Texas");
            //partieCourante.Joue("", "Texas");
        }

        public TexasTableViewModel()
        {
            cmdAbandonner = new Command(Abandonner);
            if (TG.NomJoueurLogue == null)
                TG.NomJoueurLogue = "pough";
            
            recupEtatJoueur();
            traiteDelai();

            if (TG.Contexte == "RECHARGE_PARTIE_EN_COURS")
            {
                NouvellePartie = false;
                iPartieRepo partieRepo = new PartieRepo();
                partieCourante =partieRepo.RecupUnePartie((int)TG.NumPartie);
                Titre = "Poker Nirvana, Partie " + partieCourante.Numero + ", main " + partieCourante.Numero_Main + ". Joueur: " + TG.NomJoueurLogue;

                iMainRepo mainRepo = new MainRepo();
                uneMain mainCourante = mainRepo.RecupUneMain(partieCourante.Numero, partieCourante.Numero_Main);
                TG.Bouton = mainCourante.Bouton; 
               

                JoueurRepo joueurRepo =  new JoueurRepo();
                TG.Joueurs = joueurRepo.RecupJoueursDunePartie(partieCourante);
                

                JoueurA = TG.Joueurs[0];
                JoueurA.InitImage(mainCourante);
                
                JoueurB = TG.Joueurs[1];
                JoueurB.InitImage(mainCourante);
                if (TG.Joueurs.Count() > 2)
                {
                    JoueurC = TG.Joueurs[2];
                    JoueurC.InitImage(mainCourante);
                }
                if (TG.Joueurs.Count() > 3)
                {
                    JoueurD = TG.Joueurs[3];
                    JoueurD.InitImage(mainCourante);
                }
                if (TG.Joueurs.Count() > 4)
                {
                    JoueurE = TG.Joueurs[4];
                    JoueurE.InitImage(mainCourante);
                }
                if (TG.Joueurs.Count() > 5)
                {
                    JoueurF = TG.Joueurs[5];
                    JoueurF.InitImage(mainCourante);
                }

                HistoriqueRepo HistoRepo = new HistoriqueRepo();
                List<Historique> ListeHisto = HistoRepo.RecupHistoriqueDunePartie(partieCourante.Numero);

                string Desc = "";
                int cmp = ListeHisto.Count;
                foreach (Historique H in ListeHisto)
                {
                    Desc += cmp + "- " + H.Description + "\n";
                    cmp--;
                }
                MsgHistorique = Desc;
                partieCourante = new PartieActive(NouvellePartie, 1973);
               // partieCourante.Joue("", "Texas");
            }
        }


        //-------------------------------------------
        //	  
        //-------------------------------------------
        private void ActionSynchro(object o, EventArgs e)
        {
            //MessageBox.Show("Evenement de syncho" + TG.DernierRefresh);
            string DernierHistorique = TG.GetDernierHistorique();
            string DernierRef = TG.DernierRefresh;

            DernierHistorique = DernierHistorique.Replace("-", "");
            DernierHistorique = DernierHistorique.Replace(" ", "");
            DernierHistorique = DernierHistorique.Replace(":", "");

            DernierRef = DernierRef.Replace("-", "");
            DernierRef = DernierRef.Replace(" ", "");
            DernierRef = DernierRef.Replace(":", "");

            if (Convert.ToInt64(DernierRef) < Convert.ToInt64(DernierHistorique))
            {
                // Un rafraichissement est nécessaire


                TG.DernierRefresh = TG.GetDernierHistorique();
                //partieCourante.MAJMain();
                //partieCourante.MAJEtape();
                //partieCourante.MAJProchainJoueur();
                //partieCourante.Joue("", "Synchro");
                //partieCourante.Rafraichit();
                //TG.OuvrirEcran(this, "Texas");
            }
        }

        //-------------------------------------------
        //	  
        //-------------------------------------------
        private void recupEtatJoueur()
        {
            TG.EtatDuJoueur = recupEtat();

            if (TG.EtatDuJoueur == "Connecté")
                TG.EtatDuJoueur = "DéjàConnecté";
            if (TG.EtatDuJoueur == "")
                TG.EtatDuJoueur = "Connecté";

            changeEtat();
        }

        //-------------------------------------------
        //	  
        //-------------------------------------------
        private string recupEtat()
        {
            return "";
            
            string sel = "select Etat from JoueurPartie where pokerman = '" +
                     TG.NomJoueurLogue + "' and numero_partie=" + TG.NumPartie;
            List<string>[] res = TG.MaBD.Select(sel);
            return res[0][0];
        }

        //-------------------------------------------
        //	  
        //-------------------------------------------
        private void changeEtat()
        {
            //string upd = "update joueurPartie set Etat='" + TG.EtatDuJoueur +
            //    "' where pokerman = '" +
            //    TG.NomJoueurLogue +
            //    "' and numero_partie=" + TG.NumPartie;
            //TG.MaBD.Update(upd);
        }

        //-------------------------------------------
        //	  
        //-------------------------------------------

        //-------------------------------------------
        //	  
        //-------------------------------------------
        private void traiteDelai()
        {
            delai.Tick += ActionSynchro;
            delai.Interval = TimeSpan.FromSeconds(6);
            //delai.Start();
        }

        private void GestionDistribuer(object sender, RoutedEventArgs e)
        {
            //bout_Distribuer.IsEnabled = false;
            //partieCourante.IncrementeNumeroMain();
            //partieCourante.NouvelleMain();

            //partieCourante.Joue(partieCourante.Etape, "NeoMain");
            //TG.OuvrirEcran(this, "Texas");
        }

        private void GestionPartie(object sender, RoutedEventArgs e)
        {
            delai.Stop();
            //TG.OuvrirEcran(this, "Accueil");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
 
    }
}
