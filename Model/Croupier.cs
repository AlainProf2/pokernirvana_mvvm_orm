/****************************************************
*   Auteur: Alain Martel 
*
*   2015-juin-22  
*
    Version C# de PokerNirvana php
*   Création de la table toursParole et normalisation de la table mises
*   Deboguing intense de Croupier.php de pokerzen. Corrige entre autres
*   les situations des P94M15 et P93M13.
*   
***************************************************/

namespace PokerNirvana_MVVM_ORM.Model
{
    public class Croupier
    {
      public int JoueurLogue;
      public string Etape;
      public string[] TabDecision;
      public int[] TabEng;
      public int NPS;
      public int Gros_Blind;
      public int ProchainJoueur;
      private int Bouton;
	
      public Croupier(string[] TabDD, int[] TabEngParam, int NPSparam, int DernierAAvoirParler, string EtapeParam, int BoutonParam)
      {
        TabDecision = TabDD;
	    TabEng = TabEngParam;
	    NPS = NPSparam;
	    JoueurLogue = DernierAAvoirParler;
	    Etape = EtapeParam;
	    ProchainJoueur = (DernierAAvoirParler+1)%6;
	    Bouton = BoutonParam;
      }
  
      // Cette fonction possède trois comportements distincts selon le contexte de l'appel:
      // 1- Lors d'une changement d'étape (par exemple quand on passe de turn à river)
      // 2- Lors d'une nouvelle main
      // 3- Le cas le plus usuel, on est en train de miser sur une main X à une étape Y
      public int DetermineProchainJoueur(string Contexte)
      {
	    
	    if (Contexte == "CHANGEMENT_ETAPE")
        {
	       DPJ_ChangementEtape();
	       return ProchainJoueur;
	    }
	
	    if (Contexte == "NOUVELLE_MAIN")
	    { 
          //tr("DPJ 4: Prochain joueur à parler");
          DPJ_NouvelleMain();
	      return ProchainJoueur;
	    }
	    DPJ_CasUsuel();
	    return ProchainJoueur;
      }
	
      //----------------------------------------------
      //
      //----------------------------------------------
      private void  DPJ_CasUsuel()	
      {
	    
	    // -1 Signifie que l'étape de mise est terminée
	    ProchainJoueur = -1;

	    if (TraiteCasInitiaux())
	    {
          return;
	    }
	  
	    // Construction du tableau des dernieres décisions ACTIVE
	    int[] TabDecActive = ConstruireTabDecisionActive();
    
	    if (TraitePlusVieilleEst_ALL_IN_RELANCER(TabDecActive))
	    {
          
	      return;
	    }
     
	    if (TraiteDerniereEst_RELANCER(TabDecActive))
	    {
      
	      return;
	    }
        if (TraiteDerniereEst_ALL_IN_SUIVRE(TabDecActive))	
	    {
       
	      return;
	    }

        if (Pattern_RR_RRAI(TabDecActive) || 
	        Pattern_SR_SRAI(TabDecActive))
          TrouveProchain();					
	    else
	    {
           if (Paralysie())
	          ProchainJoueur = -2;
	       else
	          ProchainJoueur = -1;
	    }
      } 
      //----------------------------------------------
      //
      //----------------------------------------------
      private bool  TraiteDerniereEst_ALL_IN_SUIVRE(int[] TabDecActive)
      {
        if (TabDecision[0] != "ALL_IN_SUIVRE")
	      return false;
	  
        //tr("La plus vieille dec : ALL_IN_SUIVRE");
        int NbParalyse=0;
        for (int i=0; i < TabDecActive.Length; i++)
	    {
	      if ( TabDecision[i] == "ALL_IN_RELANCER" ||
		       TabDecision[i] == "ALL_IN_SUIVRE" )
            NbParalyse++;				 
	    }
        if (NbParalyse == TabDecActive.Length)
	    {
	      //tr("Tous sont morts ou paralysés");
	      ProchainJoueur = -2;
	      return true;
	    }
	
	    if (Pattern_RR_RRAI(TabDecActive) ||
	        Pattern_SR_SRAI(TabDecActive))
        {
          //tr("DPJ 80: Pattern RR_RRAI ou SR_SRAI");
          TrouveProchain();
          return true;		   
	    }
	    else
	    {
          //tr("DPJ 82: Tous ont parler");
	      if (Paralysie())
    	      ProchainJoueur = -2;
	      return true;
        }
      }
  
      //----------------------------------------------
      //
      //----------------------------------------------
 
      private bool TraiteDerniereEst_RELANCER(int[] TabDecActive)
      {  
        if ( TabDecision[TabDecActive[0]] != "RELANCER")
	    {
	      return false;
	    }
       	   
	    int NbParalysee = 0;
	    for (int i=0; i<6; i++)
	    {
	      if (TabDecision[i] == "ABANDONNER" ||
		      TabDecision[i] == "MORT"       ||
		      TabDecision[i] == "ALL_IN_SUIVRE" ||		  
		      TabDecision[i] == "Muet")
		    NbParalysee++;
	    }
	    if (NbParalysee >= 5)
	    {
	      ProchainJoueur = -2;
	      return true;
	    }

	    bool TouteAutreDecisionEst_SUIVRE = true;
	    for (int i=1; i<TabDecActive.Length; i++)
	    {
	      int idx = TabDecActive[i];
	      if (TabDecision[idx] != "SUIVRE" &&
		      TabDecision[idx] != "ALL_IN_SUIVRE" )
	      {
            TouteAutreDecisionEst_SUIVRE = false;
		    break;
	      }
	    }

	    if (TouteAutreDecisionEst_SUIVRE)
	    {
          return true;
	    }
	    else
	    {
          TrouveProchain();					
	      return true;
	    }
      }

	   
      //----------------------------------------------
      //
      //----------------------------------------------
      private bool TraitePlusVieilleEst_ALL_IN_RELANCER(int[] TabDecActive)
      {
        if ( TabDecision[TabDecActive[0]] != "ALL_IN_RELANCER")
	    {
	       return false;
	    }
	    int NbMuet = 0;
	    for (int i=0; i<6; i++)
	    {
          if (TabDecision[i] == "ABANDONNER" ||
	          TabDecision[i] == "MORT" ||
		      TabDecision[i] == "ALL_IN_RELANCER" ||
		      TabDecision[i] == "ALL_IN_SUIVRE" ||
		      TabDecision[i] == "Muet" )
	        NbMuet++;
	    }
        if (NbMuet == 6)
	    {
	      ProchainJoueur = -2;
	      return true;
	    }

	    bool TouteAutreDecisionEst_SUIVRE = true;
	    for (int i=1; i<TabDecActive.Length; i++)
	    {
	      int idx = TabDecActive[i];
	      if (TabDecision[idx] != "SUIVRE" &&
		      TabDecision[idx] != "ALL_IN_SUIVRE")
	      {
            TouteAutreDecisionEst_SUIVRE = false;
		    break;
	      }
	    }

	    if (TouteAutreDecisionEst_SUIVRE)
	    {
	      if (NbMuet == 5)
		    ProchainJoueur = -2;
          return true;
	    }
	    else
	    {
          if (Pattern_RR_RRAI(TabDecActive))
	      {
            TrouveProchain();					
            return true;
          }  
          if (Pattern_SR_SRAI(TabDecActive))
	      {
            TrouveProchain();					
            return true;
          }  
          if (Pattern_RS(TabDecActive))
	      {
            ProchainJoueur = -1;					
		    return true;
	      }  
          if (Pattern_RSAI(TabDecActive))
	      {
            ProchainJoueur = -2;					
		    return true;
	      }
          if (Pattern_SR_SRAI(TabDecActive))
          {
            TrouveProchain();
		    return true;
	      }
          else           
	      {
		    if (Paralysie())
		       ProchainJoueur = -2;					
            else
               ProchainJoueur = -1;
            return true;		   
	      }
	    }
      }
  
      //----------------------------------------------
      //
      //----------------------------------------------
      private bool  Paralysie()
      {
        int Actif = 0;
        for (int i=0; i <6; i++)
	      if (TabDecision[i] == "SUIVRE" ||
              TabDecision[i] == "RELANCER" )
	        Actif++;
		
        if (Actif > 1)
    	    return false;
	    return true;	
      }
  
      //----------------------------------------------
      //
      //----------------------------------------------
      private int[] ConstruireTabDecisionActive()
      {	
	    int OffSet = (JoueurLogue + 1) % 6;	
	    int[] TabDecActive = new int[6];
        int idxDecActive = 0;
	    for (int i = 0; i <6; i++)
	    {
          if (TabDecision[OffSet] != "ABANDONNER" &&
	          TabDecision[OffSet] != "MORT" &&
		      TabDecision[OffSet] != "Muet" )
	      {
	        //tr("DPJ 5.11: Ajoute un actif: OffSet:" . TabDecision[OffSet]); 
		    TabDecActive[idxDecActive] = OffSet;
            idxDecActive++;
	      }
          OffSet = (OffSet+1) % 6;
	    }
	    return TabDecActive;
      }
  
      //----------------------------------------------
      //
      //----------------------------------------------
      private bool TraiteCasInitiaux()
      {	  
	    if (CasInitiauxPF())
	    {
	       
	       TraiteCasInitiauxPF();
	       return true;
	    }

	    if (CasInitiauxGen())
	    {
	      
	       TraiteCasInitiauxGen();
	       return true;
	    }
	    return false;
      }	


      //----------------------------------------------
      //
      //----------------------------------------------
      private bool CasInitiauxPF()
      {  
        for (int i=0; i <6; i++)
	      if (TabDecision[i] == "PETIT_BLIND" ||
              TabDecision[i] == "GROS_BLIND" )
	        return true;
	    return false;	
      }	
      //----------------------------------------------
      //
      //----------------------------------------------
      private void  TraiteCasInitiauxPF()  
      {
        for (int i=1; i <7; i++)
	    {
          int idx = (i + JoueurLogue)%6;
          if (TabDecision[idx] == "PETIT_BLIND" ||
		      TabDecision[idx] == "GROS_BLIND"  ||
		      TabDecision[idx] == "Attente" )
	      {
		    //tr("DPJ 5.2c: On a trouvé le prochain: idx-");
		    ProchainJoueur = idx;
		    return;
          }
	    }
      }
      //----------------------------------------------
      //
      //----------------------------------------------
      private bool CasInitiauxGen()
      {  
        for (int i=0; i <6; i++)
	    {
	      if (TabDecision[i] == "Attente")
	        return true;
		
          if (TabDecision[i] == "Parole")
             return true;
        } 			
	    return false;	
      }	
      //----------------------------------------------
      //
      //----------------------------------------------
      private void  TraiteCasInitiauxGen()  
      {
        bool ParoleTrouvee = false;
	
        for (int i=1; i <=6; i++)
	    {
         
          int idx = (i + JoueurLogue)%6;
       
	  
          if (TabDecision[idx] == "Attente")
	      {
	
		    ProchainJoueur = idx;
		    return;
          }
          if (TabDecision[idx] == "Parole")
	      {
	
		    if (ParoleEstDernierEtPersonneRelance())
		    {
		       if (PlusDUnActif())
                 ProchainJoueur = -1;
		       else   
		         ProchainJoueur = -2;
               return;			 
		    }   
		    else
            {
		 
		       if (!ParoleTrouvee)
		       {
		          ProchainJoueur = idx;
			      ParoleTrouvee = true;
		       }
		    }
          }
	    }
      }
 
       //----------------------------------------------
      //
      //----------------------------------------------
      private bool PlusDUnActif()
      {
         int nbActifs=0;
         for(int i=0; i<6; i++)
	     {
	        if ( TabDecision[i] == "SUIVRE" ||
		         TabDecision[i] == "RELANCER" )
              nbActifs++;		 
	     }
	     if (nbActifs > 1)
	       return true;
         return false;	   
      }
      //----------------------------------------------
      //
      //----------------------------------------------
      private bool ParoleEstDernierEtPersonneRelance()
      {
	    int nbRelancerAvantParole = 0;
	    int nbParole = 0;
	
	    for (int i=0; i<6; i++)
	    {
	        int idx = (i + JoueurLogue) % 6;
		    if ( TabDecision[idx] == "Parole")
            {
              
              nbParole++;
		    }
            else
		    {
		       if ( TabDecision[idx] == "RELANCER" ||
		            TabDecision[idx] == "ALL_IN_RELANCER")
               {
		         nbRelancerAvantParole++;
		       }
		    }
	    }
	    if (nbRelancerAvantParole == 0 &&
	        nbParole == 1 )
        {
           return true;
	    }
        return false;	   
      }
      //----------------------------------------------
      //
      //----------------------------------------------
      private void  TrouveProchain()
      {
        for (int i = 0; i<5; i++)
        {
	      //echo "
	      //Candidat " . i+1+JoueurLogue . " dd:" .TabDecision[(i+1+JoueurLogue)%6];
	      if (TabDecision[(i+1+JoueurLogue)%6] != "MORT" &&
		      TabDecision[(i+1+JoueurLogue)%6] != "ABANDONNER" &&
		      TabDecision[(i+1+JoueurLogue)%6] != "Muet" &&
		      TabDecision[(i+1+JoueurLogue)%6] != "ALL_IN_SUIVRE" &&
		      TabDecision[(i+1+JoueurLogue)%6] != "ALL_IN_RELANCER" )
          {
            ProchainJoueur = (i+1+JoueurLogue)%6;
		    break;
          }				 
	    }
      }					
      //----------------------------------------------
      //
      //----------------------------------------------
      private void  TrouveProchainProchaineMise()
      {

        for (int i = 1; i<6; i++)
        {
	      int idx = (i+Bouton)%6;
	      //echo "
	      //Candidat idx dd:" . TabDecision[idx];
	      if (TabDecision[idx] != "MORT" &&
		      TabDecision[idx] != "ABANDONNER" &&
		      TabDecision[idx] != "ALL_IN_SUIVRE" &&
		      TabDecision[idx] != "ALL_IN_RELANCER" )
          {
            ProchainJoueur = idx;
		    return;
          }				 
	    }
	    ProchainJoueur = -1;
      }					


      //----------------------------------------------
      //
      //----------------------------------------------
      private bool Pattern_RR_RRAI(int[] TabDD)
      {
          int i = 0;
         for (; i < TabDD.Length; i++)
	     {
            if (TabDecision[TabDD[i]] == "RELANCER")
              break;
         }
         i++;	 
         for(; i< TabDD.Length; i++)
	     {
            if (TabDecision[TabDD[i]] == "RELANCER" ||
		        TabDecision[TabDD[i]] == "ALL_IN_RELANCER")
		    {
		      //tr("Pattern RR ou RRAI est vrai");
              return true;
		    }
         }
         //tr("Pattern RR ou RRAI est faux");
	     return false;
      }

      //----------------------------------------------
      //
      //----------------------------------------------
      private bool Pattern_SR_SRAI(int[] TabDD)
      {
          int i = 0;
          for (; i < TabDD.Length; i++)
	     {
            if (TabDecision[TabDD[i]] == "SUIVRE")
              break;
         }
         i++;
         for (; i < TabDD.Length; i++)
	     {
            if (TabDecision[TabDD[i]] == "RELANCER" ||
		        TabDecision[TabDD[i]] == "ALL_IN_RELANCER")
		    {
              //tr("Pattern SR ou SRAI est vrai");
              return true;
		    }
         }
         //tr("Pattern SR ou SRAI est faux");
	     return false;
      }
	
      //----------------------------------------------
      //
      //----------------------------------------------
      private bool Pattern_S_R(int[] TabDD)	
      {
          int i = 0;
          for (; i < TabDD.Length; i++)
	     {
            if (TabDecision[TabDD[i]] == "SUIVRE")
              break;
         }
         i++;
         for (; i < TabDD.Length; i++)
	     {
            if (TabDecision[TabDD[i]] == "RELANCER" ||
		        TabDecision[TabDD[i]] == "ALL_IN_RELANCER")
		    {
              //tr("Pattern SR ou SRAI est vrai");
              return true;
		    }
         }
         //tr("Pattern SR ou SRAI est faux");
	     return false;
      }
	
      //----------------------------------------------
      //
      //----------------------------------------------
      private bool Pattern_RS(int[] TabDD)
      {
          int i = 0;
         for (; i < TabDD.Length; i++)
	    {
          if (TabDecision[TabDD[i]] == "RELANCER")
            break;
        }
        i++;
        for (; i < TabDD.Length; i++)
	    {
          if (TabDecision[TabDD[i]] == "SUIVRE")
	      {
            //tr("Pattern RS est vrai");
            return true;
	      }
        }
        //tr("Pattern RS est faux");
	    return false;
      }

      //----------------------------------------------
      //
      //----------------------------------------------
      private bool Pattern_RSAI(int[] TabDD)
      {
          int i = 0;
          for (; i < TabDD.Length; i++)
	    {
          if (TabDecision[TabDD[i]] == "RELANCER")
            break;
        }
        i++;
        for (; i < TabDD.Length; i++)
	    {
          if (TabDecision[TabDD[i]] == "ALL_IN_SUIVRE")
	      {
            //tr("Pattern RSAI est vrai");
            return true;
	      }
        }
        //tr("Pattern RSAI est faux");
	    return false;
      }
  
	
      //----------------------------------------------
      //
      //----------------------------------------------
	  
      private void  DPJ_NouvelleMain()
      {
        ProchainJoueur = -1;
	  
	    // Trois boucles : une pour le petit blind
	    // une pour le gros blind
	    // une pour le prochain joueur
        int i = 1;
        int ind;
	    for (; i < 6; i++)
	    {
          ind =  (i + Bouton) % 6;
	      if (TabDecision[ind] != "MORT" &&
	          TabDecision[ind] != "MOURANT")
	      {
	        int Petit_Blind = ind;
	        break;
	      }
	    }
	    //tr("Petit blind: ind");

	    i++;
	    for (; i < 12; i++)
	    {
          ind =  (i + Bouton) % 6;
	      if (TabDecision[ind] != "MORT" &&
	          TabDecision[ind] != "MOURANT")
	      {
	        Gros_Blind = ind;
	        break;
	      }
	    }
	    //tr("Gros blind: ind");

	    i++;
	    for (; i <= 18; i++)
	    {
	      ind =  (i + Bouton) % 6;
	      if (TabDecision[ind] != "MORT" &&
	          TabDecision[ind] != "MOURANT")
	      {
 	        ProchainJoueur = ind ;
	        break;
	      }
	    }
	    //tr("Prochain: ind");
      }	  
      //----------------------------------------------
      //
      //----------------------------------------------

	    private void  DPJ_ChangementEtape()
	    {
	      if ( ProchainJoueur == -2)
	      {
            //La mise est paralysée, il n'y a pas de prochain joueur à parler
            return;
	      }
	  
	      for (int i=1; i < 6; i++)
	      {
            // On inspecte le candidat à la gauche du bouton
		    // A-t-il un statut "ACTIF" ou paralysé (ABANDON, MORT, ALL_IN)

            if (TabDecision[(Bouton + i)%6] != "ABANDONNER" &&
		        TabDecision[(Bouton + i)%6] != "MORT" &&
		        TabDecision[(Bouton + i)%6] != "ALL_IN_SUIVRE" &&
		        TabDecision[(Bouton + i)%6] != "ALL_IN_RELANCER")
		    {
		      ProchainJoueur = (Bouton + i)%6;
		      return;
		    }
	      }
	      ProchainJoueur = -2;
	    }
    }
}