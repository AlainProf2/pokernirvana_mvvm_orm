using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerNirvana_MVVM_ORM.Model
{
    interface iJoueurRepo
    {
        void MAJ(Joueur c);
        List<Joueur> RecupJoueursDunePartie(int p);
        Joueur RecupUnJoueur(string Nom);
    }
}
