using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerNirvana_MVVM_ORM.Model
{
    interface iHistoriqueRepo
    {
        void Inserer(Historique c);
        void MAJ(Historique c);
        string RecupHistoriqueDunePartie(int t);
    }
}
