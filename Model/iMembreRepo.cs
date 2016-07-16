using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerNirvana_MVVM_ORM.Model
{
    interface iMembreRepo
    {
        void Inserer(Membre m);
        void MAJ(Membre m);
        void Supprimer(Membre c);
        void Tronquer();

        Membre RecupUn(string n);

        ObservableCollection<Membre> RecupTousMembres();
        
    }
}
