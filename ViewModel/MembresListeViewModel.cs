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
    class MembresListeViewModel: INotifyPropertyChanged
    {
        private string vw_Nom;
        public string Vw_Nom
        {
            get { return vw_Nom; }
            set
            {
                vw_Nom = value;
                OnPropertyChanged("Vw_Nom");
            }
        }
        private string vw_Creation;
        public string Vw_Creation
        {
            get { return vw_Creation; }
            set
            {
                vw_Creation = value;
                OnPropertyChanged("Vw_Creation");
            }
        }
        private string vw_Dernier_Logon;
        public string Vw_Dernier_Logon
        {
            get { return vw_Dernier_Logon; }
            set
            {
                vw_Dernier_Logon = value;
                OnPropertyChanged("Vw_Dernier_Logon");
            }
        }

        private ObservableCollection<Membre> sommaireMembres;
        public ObservableCollection<Membre> SommaireMembres
        {
            get { return sommaireMembres; }
            set
            {
                sommaireMembres = value;
                OnPropertyChanged("SommaireMembres");
            }
        }

        private Membre membreSelectionne;
        public Membre MembreSelectionne
        {
            get
            {
                return membreSelectionne;
            }
            set
            {
                if (value == null)
                    membreSelectionne = null;
                else
                {
                    membreSelectionne = value;
                    Vw_Nom = membreSelectionne.Nom;
                    Vw_Creation = membreSelectionne.Creation;
                    Vw_Dernier_Logon = membreSelectionne.Dernier_Logon;

                    OnPropertyChanged("MembreSelectionne");
                }
            }
        }


        //public ICommand CmdNeoTournoi { get; set; }
        //public ICommand CmdSuppTournoi { get; set; }
        public ICommand CmdToutEnreg { get; set; }


        //private void ActionNeoTournoi(object param)
        //{
        //    Tournois Tournoi = new Tournois
        //    {
        //        Numero = Int32.Parse(Vw_Numero),
        //        Debut = Vw_Debut,
        //        Gagnant = Vw_Gagnant
        //    };


        //    SommaireTournois.Add(Tournoi);
        //}

        private void ActionToutEnreg(object param)
        {
            iMembreRepo repo = new MembreRepo();
            repo.Tronquer();

            foreach (Membre c in SommaireMembres)
            {
                repo.Inserer(c);
            }
        }

        //private void ActionSuppTournoi(object param)
        //{
        //    SommaireTournois.Remove(TournoiSelectionne);
        //}


        public MembresListeViewModel()
        {
            iMembreRepo repo = new MembreRepo();

            SommaireMembres = repo.RecupTousMembres();

            //CmdNeoTournoi = new Command(ActionNeoTournoi);
            //CmdSuppTournoi = new Command(ActionSuppTournoi);
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
