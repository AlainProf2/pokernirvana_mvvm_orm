using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerNirvana_MVVM_ORM.Model
{
    interface iMainRepo
    {
        void Inserer(uneMain c);
        void MAJ(uneMain c);
        uneMain RecupUneMain(int p, int m);
    }
}
