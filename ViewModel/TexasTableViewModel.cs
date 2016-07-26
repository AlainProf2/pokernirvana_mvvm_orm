using PokerNirvana_MVVM_ORM.Model;
using PokerNirvana_MVVM_ORM.ViewModel.ORM;
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
   
        private Partie partieCourante;

        private DispatcherTimer delai = new DispatcherTimer();
        bool NouvellePartie = true;

        public TexasTableViewModel()
        {
            cmdAbandonner = new Command(Abandonner);
            TrousseGlobale.NomJoueurLogue = "Pough";
            
            recupEtatJoueur();
            traiteDelai();

            if (TrousseGlobale.Contexte == "RECHARGE_PARTIE_EN_COURS")
            {
                NouvellePartie = false;
                iPartieRepo partieRepo = new PartieRepo();
                partieCourante =partieRepo.RecupUnePartie((int)TrousseGlobale.NumPartie);
                Titre = "Poker Nirvana, Partie " + partieCourante.Numero + ", main " + partieCourante.Numero_Main + ". Joueur: " + TrousseGlobale.NomJoueurLogue;

                iMainRepo mainRepo = new MainRepo();
                uneMain mainCourante = mainRepo.RecupUneMain(partieCourante.Numero, partieCourante.Numero_Main);
                TrousseGlobale.Bouton = mainCourante.Bouton; 
               

                JoueurRepo joueurRepo =  new JoueurRepo();
                TrousseGlobale.Joueurs = joueurRepo.RecupJoueursDunePartie(partieCourante);
                

                JoueurA = TrousseGlobale.Joueurs[0];
                JoueurA.InitImage(mainCourante);
                
                JoueurB = TrousseGlobale.Joueurs[1];
                JoueurB.InitImage(mainCourante);
                if (TrousseGlobale.Joueurs.Count() > 2)
                {
                    JoueurC = TrousseGlobale.Joueurs[2];
                    JoueurC.InitImage(mainCourante);
                }
                if (TrousseGlobale.Joueurs.Count() > 3)
                {
                    JoueurD = TrousseGlobale.Joueurs[3];
                    JoueurD.InitImage(mainCourante);
                }
                if (TrousseGlobale.Joueurs.Count() > 4)
                {
                    JoueurE = TrousseGlobale.Joueurs[4];
                    JoueurE.InitImage(mainCourante);
                }
                if (TrousseGlobale.Joueurs.Count() > 5)
                {
                    JoueurF = TrousseGlobale.Joueurs[5];
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



                partieCourante = new PartieActive(TrousseGlobale.NomJoueurLogue,
                                         (int)TrousseGlobale.NumPartie,
                                         NouvellePartie,
                                         1973);
               // partieCourante.Joue("", "Texas");
            }
        }


        //-------------------------------------------
        //	  
        //-------------------------------------------
        private void ActionSynchro(object o, EventArgs e)
        {
            //MessageBox.Show("Evenement de syncho" + TrousseGlobale.DernierRefresh);
            string DernierHistorique = TrousseGlobale.GetDernierHistorique();
            string DernierRef = TrousseGlobale.DernierRefresh;

            DernierHistorique = DernierHistorique.Replace("-", "");
            DernierHistorique = DernierHistorique.Replace(" ", "");
            DernierHistorique = DernierHistorique.Replace(":", "");

            DernierRef = DernierRef.Replace("-", "");
            DernierRef = DernierRef.Replace(" ", "");
            DernierRef = DernierRef.Replace(":", "");

            if (Convert.ToInt64(DernierRef) < Convert.ToInt64(DernierHistorique))
            {
                // Un rafraichissement est nécessaire


                TrousseGlobale.DernierRefresh = TrousseGlobale.GetDernierHistorique();
                //partieCourante.MAJMain();
                //partieCourante.MAJEtape();
                //partieCourante.MAJProchainJoueur();
                //partieCourante.Joue("", "Synchro");
                //partieCourante.Rafraichit();
                //TrousseGlobale.OuvrirEcran(this, "Texas");
            }
        }

        //-------------------------------------------
        //	  
        //-------------------------------------------
        private void recupEtatJoueur()
        {
            TrousseGlobale.EtatDuJoueur = recupEtat();

            if (TrousseGlobale.EtatDuJoueur == "Connecté")
                TrousseGlobale.EtatDuJoueur = "DéjàConnecté";
            if (TrousseGlobale.EtatDuJoueur == "")
                TrousseGlobale.EtatDuJoueur = "Connecté";

            changeEtat();
        }

        //-------------------------------------------
        //	  
        //-------------------------------------------
        private string recupEtat()
        {
            return "";
            
            string sel = "select Etat from JoueurPartie where pokerman = '" +
                     TrousseGlobale.NomJoueurLogue + "' and numero_partie=" + TrousseGlobale.NumPartie;
            List<string>[] res = TrousseGlobale.MaBD.Select(sel);
            return res[0][0];
        }

        //-------------------------------------------
        //	  
        //-------------------------------------------
        private void changeEtat()
        {
            string upd = "update joueurPartie set Etat='" + TrousseGlobale.EtatDuJoueur +
                "' where pokerman = '" +
                TrousseGlobale.NomJoueurLogue +
                "' and numero_partie=" + TrousseGlobale.NumPartie;
            TrousseGlobale.MaBD.Update(upd);
        }

        //-------------------------------------------
        //	  
        //-------------------------------------------
        private void Abandonner(object param)
        {
            TrousseGlobale.EtatDuJoueur = "";
            changeEtat();


            //Button cmd = (Button)e.OriginalSource;
            //string Decision = (string)cmd.Content;
            string Decision = "Abandonner";
            //TrousseGlobale.Relance = Convert.ToInt32(CB_ValRelance.Text);

           // partieCourante.AppliqueDecision(TrousseGlobale.NomJoueurLogue,
           //                                 TrousseGlobale.PosJoueurLogue,
           //                                 Decision);
            //partieCourante.ConsequenceDeLaDecision("Texas");
            //partieCourante.Joue("", "Texas");
        }

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
            //TrousseGlobale.OuvrirEcran(this, "Texas");
        }

        private void GestionPartie(object sender, RoutedEventArgs e)
        {
            delai.Stop();
            //TrousseGlobale.OuvrirEcran(this, "Accueil");
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
