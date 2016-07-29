﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PokerNirvana_MVVM_ORM.Model
{
    class ToursParole
    {
        public ToursParole()
        {
         
        }

        public ToursParole(bool Flag)
        {
            Num_Partie = TG.NumPartie;
            Num_Main = TG.NumMain;
            NomEtape = TG.Etape;
            Num_Tour = TG.NumTour;

            Dec_J0 = TG.Joueurs[0].Decision;
            Eng_J0 = TG.Joueurs[0].Engagement;
            Date_J0 = DateTime.Now;
            Dec_J1 = TG.Joueurs[1].Decision;
            Eng_J1 = TG.Joueurs[1].Engagement;
            Date_J1 = DateTime.Now;
            if (TG.Joueurs.Count > 2)
            {
                Dec_J2 = TG.Joueurs[2].Decision;
                Eng_J2 = TG.Joueurs[2].Engagement;
                Date_J2 = DateTime.Now;
            }
            else
            {
                Dec_J2 = "MORT";
                Eng_J2 = 0;
                Date_J2 = new DateTime(1999,12,31);
            }
            if (TG.Joueurs.Count > 3)
            {
                Dec_J3 = TG.Joueurs[3].Decision;
                Eng_J3 = TG.Joueurs[3].Engagement;
                Date_J3 = DateTime.Now;
            }
            else
            {
                Dec_J3 = "MORT";
                Eng_J3 = 0;
                Date_J3 = new DateTime(1999, 12, 31);
            }
            if (TG.Joueurs.Count > 4)
            {
                Dec_J4 = TG.Joueurs[4].Decision;
                Eng_J4 = TG.Joueurs[4].Engagement;
                Date_J4 = DateTime.Now;
            }
            else
            {
                Dec_J4 = "MORT";
                Eng_J4 = 0;
                Date_J4 = new DateTime(1999, 12, 31);
            }
            if (TG.Joueurs.Count > 5)
            {
                Dec_J5 = TG.Joueurs[5].Decision;
                Eng_J5 = TG.Joueurs[5].Engagement;
                Date_J5 = DateTime.Now;
            }
            else
            {
                Dec_J5 = "MORT";
                Eng_J5 = 0;
                Date_J5 = new DateTime(1999, 12, 31);
            }
        }

        virtual public int Num_Partie { get; set; }
        virtual public int Num_Main { get; set; }
        virtual public string NomEtape { get; set; }
        virtual public int Num_Tour { get; set; }

        virtual public string Dec_J0 { get; set; }
        virtual public int Eng_J0 { get; set; }
        virtual public DateTime Date_J0 { get; set; }
        virtual public string Dec_J1 { get; set; }
        virtual public int Eng_J1 { get; set; }
        virtual public DateTime Date_J1 { get; set; }
        virtual public string Dec_J2 { get; set; }
        virtual public int Eng_J2 { get; set; }
        virtual public DateTime Date_J2 { get; set; }
        virtual public string Dec_J3 { get; set; }
        virtual public int Eng_J3 { get; set; }
        virtual public DateTime Date_J3 { get; set; }
        virtual public string Dec_J4 { get; set; }
        virtual public int Eng_J4 { get; set; }
        virtual public DateTime Date_J4 { get; set; }
        virtual public string Dec_J5 { get; set; }
        virtual public int Eng_J5 { get; set; }
        virtual public DateTime Date_J5 { get; set; }

        public override bool Equals(object o)
        {
            return (object)this == o;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
