using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerNirvana_MVVM_ORM.Model
{
    interface iTournoiRepo
    {
        void Inserer(Tournois c);
        void MAJ(Tournois c);
        void Supprimer(Tournois c);
        void Tronquer();
        System.Collections.ObjectModel.ObservableCollection<Tournois> RecupTousTournois();
    }
}
