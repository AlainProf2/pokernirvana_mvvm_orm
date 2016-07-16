using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerNirvana_MVVM_ORM.Model
{
    interface iPartieRepo
    {
        void Inserer(Partie c);
        void MAJ(Partie c);
        ObservableCollection<Partie> RecupPartiesDunTournoi(int t);
        Partie RecupUnePartie(int n);
    }
}
