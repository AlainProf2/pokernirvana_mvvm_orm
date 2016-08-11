using PokerNirvana_MVVM_ORM.Model;
using PokerNirvana_MVVM_ORM.ViewModel.ORM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokerNirvana_MVVM_ORM.ViewModel
{
    class TournoisListeViewModel : INotifyPropertyChanged
    {
        private string vw_numero;
        public string Vw_Numero
        {
            get { return vw_numero; }
            set
            {
                vw_numero = value;
                OnPropertyChanged("vw_numero");
            }
        }
        private string vw_debut;
        public string Vw_Debut
        {
            get { return vw_debut; }
            set
            {
                vw_debut = value;
                OnPropertyChanged("Vw_Debut");
            }
        }
        private string vw_gagnant;
        public string Vw_Gagnant
        {
            get { return vw_gagnant; }
            set
            {
                vw_gagnant = value;
                OnPropertyChanged("Vw_Gagnant");
            }
        }

        private ObservableCollection<Tournois> sommaireTournois;
        public ObservableCollection<Tournois> SommaireTournois
        {
            get { return sommaireTournois; }
            set
            {
                sommaireTournois = value;
                OnPropertyChanged("SommaireTournois");
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
            TournoisRepo repo = new TournoisRepo();
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
            TournoisRepo repo = new TournoisRepo();

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
