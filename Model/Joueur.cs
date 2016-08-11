using PokerNirvana_MVVM_ORM;
using System;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace PokerNirvana_MVVM_ORM.Model
{

    public class Joueur : INotifyPropertyChanged
    {
        public Joueur()
        {

        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }


        //private BDService MaBd = new BDService();
        private int position;

        private string pokerman;
        virtual public string Pokerman
        {
            get { return pokerman; }
            set
            {
                pokerman = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Pokerman"));
            }
        }

        private string creation;
        virtual public string Creation
        {
            get { return creation; }
            set
            {
                creation = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Creation"));
            }
        }

        private string dernier_Logon;
        virtual public string Dernier_Logon
        {
            get { return dernier_Logon; }
            set
            {
                dernier_Logon = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Dernier_Logon"));
            }
        }


        private string nbr_Logon;
        virtual public string Nbr_Logon
        {
            get { return nbr_Logon; }
            set
            {
                nbr_Logon = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Nbr_Logon"));
            }
        }

        private string courriel;
        virtual public string Courriel
        {
            get { return courriel; }
            set
            {
                courriel = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Courriel"));
            }
        }

        private int capital;
        private BitmapImage imagePokerman;
        private int engagement;
        private int valeurMainCourante;

        public int Capital
        {
            get { return capital; }
            set
            {
                capital = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Capital"));
            }
        }

        public int Engagement
        {
            get { return engagement; }
            set
            {
                engagement = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Engagement"));
            }
        }

       
        public BitmapImage ImagePokerman
        {
            get { return imagePokerman; }
            set
            {
                imagePokerman = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ImagePokerMan"));
            }
        }

        public int Position
        {
            get { return position; }
            set { position = value; }
        }


        private string decision;
        public string Decision
        {
            get { return decision; }
            set { decision = value; }
        }

        private string datedec;
        public string DateDec
        {
            get { return datedec; }
            set { datedec = value; }
        }

        public int ValeurMainCourante
        {
            get { return valeurMainCourante; }
            set { valeurMainCourante = value; }
        }

        public Joueur(int pos, string n, int cap, string cour, int eng, string dec)
        {
            Position = pos;
            Pokerman = n;
            Capital = cap;
            Courriel = cour;
            Decision = dec;
            Engagement = 0;
            ImagePokerman = new BitmapImage(new Uri(TG.PathImage + Pokerman + ".jpg"));

            if (Capital + eng == 0)
            {
                if (Decision != "PETIT_BLIND" && Decision != "GROS_BLIND")
                {
                    Decision = "MORT";
                    Uri monUri = new Uri(TG.PathImage + "galerie/Mort.jpg");
                    ImagePokerman = new BitmapImage(monUri);
                }
            }
        }

        // Cette méthode est nécessaire pour les tests unitaires
        //public void Init(int cap, int eng, int val, string dec)
        //{
        //    Capital = cap;
        //    Decision = dec;
        //    Engagement = eng;
        //    ValeurMainCourante = val;
        //}
        //private bool inviter;
        //public bool Inviter
        //{
        //    get { return inviter; }
        //    set
        //    {
        //        inviter = value;
        //    }
        //}

        public Joueur(string n, int c)
        {
            Pokerman = n;
            Capital = c;
            string fic = TG.PathImage + n + ".jpg";
            ImagePokerman = new BitmapImage(new Uri(fic));
        }

    }
}