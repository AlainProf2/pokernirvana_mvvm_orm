using PokerNirvana_MVVM_ORM.Model;
using PokerNirvana_MVVM_ORM.ViewModel.ORM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerNirvana_MVVM_ORM.ViewModel
{
    class TournoisPartiesViewModel : INotifyPropertyChanged
    {
        private string vw_TypeTournoi;
        public string Vw_TypeTournoi
        {
            get { return vw_TypeTournoi; }
            set
            {
                vw_TypeTournoi = value;
                OnPropertyChanged("Vw_TypeTournoi");
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
                    PartiesDuTournoi = new ObservableCollection<Partie>();
                    iPartieRepo PartiesRepo = new PartieRepo();
                    PartiesDuTournoi = PartiesRepo.RecupPartiesDunTournoi(tournoiselectionne.Numero);
                    
                    Vw_TypeTournoi = tournoiselectionne.NbVicRequise.ToString() + " de " + tournoiselectionne.NbPartie.ToString();

                    Adversaires = new ObservableCollection<Membre>();
                    iMembreRepo MbrRepo = new MembreRepo();
                    if (tournoiselectionne.J0 != null)
                        Adversaires.Add(MbrRepo.RecupUn(tournoiselectionne.J0));
                    if (tournoiselectionne.J1 != null)
                        Adversaires.Add(MbrRepo.RecupUn(tournoiselectionne.J1));
                    if (tournoiselectionne.J2 != null)
                        Adversaires.Add(MbrRepo.RecupUn(tournoiselectionne.J2));
                    if (tournoiselectionne.J3 != null)
                        Adversaires.Add(MbrRepo.RecupUn(tournoiselectionne.J3));
                    if (tournoiselectionne.J4 != null)
                        Adversaires.Add(MbrRepo.RecupUn(tournoiselectionne.J4));
                    if (tournoiselectionne.J5 != null)
                        Adversaires.Add(MbrRepo.RecupUn(tournoiselectionne.J5));


             
                    OnPropertyChanged("TournoiSelectionne");
                }
            }
        }



        private ObservableCollection<Membre> adversaires;
        public ObservableCollection<Membre> Adversaires
        {
            get { return adversaires; }
            set
            {
                adversaires = value;
                OnPropertyChanged("Adversaires");
            }
        }
        private ObservableCollection<Partie> partiesDuTournoi;
        public ObservableCollection<Partie> PartiesDuTournoi
        {
            get { return partiesDuTournoi; }
            set
            {
                partiesDuTournoi = value;
                OnPropertyChanged("PartiesDuTournoi");
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

        public TournoisPartiesViewModel()
        {
            iTournoiRepo repo = new TournoisRepo();
            SommaireTournois = repo.RecupTousTournois();
            
             //CmdNeoTournoi = new Command(ActionNeoTournoi);
            //CmdSuppTournoi = new Command(ActionSuppTournoi);
            //CmdToutEnreg = new Command(ActionToutEnreg);
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
