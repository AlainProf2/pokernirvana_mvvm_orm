/****************************************************/
/*   Auteur:   Alain Martel 
/*   Projet:   PokerTexas, 
/*   Desc:     Un moteur de poker Texas Hold'Hem (conversion c++ à c#)
/*   Création: 2015-juin-19
/*
/*   Fichier:	Paquet.cs
/*   Desc:		Evolution vers pokerNirvana
 * 
 *   Modification
 *   2016-07-05    Adaptation pour PokerNirvana_MVVM_ORM
/***************************************************/
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.ComponentModel;

namespace PokerNirvana_MVVM_ORM.Model
{
    public class Carte
    {
        private int valeur;
        public int Valeur
        {
            get { return valeur; }
            set { valeur = value; }
        }
        private int sorte;
        public int Sorte
        {
            get { return sorte; }
            set { sorte = value; }
        }

        public string NomPhysique;
        public DoubleAnimation posLeft;
        public DoubleAnimation posTop;
        public BitmapImage imgCarte;
        public double positionLeft;
        public double positionTop;

        public Carte()
        {
            Valeur = -1;
            Sorte = -1;
            imgCarte = new BitmapImage(new Uri(TrousseGlobale.PathImage + "Cartes/endos.gif"));
        }

        public Carte(int v, int s)
        {
            Valeur = v;
            Sorte = s;
            NomPhysique = GetNomTextuel();
            if (NomPhysique != "vide_vide.gif")
                imgCarte = new BitmapImage(new Uri(TrousseGlobale.PathImage + "Cartes/" + NomPhysique));
            else
                imgCarte = null;
        }

        public Carte(string v, string s)
        {
            Valeur = Convert.ToInt32(v);
            Sorte = Convert.ToInt32(s);
            string NomTex = GetNomTextuel();
            if (NomTex != "vide_vide")
                imgCarte = new BitmapImage(new Uri(TrousseGlobale.PathImage + "Cartes/" + NomTex));
            else
                imgCarte = null;
        }

        public void CacheImage()
        {
            imgCarte = new BitmapImage(new Uri(TrousseGlobale.PathImage + "Cartes/endos.gif"));
        }


        public void InitPosition(int joueur, int posCarte)
        {
            if (joueur < 3)
                positionLeft = 285 + (joueur * 400) + (posCarte * 25);
            else
                positionLeft = 1085 - (joueur - 3) * 400 + (posCarte * 25);

            if (joueur < 3)
            {
                positionTop = 305;
                if (joueur == 1)
                    positionTop = 145;
            }
            else
            {
                positionTop = 580;
                if (joueur == 4)
                    positionTop = 740;
            }
        }

        public void InitPosition(string Etape, int posCarte)
        {
            if (Etape == "FLOP")
            {
                positionLeft = 450 + (posCarte * 75);
                positionTop = 415;
            }
        }
        public void InitPosition(string Etape)
        {
            int offset;
            offset = 800;
            if (Etape == "TURN")
                offset = 720;

            positionLeft = offset;
            positionTop = 415;
        }

        public void InitAnimation(int joueur, int posCarte, double duree)
        {
            posLeft = new DoubleAnimation();
            if (joueur < 3)
                posLeft.To = 285 + (joueur * 400) + (posCarte * 25);
            else
                posLeft.To = 1085 - ((joueur - 3) * 400) + (posCarte * 25);
            posLeft.Duration = TimeSpan.FromSeconds(duree);

            posTop = new DoubleAnimation();
            if (joueur < 3)
            {
                posTop.To = 305;
                if (joueur == 1)
                    posTop.To = 145;
            }
            else
            {
                posTop.To = 580;
                if (joueur == 4)
                    posTop.To = 740;
            }
            posTop.Duration = TimeSpan.FromSeconds(duree);
        }

        public void InitAnimation(string Etape, int noCarte, double duree)
        {
            if (Etape == "FLOP")
            {
                posLeft = new DoubleAnimation();
                posLeft.To = 450 + (noCarte * 75);
                posLeft.Duration = TimeSpan.FromSeconds(duree);

                posTop = new DoubleAnimation();
                posTop.To = 415;
                posTop.Duration = TimeSpan.FromSeconds(duree);
            }
        }
        public void InitAnimation(string Etape, double duree)
        {
            int offset;
            offset = 800;
            if (Etape == "TURN")
                offset = 720;

            posLeft = new DoubleAnimation();
            posLeft.To = offset;
            posLeft.Duration = TimeSpan.FromSeconds(duree);

            posTop = new DoubleAnimation();
            posTop.To = 415;
            posTop.Duration = TimeSpan.FromSeconds(duree);
        }

        public string GetNomTextuel()
        {
            string s = "", v = "";
            switch (Sorte)
            {
                case -1: s = "vide"; break;
                case 0: s = "Pique"; break;
                case 1: s = "Trefle"; break;
                case 2: s = "Carreau"; break;
                case 3: s = "Coeur"; break;
                default: Console.WriteLine("couleur de carte inexistante: Sorte");
                    break;
            }
            switch (Valeur)
            {
                case -1: v = "vide"; break;
                case 0: v = "Deux"; break;
                case 1: v = "Trois"; break;
                case 2: v = "Quatre"; break;
                case 3: v = "Cinq"; break;
                case 4: v = "Six"; break;
                case 5: v = "Sept"; break;
                case 6: v = "Huit"; break;
                case 7: v = "Neuf"; break;
                case 8: v = "Dix"; break;
                case 9: v = "Valet"; break;
                case 10: v = "Reine"; break;
                case 11: v = "Roi"; break;
                case 12: v = "As"; break;
                default: Console.WriteLine("valeur de carte inexistante:" + Valeur);
                    break;
            }
            return s + "_" + v + ".gif";
        }

        public string afficheTexte()
        {
            string CarteTexte = "";
            switch (Valeur)
            {
                case -1: break;
                case 0: CarteTexte = "2"; break;
                case 1: CarteTexte = "3"; break;
                case 2: CarteTexte = "4"; break;
                case 3: CarteTexte = "5"; break;
                case 4: CarteTexte = "6"; break;
                case 5: CarteTexte = "7"; break;
                case 6: CarteTexte = "8"; break;
                case 7: CarteTexte = "9"; break;
                case 8: CarteTexte = "10"; break;
                case 9: CarteTexte = "J"; break;
                case 10: CarteTexte = "Q"; break;
                case 11: CarteTexte = "K"; break;
                case 12: CarteTexte = "A"; break;
                default: CarteTexte = "valeur de carte inexistante:" + Valeur; break;
            }
            switch (Sorte)
            {
                case -1: break;
                case 0: CarteTexte += " de pique"; break;
                case 1: CarteTexte += " de trèfle"; break;
                case 2: CarteTexte += " de carreau"; break;
                case 3: CarteTexte += " de coeur"; break;
                default: CarteTexte += " couleur de carte inexistante:" + Sorte; break;
            }
            return CarteTexte;
        }
    }

    public class Paquet
    {
        Carte[] cartes = new Carte[52];
        Carte[] cartesBrassees = new Carte[52];
        int indiceCarteCourante;

        public Paquet()
        {
            int i = 0;
            for (int sor = 0; sor < 4; sor++)
            {
                for (int val = 0; val < 13; val++)
                {
                    cartes[i] = new Carte(val, sor);
                    i++;
                }
            }
            indiceCarteCourante = 0;
        }

        public void affiche()
        {
            for (int i = 0; i < cartes.Length; i++)
            {
                cartes[i].afficheTexte();
            }
        }

        public void brasse()
        {
            int[] TabValChoisie = new int[52];
            int AleaTmp = -1;
            int AleaTmpTmp = 0;
            int indice = -1;
            string etat = null;

            for (int i = 0; i < 52; i++) { TabValChoisie[i] = -1; }
            for (int i = 0; i < 52; i++)
            {
                bool TrouveProchaine = false;
                while (!TrouveProchaine)
                {
                    DateTime DTCourant = DateTime.Now;
                    int monTick = (int)DTCourant.Ticks;
                    Random r = new Random(monTick);
                    AleaTmpTmp = r.Next(0, 52);
                    if (AleaTmp != AleaTmpTmp)
                        AleaTmp = AleaTmpTmp;
                    else
                        continue;
                    // Console.Write(" " + AleaTmp + " ");

                    for (int j = 0; j < 52; j++)
                    {
                        if (TabValChoisie[j] == -1)
                        {
                            indice = j;
                            etat = "OK";
                            break;
                        }
                        else
                        {
                            if (TabValChoisie[j] == AleaTmp)
                            {
                                etat = "DEJA_PRIS";
                                break;
                            }
                        }
                    }
                    if (etat == "OK")
                    {
                        TabValChoisie[indice] = AleaTmp;
                        TrouveProchaine = true;
                    }
                }
            }
            for (int i = 0; i < 52; i++)
            {
                cartesBrassees[i] = cartes[TabValChoisie[i]];
            }
        }

        public Carte donneProchaineCarte()
        {
            if (indiceCarteCourante < 52)
                indiceCarteCourante++;
            else
                indiceCarteCourante = 1;
            return cartesBrassees[indiceCarteCourante - 1];
        }
    }

    //public class uneMain : INotifyPropertyChanged
    //{
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    public void OnPropertyChanged(PropertyChangedEventArgs e)
    //    {
    //        if (PropertyChanged != null)
    //            PropertyChanged(this, e);
    //    }

    //    public Carte[] mainOrigine;
    //    public Carte[] mainTriee;
    //    public Evaluateur Eval;

    //    public uneMain(Carte[] lamain)
    //    {
    //        mainTriee = new Carte[7];
    //        for (int i = 0; i < 7; i++)
    //        {
    //            mainOrigine[i] = lamain[i];
    //        }
    //        Trier();
    //        Eval = new Evaluateur();
    //    }

    //    public uneMain()
    //    {
    //        mainOrigine = new Carte[7];
    //        for (int i = 0; i < 7; i++)
    //        {
    //            mainOrigine[i] = new Carte();
    //        }
    //        Eval = new Evaluateur();
    //    }

    //    public uneMain(double duree)
    //    {
    //        mainOrigine = new Carte[7];
    //        for (int i = 0; i < 7; i++)
    //        {
    //            mainOrigine[i] = new Carte();
    //        }
    //        Eval = new Evaluateur();
    //    }

    //    uneMain(Carte c0, Carte c1, Carte c2, Carte c3, Carte c4)
    //    {
    //        mainOrigine[0] = c0;
    //        mainOrigine[1] = c1;
    //        mainOrigine[2] = c2;
    //        mainOrigine[3] = c3;
    //        mainOrigine[4] = c4;
    //    }


    //    public BitmapImage ImageCarte0
    //    {
    //        get { return mainOrigine[0].imgCarte; }
    //        set
    //        {
    //            mainOrigine[0].imgCarte = value;
    //            OnPropertyChanged(new PropertyChangedEventArgs("ImageCarte0"));
    //        }
    //    }
    //    public BitmapImage ImageCarte1
    //    {
    //        get { return mainOrigine[1].imgCarte; }
    //        set
    //        {
    //            mainOrigine[1].imgCarte = value;
    //            OnPropertyChanged(new PropertyChangedEventArgs("ImageCarte1"));
    //        }
    //    }
    //    public BitmapImage ImageFlop0
    //    {
    //        get { return mainOrigine[2].imgCarte; }
    //        set
    //        {
    //            mainOrigine[2].imgCarte = value;
    //            OnPropertyChanged(new PropertyChangedEventArgs("ImageFlop0"));
    //        }
    //    }
    //    public BitmapImage ImageFlop1
    //    {
    //        get { return mainOrigine[3].imgCarte; }
    //        set
    //        {
    //            mainOrigine[3].imgCarte = value;
    //            OnPropertyChanged(new PropertyChangedEventArgs("ImageFlop1"));
    //        }
    //    }
    //    public BitmapImage ImageFlop2
    //    {
    //        get { return mainOrigine[4].imgCarte; }
    //        set
    //        {
    //            mainOrigine[4].imgCarte = value;
    //            OnPropertyChanged(new PropertyChangedEventArgs("ImageFlop2"));
    //        }
    //    }
    //    public BitmapImage ImageTurn
    //    {
    //        get { return mainOrigine[5].imgCarte; }
    //        set
    //        {
    //            mainOrigine[5].imgCarte = value;
    //            OnPropertyChanged(new PropertyChangedEventArgs("ImageTurn"));
    //        }
    //    }
    //    public BitmapImage ImageRiver
    //    {
    //        get { return mainOrigine[6].imgCarte; }
    //        set
    //        {
    //            mainOrigine[6].imgCarte = value;
    //            OnPropertyChanged(new PropertyChangedEventArgs("ImageRiver"));
    //        }
    //    }


    //    public int DonneValeur()
    //    {
    //        int[] v = new int[5];
    //        int[] s = new int[5];
    //        int res = -1;
    //        for (int i = 0; i < 5; i++)
    //        {
    //            v[i] = mainTriee[i].Valeur;
    //            s[i] = mainTriee[i].Sorte;
    //        }
    //        res = Eval.Eval_Seq_Flush(v, s); if (res > 0) { return res; }
    //        res = Eval.Eval_Carre(v, s); if (res > 0) { return res; }
    //        res = Eval.Eval_Full(v, s); if (res > 0) { return res; }
    //        res = Eval.Eval_Flush(v, s); if (res > 0) { return res; }
    //        res = Eval.Eval_Seq(v, s); if (res > 0) { return res; }
    //        res = Eval.Eval_Brelan(v, s); if (res > 0) { return res; }
    //        res = Eval.Eval_Deux_Paires(v, s); if (res > 0) { return res; }
    //        res = Eval.Eval_Paire(v, s); if (res > 0) { return res; }
    //        res = Eval.Eval_Rien(v, s); if (res > 0) { return res; }
    //        return res;
    //    }

    //    public void Trier()
    //    {
    //        mainTriee = new Carte[5];
    //        int LaPlusGrosse = -2;
    //        int IndicePlusGrosse = -1;
    //        int[] ValeurUtilisée = new int[52];

    //        for (int i = 0; i < 5; i++) { ValeurUtilisée[i] = -1; }

    //        for (int i = 0; i < 5; i++)
    //        {
    //            for (int j = 0; j < 5; j++)
    //            {
    //                int val = mainOrigine[j].Valeur;
    //                if (mainOrigine[j].Valeur > LaPlusGrosse)
    //                {
    //                    if (ValeurUtilisée[j] == -1)
    //                    {
    //                        LaPlusGrosse = mainOrigine[j].Valeur;
    //                        IndicePlusGrosse = j;
    //                    }
    //                }
    //            }
    //            ValeurUtilisée[IndicePlusGrosse] = 0;
    //            mainTriee[i] = mainOrigine[IndicePlusGrosse];
    //            LaPlusGrosse = -2;
    //        }
    //    }

    //}
}


