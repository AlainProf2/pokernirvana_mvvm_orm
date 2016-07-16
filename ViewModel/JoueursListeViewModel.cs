using PokerNirvana_MVVM_ORM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerNirvana_MVVM_ORM.ViewModel
{
    class JoueursListeViewModel: INotifyPropertyChanged
    {
        private string vw_pokerman;
        public string Vw_Pokerman
        {
            get { return vw_pokerman; }
            set
            {
                vw_pokerman = value;
                OnPropertyChanged("Vw_Pokerman");
            }
        }
        private string vw_creation;
        public string Vw_Creation
        {
            get { return vw_creation; }
            set
            {
                vw_creation = value;
                OnPropertyChanged("Vw_Creation");
            }
        }
        private string vw_dernierLogon;
        public string Vw_DernierLogon
        {
            get { return vw_dernierLogon; }
            set
            {
                vw_dernierLogon = value;
                OnPropertyChanged("Vw_DernierLogon");
            }
        }

        private ObservableCollection<Joueur> sommaireJoueurs;
        public ObservableCollection<Joueur> SommaireJoueurs
        {
            get { return sommaireJoueurs; }
            set
            {
                sommaireJoueurs = value;
                OnPropertyChanged("SommaireJoueurs");
            }
        }

        private Tournois tournoiselectionne;
        public Tournois TournoiSelectionne
        {
            get
            {
                return tournoiselectionne;
            }
            set
            {
                if (value == null)
                    tournoiselectionne = null;
                else
                {
                    tournoiselectionne = value;
                    Vw_Numero = tournoiselectionne.Numero.ToString();
                    Vw_Debut = tournoiselectionne.Debut;
                    Vw_Gagnant = tournoiselectionne.Gagnant;

                    OnPropertyChanged("TournoiSelectionne");
                }
            }
        }


        public ICommand CmdNeoTournoi { get; set; }
        public ICommand CmdSuppTournoi { get; set; }
        public ICommand CmdToutEnreg { get; set; }


        private void ActionNeoTournoi(object param)
        {
            Tournois Tournoi = new Tournois
            {
                Numero = Int32.Parse(Vw_Numero),
                Debut = Vw_Debut,
                Gagnant = Vw_Gagnant
            };


            SommaireTournois.Add(Tournoi);
        }

        private void ActionToutEnreg(object param)
        {
            iTournoiRepo repo = new TournoisRepo();
            repo.Tronquer();

            foreach (Tournois c in SommaireTournois)
            {
                c.Numero = 0;
                repo.Inserer(c);
            }
        }

        private void ActionSuppTournoi(object param)
        {
            SommaireTournois.Remove(TournoiSelectionne);
        }


        public TournoisListeViewModel()
        {
            iTournoiRepo repo = new TournoisRepo();

            SommaireTournois = repo.RecupTousTournois();

            CmdNeoTournoi = new Command(ActionNeoTournoi);
            CmdSuppTournoi = new Command(ActionSuppTournoi);
            CmdToutEnreg = new Command(ActionToutEnreg);
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
