using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerNirvana_MVVM_ORM.Model
{
    class ConfigPartie
    {
       public string Type;
       public int RythmeCroissance;
       public int Maximum;
       public int DelaiReflexion;
       public string Courriel;
   
      public ConfigPartie(int Partie)
      {
     //    string sel = "select Blind_Type, Blind_Croissance, Blind_Max, DelaiReflexion, Courriel from Configuration where num_partie=" + Partie;
     //    List<string>[] res = TrousseGlobale.MaBD.Select(sel);
     //    Type             = res[0][0];	 
     //    RythmeCroissance = Convert.ToInt32(res[0][1]);	 
     //    Maximum          = Convert.ToInt32(res[0][2]);
     //    DelaiReflexion   = Convert.ToInt32(res[0][3]);
     //    Courriel		  = res[0][4];
     }
  }
}
