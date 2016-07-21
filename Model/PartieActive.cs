using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

using System.Threading;
using PokerNirvana_MVVM_ORM.View;
using PokerNirvana_MVVM_ORM.ViewModel;

namespace PokerNirvana_MVVM_ORM.Model
{
    public class PartieActive : Partie
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        //public void OnPropertyChanged(PropertyChangedEventArgs e)
        //{
        //    //if (PropertyChanged != null)
        //    //    PropertyChanged(this, e);
        //}
        virtual public Joueur[] Joueurs {get; set;}
        public uneMain[] MainDesJoueurs;
        public int JoueurLogue;        
        public int ProchainJoueur;
        public string GagnantPartie = "";
        private int Num_Tour;
        private int NiveauPourSuivre;

        public string Etape;
        //private int Num_Enchere;
        private int Bouton;
        private string DateCreation;
        private BDService MaBd;

        private Paquet lePaquet;

        //private DateTime DateDebut;
        public Carte[] Flop;
        public Carte Turn;
        public Carte River;
        private Evaluateur Eval;
        private ConfigPartie Config;
        //public util U = new util();

        private string msgAttente;
        public string MsgAttente
        {
            get { return msgAttente; }
            set { msgAttente = value; }
        }

        private string gagnantsMain;
        public string  GagnantsMain
        {
            get { return gagnantsMain; }
            set { gagnantsMain = value; }
        }

        private string titre;
        public string Titre
        {
            get { return titre; }
            set { titre = value; }
        }
      
        private string titreOptions;
        public string  TitreOptions
        {
            get { return titreOptions; }
            set { titreOptions = value; }
        }

        private string msgHistorique;
        public string MsgHistorique
        {
            get { return msgHistorique; }
            set { msgHistorique = value; }
        }

        private string dernierRefresh;
        public string DernierRefresh
        {
            get { return dernierRefresh; }
            set { dernierRefresh = value; }
        }
        
        private int resteDelai;
        public int ResteDelai
        {
            get { return resteDelai; }
            set { resteDelai = value; }
        }

        private TexasTable laTable;  

      //----------------------------------------------
      //  Constructeur de Partie
      //----------------------------------------------

      public PartieActive(string JouLog, int NumPartie, bool NouvellePartie, int RD)
      {
          InfoDeBase(JouLog, NumPartie);
          
          if (NouvellePartie)
          {
              //InitNouvellePartie();
              //TrousseGlobale.Contexte = "RECHARGE_PARTIE_EN_COURS";
          }
          else
              ReCharge(RD);
      }


    /*--------------------------------------------------------------
    /
    /---------------------------------------------------------------*/
      private void InfoDeBase(string JouLog, int NumPartie)
      {
	      GagnantsMain = ""; 
	      Numero      = NumPartie;
          //MaBd   = new BDService();
          JoueurLogue = ConvertNomToNum(JouLog);
	      Config = new ConfigPartie(Numero);
          Eval   = new Evaluateur();
       }
 
    /*--------------------------------------------------------------
     /
     /---------------------------------------------------------------*/
      private void InitNouvellePartie()
       {	  
	      
 	      Numero_Main = 0;
          Num_Tour  = 1;
          Etape = "PRE_FLOP";
          Bouton = 0;

          InsertionPremiereMain();
          //TrousseGlobale.AjouteHistorique("Début de la partie " + Numero, Numero);
          //TrousseGlobale.DernierRefresh = TrousseGlobale.GetDernierHistorique();
          ProchainJoueur = 1;
	      NouvelleMain();
       }
	  
     /*--------------------------------------------------------------
     /
     /---------------------------------------------------------------*/
      private void InsertionPremiereMain()
      {
         
      }
    /*--------------------------------------------------------------
     /
     /---------------------------------------------------------------*/
      private void ReCharge(int RD)
      {
          string sel = "select numero_main, debut from partie where numero = " + Numero;
          //List<string>[] resPartie = MaBd.Select(sel);

          // Une partie en cours existe, nous la poursuivons
          //DateCreation = resPartie[0][1]; 
          //Numero_Main = Convert.ToInt32(resPartie[0][0]);

          //sel = "select etape from main where num_partie = " + Numero + " and num_Main = " + Numero_Main;
          //List<string>[] resMain = MaBd.Select(sel);

          // Une partie en cours existe, nous la poursuivons
          //Etape = resMain[0][0];

          //TrousseGlobale.DernierRefresh = TrousseGlobale.GetDernierHistorique();
          ResteDelai    	 = RD;  
	      bool MainTerminee = SelBoutonEtEtape();
		
          PrepareMain(MainTerminee);
	      ChargeCarte();
          InitJoueurs();
	    }  

        /*--------------------------------------------------------------
 /
 /---------------------------------------------------------------*/
   private void PrepareMain(bool MainTerminee)		
   {
      string req;
      List<string>[] res;
      if(MainTerminee)
      {
        req = "select gagnant from main where num_partie= " + Numero + " and num_main = " + Numero_Main;
        //res = MaBd.Select(req);
        //if (res[0][0] !="")
        //  GagnantsMain = res[0][0];   
               
        req = "select num_tour, prochainJoueur from etapes where num_partie = " + Numero +
        " and num_main = " + Numero_Main+ " and (etape = 'MAIN_TERMINEE_TRAITEE' or etape = 'MAIN_TERMINEE_TRAITEE_OUVERTE')";
      }
      else
	  {
	    req = "select num_tour, ProchainJoueur from etapes where num_partie = " + Numero +
                " and num_main = " + Numero_Main + " and etape = '" + Etape + "'";
      }

      //res = MaBd.Select(req);
      Num_Tour = 1;
      ProchainJoueur = Bouton;
      //if (res[0][0] != "")
      //{
      //    Num_Tour = Convert.ToInt32(res[0][0]);
      //    ProchainJoueur = Convert.ToInt32(res[0][1]);
      //}
    }

 /*--------------------------------------------------------------
 /
 /   AppliqueDecision
 /
 /---------------------------------------------------------------*/
  public void AppliqueDecision(string joueur, int Position, string Decision)
  {
      return;
    int Relance = 0;

    GetEngagement();
    NiveauPourSuivre = 0; // GetNiveauPourSuivre();
    int deltaPourSuivre = NiveauPourSuivre - Joueurs[Position].Engagement;
	//tr("AppliqueDecision NPS: NiveauPourSuivre Delta:deltaPourSuivre");
 
    switch (Decision)
    {
    
	  case "ABANDONNER":
        //TrousseGlobale.AjouteHistorique(joueur + " abandonne", Numero);
		Joueurs[Position].Decision = "ABANDONNER";
        break;
      case "GRATOS":
        //TrousseGlobale.AjouteHistorique(joueur + " y va gratos", Numero);
		   Decision = "SUIVRE";
		break;   
      case "SUIVRE":
		if (deltaPourSuivre >= Joueurs[Position].Capital)
		{
		   Decision = "ALL_IN_SUIVRE";
           ////TrousseGlobale.AjouteHistorique(joueur + " suit ALL_IN", Numero);
		   Joueurs[Position].Engagement += Joueurs[Position].Capital;
		   Joueurs[Position].Capital = 0;
        }		   
		else
        {
            //TrousseGlobale.AjouteHistorique(joueur + " suit", Numero);
		   Joueurs[Position].Engagement = NiveauPourSuivre;
		   Joueurs[Position].Capital -= deltaPourSuivre;
		}
		updateCapital(Position);
        break;
      
	  case "RELANCER DE:":
        //Relance = TrousseGlobale.Relance;

        
        JoueurLogue= ConvertNomToNum(joueur);
       
	    Joueurs[Position].Engagement = NiveauPourSuivre + Relance;
	    Joueurs[Position].Capital -= deltaPourSuivre + Relance;
		
		updateCapital(Position);
       
        if (Joueurs[Position].Capital <= 0)
        {
           Decision = "ALL_IN_RELANCER";
           //TrousseGlobale.AjouteHistorique(joueur + " relance ALL_IN", Numero);
        }		   
		else
		{
           Decision = "RELANCER";
           //TrousseGlobale.AjouteHistorique(joueur + " relance de " + Relance, Numero);
		}
		break;
    }
	Joueurs[Position].Decision = Decision;
    UpdateParole(Decision, Joueurs[Position].Engagement, Relance);
 }

 /*--------------------------------------------------------------
 /
 /
 /---------------------------------------------------------------*/
  private void UpdateParole( string Decision, int montantEngage, int Relance)
  {
   
    DateTime date =  DateTime.Now;      
    string upd = "update main set niveauPourSuivre = niveauPourSuivre + " + Relance +
               " where num_partie = " + Numero + " and num_main  = " + Numero_Main; 
    //MaBd.Update(upd);

    upd = "update toursParole set dec_j" + JoueurLogue + " = '" + Decision + "'," + 
                      "eng_j" + JoueurLogue + " = " + montantEngage + ", " +
                      "date_J" + JoueurLogue + "= '" + date + "'" +
                      "where num_partie = "  + Numero + " and " +
                     " num_main  =  " + Numero_Main + " and etape = '" + Etape + "' and " +
					 " num_tour = " + Num_Tour; 
    //MaBd.Update(upd);
	string[] TabDD = PrepareDerniereDecision();
    int[] TabEng = new int[6];
    for (int i=0; i<6; i++)
	   TabEng[i] = Joueurs[i].Engagement;
   
    Croupier croupier = new Croupier(TabDD, TabEng, NiveauPourSuivre, JoueurLogue, Etape, Bouton);
    ProchainJoueur = croupier.DetermineProchainJoueur("");
	
	upd = "update etapes set ProchainJoueur = " + ProchainJoueur + ", " +
					  "date_evenement = '" + date + "' " +
               "where num_partie = " +  Numero + " and " +
                     "num_main  =  " + Numero_Main + " and " +
                     "etape = '" + Etape + "'"; 
    //MaBd.Update(upd);
  }

 /*--------------------------------------------------------------
 /
 /---------------------------------------------------------------*/
  private string[] PrepareDerniereDecision()
  {
    string sel = "select dec_j0, dec_j1, dec_j2, dec_j3, dec_j4, dec_j5 " + 
	         "from toursParole where num_partie = " + Numero + " and " +
	                                  "num_main = " + Numero_Main + " and " +
						                 "etape = '" + Etape + "' and " +
						              "num_tour = " + Num_Tour;
    //List<string>[] resParole = MaBd.Select(sel);
    List<string>[] resParole2 = new List<string>[6];
	
	if ( Num_Tour > 1)
	{
      sel = "select dec_j0, dec_j1, dec_j2, dec_j3, dec_j4, dec_j5 " + 
	      "from toursParole where num_partie = " + Numero + " and " +
	                                  "num_main = " + Numero_Main + " and " +
						                 "etape = '" + Etape + "' and " +
						              "num_tour = " + Num_Tour + "- 1";
      //resParole2 = MaBd.Select(sel);
	}
	
    string[] tabDerniereDecision = new string [6];
    //for (int i=0; i<6; i++)
    //{
    //   tabDerniereDecision[i] = resParole[0][i];
    //   if (tabDerniereDecision[i] == "Parole")
    //   {
    //      tabDerniereDecision[i] = resParole2[0][i];
    //   }
    //}
	return tabDerniereDecision;
  }

/*--------------------------------------------------------------
 /
 /   
 /
 /---------------------------------------------------------------*/
  private void GetEngagement()
  {
     // On prend la somme déjà engagée par le joueur et le niveau requis pour suivre
	 string req = "Select eng_j0, eng_j1, eng_j2, eng_j3, eng_j4, eng_j5 " +
 					" from toursParole  where num_partie = " + Numero + " and "+
                    "num_main  = " + Numero_Main + " and etape = '" + Etape + "' and "+
					" num_tour = " + Num_Tour;
 
     ////List<string>[] res = MaBd.Select(req);
    // if (res[0][0] != "")
    // {
    //    for (int i=0; i<6; i++)
    //    {	
    //       //tr (Joueurs[i].Pokerman . " k:" . Joueurs[i].Capital);
    //       Joueurs[i].Engagement =  Convert.ToInt32(res[0][i]);    	 
    //    }
    //}
    //else
    //{
    //    for (int i=0; i<6; i++)
    //    {	
    //       Joueurs[i].Engagement =  0;    	 
    //    }
    //}
  }

 /*--------------------------------------------------------------
 /
 /   ConsequenceDeLaDecision()
 /
 /---------------------------------------------------------------*/
  public void ConsequenceDeLaDecision(string From)
  {    
	string statutDeMise = getEtatDeLaMise();
    switch(statutDeMise)
    {
      case ("MISE_PARALYSEE"):
        DetermineProchaineEtape();
		if (From != "RECUR")
           //TrousseGlobale.AjouteHistorique("Mise bloquée " + Etape, Numero);

        // Insertion de la prochaine Etape de mise de la main courante
        insertNouvelleEtape();    
        Num_Tour  = 1;          
        
        if (Etape == "FLOP")
		{
          distribueFlop();
		  ConsequenceDeLaDecision("RECUR");
          return;
        }
        if (Etape == "TURN")
		{
          distribueTurn();
		  ConsequenceDeLaDecision("RECUR");
          return;
		}
        if (Etape == "RIVER")
		{
          distribueRiver();
          ConsequenceDeLaDecision("RECUR");
          return;
        }
        if (Etape == "POST_RIVER")
		{
          TraitementMainTermine("PARALYSIE");
        }
        break;

      case ("MISE_TERMINEE_MAIN_TERMINEE_DOMINATION"):
		TraitementMainTermine("DOMINATION");
        break;

      case ("MAIN_TERMINEE_DEPARTAGE"):
		TraitementMainTermine("DEPARTAGE");
	    break;
		     
      case ("MISE_TERMINEE_MAIN_CONTINUE"):
      
        DetermineProchaineEtape();
        //TrousseGlobale.AjouteHistorique("On passe à: " + Etape, Numero);
 
        // Insertion de la prochaine Etape de mise de la prochaine main courante
        insertNouvelleEtape();    
   		//Msg = "Tu as la parole.";
		//CreateCourriel("PokerNirvana: Partie Numero, main Numero_Main nouvelle étape:Etape", Msg, Joueurs[ProchainJoueur], Config.Courriel);
        Num_Tour  = 1;          
      
        if (Etape == "FLOP")
          distribueFlop();
        if (Etape == "TURN")
          distribueTurn();
        if (Etape == "RIVER")
          distribueRiver();
        break;
        
      case ("MISE_CONTINUE"):
	    if (tourComplete())
		{
		   Num_Tour++;
	       insereNouveauTour();
		}
        //tr("Mise continue: prochain: ProchainJoueur");
        //Msg = "A toi la parole.";
        //CreateCourriel("PokerNirvana: Partie Numero, main Numero_Main, étape Etape", Msg, Joueurs[ProchainJoueur], Config.Courriel);
        break;
		  
      default : 
          break;           
    }
  }

 /*--------------------------------------------------------------
 /
 /   tourComplete()
 /
 /---------------------------------------------------------------*/
  private bool tourComplete()
  {
      for (int i = 0; i < 6; i++)
      {
          if (Joueurs[i].Decision == "Attente" ||
              Joueurs[i].Decision == "GROS_BLIND" ||
              Joueurs[i].Decision == "Parole")
              return false;
      }
      return true;
  }
 /*--------------------------------------------------------------
 /
 /---------------------------------------------------------------*/
  private void insereNouveauTour()
  {
     for (int i=0; i<6; i++)
	 {
	    if (Joueurs[i].Decision == "ALL_IN_RELANCER" ||
		    Joueurs[i].Decision == "ALL_IN_SUIVRE" )
          Joueurs[i].Decision = "Muet";			

        if (Joueurs[i].Decision == "RELANCER" ||
		    Joueurs[i].Decision == "SUIVRE"  ||
			Joueurs[i].Decision == "GRATOS")
          Joueurs[i].Decision = "Parole";			
     }
	 
	 DateTime dateIns = DateTime.Now;
        
	 string ins = "insert into toursParole values( " + Numero + ", " + Numero_Main + ", '" + Etape+ "', " + Num_Tour + " ,'" + 
	 Joueurs[0].Decision + "'," + Joueurs[0].Engagement + ", '" + dateIns + "','" + 
	 Joueurs[1].Decision + "'," + Joueurs[1].Engagement + ", '" + dateIns + "','" + 
	 Joueurs[2].Decision + "'," + Joueurs[2].Engagement + ", '" + dateIns + "','" + 
	 Joueurs[3].Decision + "'," + Joueurs[3].Engagement + ", '" + dateIns + "','" + 
	 Joueurs[4].Decision + "'," + Joueurs[4].Engagement + ", '" + dateIns + "','" + 
	 Joueurs[5].Decision + "'," + Joueurs[5].Engagement + ", '" + dateIns +  "')";

	 //MaBd.Insert(ins);

     string upd = "update etapes set Num_tour = " + Num_Tour +
             " where Num_Partie = " + Numero + " and " +
                  " num_main   = " + Numero_Main + " and " +
                  " etape      = '" + Etape + "'";
     //MaBd.Update(upd);					
  }




/*--------------------------------------------------------------
 /
 /---------------------------------------------------------------*/
       private bool SelBoutonEtEtape()
       {
         string sel1 = "select etape, bouton, fin from main where num_partie= " + Numero + " and num_main = " + Numero_Main;
         //List<string>[] resMain = MaBd.Select(sel1);
         //if (resMain[0][0]!= "")
         //  Bouton = Convert.ToInt32(resMain[0][1]);
         
	   
         //string sel2 = "select etape from etapes "+
         //         "where num_partie= " + Numero + " and "+
         //               "num_main = " +  Numero_Main + " and "+
         //               "(etape = ' " + resMain[0][0] + "' or  "+
         //               " etape = 'MAIN_TERMINEE_TRAITEE' or  " +
         //               " etape = 'MAIN_TERMINEE_TRAITEE_OUVERTE')";
         // //List<string>[] resEtape = MaBd.Select(sel2);
          //if (resEtape[0][0]!= "")
          //  Etape = resEtape[0][0];  
          //    // Si la date de fin (resMain[0][2]) est NULL ça veut dire que la main se poursuit*/

          //if (resMain[0][0] == "")
          //{
          //    return false;
          //}
          //if (resMain[0][2] == "")
          //{
          //    return false;
          //}
           return true;		 
       }
        /**************************************
        *
        **************************************/
          private int ConvertNomToNum(string nom)
          {
             string sel = "select position from joueurPartie where pokerman = '" + nom + "' and numero_partie = " + Numero;
             //List<string>[] res  = MaBd.Select(sel);
             //if (res[0][0] != "")
             //   return (Convert.ToInt32(res[0][0]));
             //MessageBox.Show("nom: nom de joueur impossible");
             return 0;
          }

         /**************************************
         *
         **************************************/
        public string ConvertNumToNom(int num)
        {
           if (num == -1)
	          return "fin d'étape de mise";
           if (num == -2)
              return "fin de main";	 
           if (num == -3)
              return "Entre deux partie";	 

	       string sel = "select pokerman from joueurPartie where position = " + num  + " and numero_partie = " + Numero;
	 
            ////List<string>[] res = MaBd.Select(sel); 
           return ""; // res[0][0];
        }

  
         /*--------------------------------------------------------------
         /
         /---------------------------------------------------------------*/
          public void NouvelleMain()
          {
            Numero_Main++;
	        IncrementeNumMain();

            lePaquet = new Paquet();
            lePaquet.brasse();
	        InitJoueurs();
            distribueMains();
	
            Etape = "PRE_FLOP";

            if (Numero_Main == 1)
            {
                Bouton = 0;
            }
            else
                Bouton = GetNextBouton();

            CalculDesBlinds();  
            insertionNeoMain();
            MAJCapitauxBlind(); 
	
	        MiseAZeroDecisionEtEng();
	
            insertNouvelleEtape();	
	        //string Msg = "A ton tour.";
            //CreateCourriel("PokerNirvana: Partie " + Numero + ", nouvelle main: main " + Numero_Main, Msg, 
            //               Joueurs[ProchainJoueur], 
            //               Config.Courriel);
	
          }
  
/*--------------------------------------------------------------
 /
 /
 /---------------------------------------------------------------*/
  private void MAJCapitauxBlind()
  {
     for (int i=0; i<6; i++)
	 {
        if (Joueurs[i].Decision == "PETIT_BLIND" ||
		    Joueurs[i].Decision == "GROS_BLIND")
	    {
          string upd = "update joueurPartie set capital = capital - " + Joueurs[i].Engagement + " where position = " + i + " and numero_partie = " + Numero;
          //MaBd.Update(upd);
		  break;
		}
     }
  }
 
 /*--------------------------------------------------------------
 /
 /---------------------------------------------------------------*/
  private void MiseAZeroDecisionEtEng()
  {
    for (int i=0; i<6; i++)
	{
	   if (Joueurs[i].Decision != "MORT" &&
		   Joueurs[i].Decision != "MOURANT" &&
		   Joueurs[i].Decision != "GROS_BLIND" &&
		   Joueurs[i].Decision != "PETIT_BLIND")
	   {
		   Joueurs[i].Decision = "Attente";
		   Joueurs[i].Engagement = 0;
	   }
    }	
  }

/*--------------------------------------------------------------
 /
 /---------------------------------------------------------------*/
  private void insertNouvelleEtape()
  {
    DateTime DateIns = DateTime.Now;
    if (Etape == "PRE_FLOP")
    {
        for (int i = 1; i <= 6; i++)
        {
            int indice = (i + Bouton) % 6;
            if (Joueurs[indice].Decision == "PETIT_BLIND" ||
                Joueurs[indice].Decision == "GROS_BLIND")
            {
                updateCapital(indice);
            }
        }
    }
    else
    {
        string Etape_Anterieur = "";
        if (Etape == "FLOP") Etape_Anterieur = "PRE_FLOP";
        if (Etape == "TURN") Etape_Anterieur = "FLOP";
        if (Etape == "RIVER") Etape_Anterieur = "TURN";
        if (Etape == "POST_RIVER") Etape_Anterieur = "RIVER";
        string select = "select dec_j0, eng_j0, dec_j1, eng_j1, dec_j2, eng_j2," +
                        "dec_j3, eng_j3, dec_j4, eng_j4, dec_j5, eng_j5 " +
                "from toursParole where num_partie = " + Numero + " and " +
                                  " num_main = " + Numero_Main + " and " +
                                   "   etape = '" + Etape_Anterieur + "' and " +
                                     " num_tour in (select max(num_tour) from toursparole " +
                                      "                              where num_partie = " + Numero + " and " +
                                       "                             num_main = " + Numero_Main + " and " +
                                        "                            etape = '" + Etape_Anterieur + "')";

        //List<string>[] res = MaBd.Select(select);
        //if (res[0][0] == "")
        //{
        //    MessageBox.Show("Erreur 352: sel");
        //}

        //List<string> ligne = res[0];
        //for (int i = 0; i < 6; i++)
        //{
        //    if (ligne[i * 2] != "ABANDONNER" &&
        //        ligne[i * 2] != "Muet" &&
        //        ligne[i * 2] != "MORT" &&
        //        ligne[i * 2] != "MOURANT" &&
        //        ligne[i * 2] != "ALL_IN_SUIVRE" &&
        //        ligne[i * 2] != "ALL_IN_RELANCER")
        //        ligne[i * 2] = "Attente";

        //    Joueurs[i].Decision = ligne[i * 2];
        //    Joueurs[i].Engagement = Convert.ToInt32(ligne[(i * 2) + 1]);
        //}
    }
    int NPS = 0; // GetNiveauPourSuivre();

    string[] TabDD = new string[6];
    int[] TabEng = new int[6];
    for (int i = 0; i < 6; i++)
    {
        TabDD[i] = Joueurs[i].Decision;
        TabEng[i] = Joueurs[i].Engagement;
    }
    Croupier croupier = new Croupier(TabDD, TabEng, NiveauPourSuivre, JoueurLogue, Etape, Bouton);
    if (ProchainJoueur == -2)
        ProchainJoueur = croupier.DetermineProchainJoueur("NOUVELLE_MAIN");
    else
        ProchainJoueur = croupier.DetermineProchainJoueur("CHANGEMENT_ETAPE");
    string ins = "insert into etapes values( " + Numero + ", " + Numero_Main + ", '" + Etape + "', " + ProchainJoueur + ", '" + DateIns + "', " + Num_Tour + ")";
    //MaBd.Insert(ins);
    insereNouveauTour();
  }

/*--------------------------------------------------------------
 /
 /---------------------------------------------------------------*/
  public void distribueFlop()  
  {  
     string req = "select f_c0_v, f_c0_s, " + 
                    "f_c1_v, f_c1_s, " +
                    " f_c2_v, f_c2_s " +
             "from main where Num_partie = " + Numero + " and " +
                             " Num_Main = " + Numero_Main;
     //List<string>[] res = MaBd.Select(req);
     //Flop[0] = new Carte(Convert.ToInt32(res[0][0]), Convert.ToInt32(res[0][1]));                           
     //Flop[1] = new Carte(Convert.ToInt32(res[0][2]), Convert.ToInt32(res[0][3]));                           
     //Flop[2] = new Carte(Convert.ToInt32(res[0][4]), Convert.ToInt32(res[0][5]));                           
   
  }
 
/*--------------------------------------------------------------
 /
 /---------------------------------------------------------------*/
  public void distribueTurn()  
  {
     string req = "select t_v, t_s" +
             " from main where Num_partie = " + Numero + " and " +
                             " Num_Main = " + Numero_Main;
     //List<string>[] res = MaBd.Select(req);
     //Turn = new Carte(Convert.ToInt32(res[0][0]), Convert.ToInt32(res[0][1]));                           
   
  }
  
/*--------------------------------------------------------------
 /
 /---------------------------------------------------------------*/
  public void distribueRiver()
  {
      string req = "select r_v, r_s" +
              " from main where Num_partie = " + Numero + " and " +
                              " Num_Main = " + Numero_Main;
      //List<string>[] res = MaBd.Select(req);
      //River = new Carte(Convert.ToInt32(res[0][0]), Convert.ToInt32(res[0][1]));                           
   }




 /*--------------------------------------------------------------
 /
 /--------------------------------------------------------------*/
  private void GetNiveauPourSuivre()
 {
   string sel = "select niveauPourSuivre from main " +  
		         " where num_partie = " + Numero + " and " +
                        " num_main  = " + Numero_Main; 
   
   //List<string>[] res = MaBd.Select(sel); 
   //return Convert.ToInt32(res[0][0]);
   
 }

 /*--------------------------------------------------------------
 /
 /---------------------------------------------------------------*/
  private void IncrementeNumMain()
  {
    string upd = "update partie set numero_main = "+ Numero_Main + " where numero = " + Numero;
    //MaBd.Update(upd);
  }

/*--------------------------------------------------------------
 /
 /
 /---------------------------------------------------------------*/
  private void insertionNeoMain()
  {
    int j0c0v =     MainDesJoueurs[0].mainOrigine[0].Valeur;
    int j0c0s =     MainDesJoueurs[0].mainOrigine[0].Sorte;
    int j0c1v = MainDesJoueurs[0].mainOrigine[1].Valeur;
    int j0c1s = MainDesJoueurs[0].mainOrigine[1].Sorte;
    int j1c0v = MainDesJoueurs[1].mainOrigine[0].Valeur;
    int j1c0s = MainDesJoueurs[1].mainOrigine[0].Sorte;
    int j1c1v = MainDesJoueurs[1].mainOrigine[1].Valeur;
    int j1c1s =     MainDesJoueurs[1].mainOrigine[1].Sorte;
    int j2c0v =     MainDesJoueurs[2].mainOrigine[0].Valeur;
    int j2c0s =     MainDesJoueurs[2].mainOrigine[0].Sorte;
    int j2c1v =     MainDesJoueurs[2].mainOrigine[1].Valeur;
    int j2c1s =     MainDesJoueurs[2].mainOrigine[1].Sorte;
    int j3c0v =     MainDesJoueurs[3].mainOrigine[0].Valeur;
    int j3c0s =     MainDesJoueurs[3].mainOrigine[0].Sorte;
    int j3c1v =     MainDesJoueurs[3].mainOrigine[1].Valeur;
    int j3c1s =     MainDesJoueurs[3].mainOrigine[1].Sorte;
    int j4c0v =     MainDesJoueurs[4].mainOrigine[0].Valeur;
    int j4c0s =     MainDesJoueurs[4].mainOrigine[0].Sorte;
    int j4c1v =     MainDesJoueurs[4].mainOrigine[1].Valeur;
    int j4c1s =     MainDesJoueurs[4].mainOrigine[1].Sorte;
    int j5c0v =     MainDesJoueurs[5].mainOrigine[0].Valeur;
    int j5c0s =     MainDesJoueurs[5].mainOrigine[0].Sorte;
    int j5c1v =     MainDesJoueurs[5].mainOrigine[1].Valeur;
    int j5c1s =     MainDesJoueurs[5].mainOrigine[1].Sorte;
    
    int f0_v  =     Flop[0].Valeur;
    int f0_s  =     Flop[0].Sorte;
    int f1_v  =     Flop[1].Valeur;
    int f1_s  =     Flop[1].Sorte;
    int f2_v  =     Flop[2].Valeur;
    int f2_s  =     Flop[2].Sorte;
    
    int t_v =       Turn.Valeur;
    int t_s =       Turn.Sorte;

    int r_v =       River.Valeur;
    int r_s =       River.Sorte;

	int NPS = NiveauPourSuivre * 1;
    string ins="insert into main values( " + Numero + ", " +  Numero_Main + ", " + Bouton + ", '" + Etape + "', '" + DateTime.Now + "'," + NPS + 
        ", NULL, "  + j0c0v  + ", " + j0c0s + ", " +j0c1v + ", " +j0c1s  + ", " +                
        j1c0v  +","  + j1c0s  + ", " + j1c1v + ", " + j1c1s + ", " + 
        j2c0v  + "," + j2c0s  + ", " + j2c1v + ", " + j2c1s + ", " +
        j3c0v  + "," + j3c0s  + ", " + j3c1v + ", " + j3c1s + ", " +
        j4c0v  + "," + j4c0s  + ", " + j4c1v + ", " + j4c1s + ", " +
        j5c0v  + "," + j5c0s  + ", " + j5c1v + ", " + j5c1s + ", " +              
        f0_v+ ", " + f0_s + ", " + f1_v   + "," + f1_s  + "," + f2_v  + "," + f2_s  + "," + t_v  + "," + t_s  + "," + r_v  + "," + r_s  + "," + 
        "NULL,NULL,NULL,NULL,NULL,NULL,NULL)";

     //MaBd.Insert(ins);
     //TrousseGlobale.AjouteHistorique(ConvertNumToNom(Bouton) + " distribue la main " + Numero_Main, Numero);
  }


 /**************************************
*
**************************************/
   private void CalculDesBlinds()
   {
	  int PB =  TrouvePetitBlind();
	  int P_K = Joueurs[PB].Capital;
	  
	  int GB =  TrouveGrosBlind();
	  int G_K = Joueurs[GB].Capital;
	  
	  int UTG_K = CapitalMaxOutreBlind(PB, GB);
      int PetitBlindTheo = CalculePetitBlindTheorique();

	  int GrosBlindTheo = 2 * PetitBlindTheo;
	  
      //TrousseGlobale.AjouteHistorique("Gros blind: " + GrosBlindTheo, Numero);
	  // On déduit les valeurs Engagement et Decision du joueur petit blind
	  if (P_K <= PetitBlindTheo) 
	  {
		 if ( (P_K > G_K) &&
		      (P_K > UTG_K))
		 {
            //Joueurs[PB].Engagement =  TrousseGlobale.maxEntre(G_K, UTG_K);
            Joueurs[PB].Capital -=	Joueurs[PB].Engagement;		
		    Joueurs[PB].Decision = "PETIT_BLIND";

		    Joueurs[GB].Engagement = Joueurs[GB].Capital; 
			Joueurs[GB].Capital = 0;
		    Joueurs[GB].Decision = "ALL_IN_SUIVRE";
         }
		 else
		 {
    		Joueurs[PB].Engagement = P_K;
	    	Joueurs[PB].Capital = 0;
		    Joueurs[PB].Decision = "ALL_IN_RELANCER";
		 }
	  }
	  else
	  {
	     // Capital du Joueur Petit blind > Petit Blind théorique
		 //Joueurs[PB].Engagement =  TrousseGlobale.minEntre( TrousseGlobale.maxEntre(G_K, UTG_K),PetitBlindTheo);

		 Joueurs[PB].Capital -= Joueurs[PB].Engagement ;
		 Joueurs[PB].Decision = "PETIT_BLIND";
	  }

      // ---------------------------------------------------
      // Maintenant le GROS Blind
	  
      if ((G_K > P_K) && (G_K > UTG_K))
      {
	    //tr(" Gros blind dominant");
	    Joueurs[GB].Decision = "GROS_BLIND";
		//Joueurs[GB].Engagement =  TrousseGlobale.minEntre(GrosBlindTheo,  TrousseGlobale.maxEntre(P_K, UTG_K));
		Joueurs[GB].Capital -= Joueurs[GB].Engagement;
		//tr(" Engagement: ( GB ) " .  Joueurs[GB].Engagement);
	  }
	  else if (G_K > P_K)
	  {
	    if (G_K > GrosBlindTheo)
		{
	        Joueurs[GB].Decision = "GROS_BLIND";
		    //Joueurs[GB].Engagement =  TrousseGlobale.minEntre(GrosBlindTheo, UTG_K);

		    Joueurs[GB].Capital -= Joueurs[GB].Engagement;
	    }
		else if (G_K > UTG_K) 
		{
		    //tr(" Gros blind domine UTG");
	        Joueurs[GB].Decision = "GROS_BLIND";
		    Joueurs[GB].Engagement = UTG_K;
		    Joueurs[GB].Capital -= Joueurs[GB].Engagement;
		}
		else
		{
		    //tr(" Gros blind domine par gros theo et UTG");
	        Joueurs[GB].Decision = "ALL_IN_RELANCER";
		    Joueurs[GB].Engagement = G_K;
		    Joueurs[GB].Capital -= Joueurs[GB].Engagement;
		}
	  }
	  else if (G_K <= P_K)
	  {
	    //tr(" Gros blind est domine par petit");
		if (G_K <= PetitBlindTheo)
		{
          Joueurs[GB].Decision = "ALL_IN_SUIVRE";
		  Joueurs[GB].Engagement = Joueurs[GB].Capital;
		  Joueurs[GB].Capital = 0;
		}
		else if (G_K <= GrosBlindTheo)
		{
	      Joueurs[GB].Decision = "ALL_IN_RELANCER";
  		  Joueurs[GB].Engagement = Joueurs[GB].Capital;
	      Joueurs[GB].Capital = 0;
		}
		else
		{
	      Joueurs[GB].Decision = "GROS_BLIND";
  		  //Joueurs[GB].Engagement = TrousseGlobale.minEntre(GrosBlindTheo, G_K);
	      Joueurs[GB].Capital -= Joueurs[GB].Engagement ;
		}
	  }
	  //NiveauPourSuivre =  TrousseGlobale.maxEntre(Joueurs[PB].Engagement,Joueurs[GB].Engagement);
	  string upd = "update main set NiveauPourSuivre = " + NiveauPourSuivre +
	          " where num_partie = " + Numero + " and num_main = " + Numero_Main;
      //MaBd.Update(upd); 
   }
     
/**************************************
*
**************************************/
   private int TrouvePetitBlind()  
   {
	  for (int i=1; i < 6; i++)
      {
	     int indice = (i + Bouton) % 6 ;
         if (Joueurs[indice].Decision != "MORT" &&
		     Joueurs[indice].Decision != "MOURANT")
         {
		    return indice;
	     }
	  }
 	  return 8;
   } 
/**************************************
*
**************************************/
   private int TrouveGrosBlind()  
   {
      int i = 1;
      for (; i < 6; i++)
      {
	     int indice = (i + Bouton) % 6 ;
         if (Joueurs[indice].Decision != "MORT" &&
		     Joueurs[indice].Decision != "MOURANT")
         {
		    break;
	     }
	  }
      i++;
	  for (; i <= 6; i++)
	  {
	    int indice = (i + Bouton) % 6;
        if (Joueurs[indice].Decision != "MORT" &&
		    Joueurs[indice].Decision  != "MOURANT")
		{
		   return indice;
		}
	  }
      MessageBox.Show("Erreur pas de gros blind");
      return -1;
   }
  /**************************************
*
**************************************/
   private int CapitalMaxOutreBlind(int PB, int GB)   
   {
      int KMax = 0;
      for (int i=0; i < 6; i++)
      {
	     if (i != PB && i != GB)
		 {
		    if (Joueurs[i].Capital > KMax)
			   KMax = Joueurs[i].Capital;
	     }
	  }
	  return KMax;
   }

/*--------------------------------------------------------------
 /
 /---------------------------------------------------------------*/
  private int CalculePetitBlindTheorique()	  
  {
     switch (Config.Type)
	 {
	    case "Aucune":
		   return 0;
		  

        case "SansCroissance":
		   return 1;
		  
		   
        case "AugmenteAddition"	:
		   int PetitBlind = 1;
		   int indice = (Numero_Main - (Numero_Main % Config.RythmeCroissance)) / Config.RythmeCroissance;
           for(int i=0; i < indice; i++)
           {
		      PetitBlind++;
			  if (PetitBlind >= (Config.Maximum) )
			     break;
		   }
		   return PetitBlind;
		  
		   
        case "AugmenteMulti"	:
		   PetitBlind = 1;
		   indice = (Numero_Main - (Numero_Main % Config.RythmeCroissance)) / Config.RythmeCroissance;
		   
           for(int i=0; i < indice; i++)
           {
		      PetitBlind *= 2;
			  if (PetitBlind >= (Config.Maximum) )
			     break;
		   }
		 
		   return PetitBlind;
	 }
     return -1;
  }




  /*--------------------------------------------------------------
   /
   /
   /---------------------------------------------------------------*/
        private void InitJoueurs()
        {
            
            string requete = "SELECT courriel, jp.pokerman, capital, position FROM joueurPartie jp, joueur j WHERE numero_partie = "+ Numero + " and j.pokerman = jp.pokerman order by position";
            List<string>[] res0;
            //res0 = MaBd.Select(requete);

            int NbJoueur = 6; // res0.Length;
            Joueurs = new Joueur[6];
            int i;

            for (i = 0; i < NbJoueur; i++)
            {
                //Joueurs[i] = new Joueur(Convert.ToInt32(res0[i][3]),
                //                      res0[i][1],
                //                      Convert.ToInt32(res0[i][2]),
                //                      res0[i][0], 0, "Attente");
            }
            for (i = NbJoueur; i < 6; i++ )
            {
                Joueurs[i] = new Joueur(i, "Inconnu", 0, "courriel@hotmele.com", 0, "MORT");
            }

            requete = "SELECT dec_j0, eng_j0, dec_j1, eng_j1, " +
                             "dec_j2, eng_j2, dec_j3, eng_j3, " +
                             "dec_j4, eng_j4, dec_j5, eng_j5 " +
                        "FROM toursParole " +
                        "WHERE num_partie = " + Numero +
                        " and  num_main = " + Numero_Main +
                        " and  Etape = '" + Etape + "'";
   
            List<string>[] res1;
            //res1 = MaBd.Select(requete);
           
          //  if (res1[0][0] != "")
          //  {
          //     for (i = 0; i < NbJoueur; i++)
          //     {
                   
          //         Joueurs[i].Decision = res1[0][i * 2];
          //         Joueurs[i].Engagement =  Convert.ToInt32(res1[0][(i*2)+1]);
          //         //if (Joueurs[i].Decision == "ABANDONNER")
          //         //  Joueurs[i].ImagePokerman = new BitmapImage(new Uri(TrousseGlobale.PathImage + "galerie/Abandonner.jpg")); 
          //         //if (Joueurs[i].Decision == "MORT")
          //         //  Joueurs[i].ImagePokerman = new BitmapImage(new Uri(TrousseGlobale.PathImage + "galerie/Mort.jpg"));
          //         //if (Joueurs[i].Decision == "ALL_IN_SUIVRE" || Joueurs[i].Decision == "ALL_IN_RELANCER")
          //         //   Joueurs[i].ImagePokerman = new BitmapImage(new Uri(TrousseGlobale.PathImage + "galerie/AllIn.jpg"));
          //     }
          //}
        }

/*--------------------------------------------------------------
 /
 /
 /---------------------------------------------------------------*/
          private void getJoueursDecision()
          {
              string select = "select dec_j0, eng_j0, dec_j1, eng_j1, dec_j2,  eng_j2,dec_j3,  eng_j3,dec_j4, eng_j4, dec_j5, eng_j5" +
               " from toursParole where num_partie = " + Numero + " and num_main = " +
               Numero_Main + " and etape = '" + Etape + "'";
              List<string>[] res;
            res = MaBd.Select(select);
            for (int i = 0; i < 6; i++)
            {
                Joueurs[i].Decision = res[0][i*2];
                Joueurs[i].Engagement = Convert.ToInt32(res[0][(i*2)+1]);
            }
          }


/**************************************
*
**************************************/
  private void VideEngagement()
  {
     if (ProchainJoueur == -2)
	 {
	    for (int i=0; i<6; i++)
		{
		   Joueurs[i].Engagement=0;
		}
	 }
  }
     
/*--------------------------------------------------------------
 /
 /
 /---------------------------------------------------------------*/
  private void TransitionUsuelle(string statut)
  {
    string JoueurSession = "Tigars"; //TrousseGlobale.PokermanJoueurLogue.ToLower();
    //string js = JoueurSession;
    //string pj = ConvertNumToNom(ProchainJoueur);
	  
    if (JoueurSession == ConvertNumToNom(ProchainJoueur).ToLower())
    {
      //if ($this->DelaiDepasse())
      //{
      //  $this->MessageDelaiAtteint('réflexion');
      //}
	  //else
	  {
        //TrousseGlobale.changeEtat("Réflexion");
        OptionsDuJoueur();
	  }
    }
    else
    {
	  if (ProchainJoueur != -3)
	  {
          //laTable.bout_Suivre.Visibility = Visibility.Collapsed;
          //laTable.bout_Abandonner.Visibility = Visibility.Collapsed;
          //laTable.bout_Relancer.Visibility = Visibility.Collapsed;
          //laTable.bout_Suivre.Visibility = Visibility.Collapsed;
          //laTable.bout_Distribuer.Visibility = Visibility.Collapsed;
          //laTable.CB_ValRelance.Visibility = Visibility.Collapsed;
          //MessageAttente();
      }
    }
  }
        

        /*--------------------------------------------------------------
         /
         /---------------------------------------------------------------*/
        public void Joue(string statut, string denouement)
        {
           switch(statut)
           {
              case "MAIN_TERMINEE_TRAITEE":
              case "MAIN_TERMINEE_TRAITEE_OUVERTE":
                TransitionDeMain();	   
                break;
		 
              case "PARTIE_TERMINEE":		 
                TransitionDePartie();
                int iGagnant = ConvertNomToNum(GagnantPartie);
                break;
 
              default: 
                TransitionUsuelle(statut);
                break;
           }
           

          if (denouement == "NeoMain")
          {
              FaireMourirMourant();
              VideTableEtRafraichit();
          }

          if (ProchainJoueur != -3)
          {
             //AfficheClavardage();
             VideEngagement();
             AfficheTable(denouement); 
          }
       }



     /*--------------------------------------------------------------
     /
     /
     /---------------------------------------------------------------*/
      private void VideTableEtRafraichit()
      {
          //laTable.CarteFlop.Visibility = Visibility.Collapsed;
          //laTable.CarteTurn.Visibility = Visibility.Collapsed;
          //laTable.CarteRiver.Visibility = Visibility.Collapsed;
      }
     /*--------------------------------------------------------------
     /
     /
     /---------------------------------------------------------------*/
      private void TransitionDePartie()
      {}
         
     /*--------------------------------------------------------------
     /
     /
     /---------------------------------------------------------------*/
      private void FaireMourirMourant()
      { 
        for (int i=0; i < 6; i++)
	    {
	      if (Joueurs[i].Decision == "MOURANT")
		  {
            string upd = "update toursParole set dec_j" + i  + "= 'MORT' " + 
		          "where num_partie = " + Numero + " and " +
				        "num_main = " + Numero_Main + " and " +
						"etape = '" + Etape + "'";
         
            Joueurs[i].Decision = "MORT";
            //MaBd.Update(upd); 
     	  }
	    }
      }

    /*--------------------------------------------------------------
 /
 /
 /---------------------------------------------------------------*/
  private void TransitionDeMain()
  {
     string req = "select date_evenement from etapes where num_partie = " + Numero + " and  " +
			       "num_main=" + Numero_Main + " and (etape='MAIN_TERMINEE_TRAITEE' or  etape='MAIN_TERMINEE_TRAITEE_OUVERTE')";
     //List<string>[] res = MaBd.Select(req); 
     //string Depuis = res[0][0]; 	
	
     int ProchainBouton = GetNextBouton();
     if (ProchainBouton == JoueurLogue)
     {
         //      //msgComplementaire = "";	
         //      if (Config.DelaiReflexion != 0)
         //      {
         //         if (1 ==2 ) //DelaiDepasse())
         //         {
         //            ;//MessageDelaiAtteint('distribuer');
         //         }
         //         //else
         //         //{    
         //         //   msgComplementaire = "Distribution auto dans <span id='ResteAvantActionAuto'>" . Config.DelaiReflexion . "</span>";
         //         //}
         //      }
         if (Terminee())
         {
             GagnantPartie = GagnantsMain;
             //   //      echo "
             //   //<body>
             //   //  <div id='EspaceDeJeu' >		 
             //   //    <div id='Option' class='Rouge'>";
             //   //      AfficheBandeau();
             //   //      echo "
             //   //      GagnantsMain gagne(nt) la main! La partie est terminée. GagnantsMain remporte les honneurs!";
             //   //      Msg = "La partie Numero est terminée. GagnantsMain remporte les honneurs!";
             //   //      CreateCourriel("PokerNirvana : Partie Numero terminée", Msg, 'BroadCast', Config.Courriel);
         }
         //     elseif (!DelaiDepasse())
         //     {
         //       echo "
         //<body onload='Moniteur(Numero, DernierRefresh, ". DELAI_VERIF_NOUVEL_EVENEMENT .")'>
         //  <div id='EspaceDeJeu' >
         //    <div id='Option' class='Verte'>";
         //       AfficheBandeau();
         //       echo "
         //      <form action='./texas.php?partieEnCours=Numero' method='post'>
         //        GagnantsMain gagne(nt) la main!		
         //        <input type='hidden' name='NouvelleMain'/> 
         //        <input type='hidden' name='Position' value='". Joueurs[JoueurLogue].Position . "'/>
         //        <input type='submit' value='Passer la prochaine main'/>
         //      </form>
         //        msgComplementaire";

         //       SetCroupier();
         //     }
         //  }
         else
         {
             MsgAttente = GagnantsMain + " gagne(nt) la main! On attend que "  + Joueurs[ProchainBouton].Pokerman + " passe les carte";
             laTable.TB_MsgAttente.DataContext = this;
             //TrousseGlobale.OuvrirEcran(laTable, "Texas");
         }
     }
     else
     {
         string msgComplementaire = "";
         //       if (Config.DelaiReflexion ==0)
         //          msgComplementaire = "";
         //       else
         //       {		 
         //         reste = ObtientDelai();
         //         msgComplementaire = "Distribution auto dans <span id='ResteAvantActionAuto'>reste </span>";
         //       }
         //       echo "
         //   <body onload='Moniteur(Numero, DernierRefresh, ". DELAI_VERIF_NOUVEL_EVENEMENT .")'>
         //     <div id='EspaceDeJeu' >		 
         //       <div id='Option' class='Rouge'>";
         //       AfficheBandeau();
         //       echo "
         MsgAttente = GagnantsMain + " gagne(nt) la main! On attend que " + Joueurs[ProchainBouton].Pokerman + " passe les cartes " + msgComplementaire;
         laTable.TB_MsgAttente.DataContext = this;
         //    }
         //    else
         //    {
         //       GagnantPartie = GagnantsMain;
         //       echo "
         //   <body>
         //     <div id='EspaceDeJeu' >		 
         //       <div id='Option' class='Rouge'>";
         //       AfficheBandeau();
         //       echo "
         //         GagnantsMain gagne la main! La partie est terminée. GagnantsMain remporte les honneurs!";
         //         Msg = "La partie Numero est finie. GagnantsMain remporte les honneurs!";
         //         CreateCourriel("PokerNirvana : Partie Numero terminée", Msg, 'BroadCast', Config.Courriel);
         //    }
         //  }
         //  echo "		
         //    </div>  ";
     }
  } 


         /*--------------------------------------------------------------
         /
         /---------------------------------------------------------------*/
          public void IncrementeNumeroMain()
          {
	        string upd = "update partie set numero_main = numero_main+1" +
	                           " where numero = " + Numero;
            //MaBd.Update(upd);
            int BoutonTmp = GetNextBouton();
            //TrousseGlobale.AjouteHistorique("Main " + (Numero_Main + 1) + " début.\nBouton:" + Joueurs[BoutonTmp].Pokerman,1);

          }

 
        /*--------------------------------------------------------------
        /
        /---------------------------------------------------------------*/
        private void OptionsDuJoueur()
        {
            TitreOptions = "Vos options:";
            AfficheHistorique();
            AfficheAttente();

            //laTable.TB_Options.DataContext = this;
            //laTable.TB_MsgAttente.DataContext = this;
            //laTable.TB_Historique.DataContext = this;
            

            //laTable.bout_Suivre.IsEnabled = true;
            //laTable.bout_Suivre.Visibility = Visibility.Visible;

            //laTable.bout_Relancer.IsEnabled = true;
            //laTable.bout_Relancer.Visibility = Visibility.Visible;
            //fixeRelance();

            //laTable.bout_Abandonner.IsEnabled = true;
            //laTable.bout_Abandonner.Visibility = Visibility.Visible;
            //laTable.bout_Distribuer.IsEnabled = false;
            //laTable.bout_Distribuer.Visibility = Visibility.Collapsed;
        }


        /*--------------------------------------------------------------
         /
         /---------------------------------------------------------------*/
        public void fixeRelance()
        {
            //for (int i = 1; i <= CalculeRelanceMaximale(); i++ )
            //    laTable.CB_ValRelance.Items.Add(i);
        }

/*--------------------------------------------------------------
 /
 /
 /---------------------------------------------------------------*/
  private int CalculeRelanceMaximale()  
  {
      int NiveauPourSuivre = 0; // GetNiveauPourSuivre(); 
	 
	 int RelanceMaxJoueur = Joueurs[JoueurLogue].Capital - ( NiveauPourSuivre - Joueurs[JoueurLogue].Engagement);
	 
	 int RelanceMaxTable = 0;
     int RelanceMaxCourante = 0;
	 for (int i=0; i<6; i++)
	 {
	   if ( Joueurs[i].Decision == "SUIVRE" ||
	        Joueurs[i].Decision == "RELANCER" ||
	        Joueurs[i].Decision == "Attente" ||
	        Joueurs[i].Decision == "PETIT_BLIND" ||
	        Joueurs[i].Decision == "GROS_BLIND")
	   {
	     if (i == JoueurLogue)
		    continue;
         RelanceMaxCourante = Joueurs[i].Capital - ( NiveauPourSuivre -Joueurs[i].Engagement);
 	     if (RelanceMaxTable < RelanceMaxCourante)
         {
            RelanceMaxTable = RelanceMaxCourante;
         }
       }
     }	   
	 	 
	 if (RelanceMaxTable < RelanceMaxJoueur)
	    return RelanceMaxTable;
	 return RelanceMaxJoueur;	
  }

        /*--------------------------------------------------------------
         /
         /---------------------------------------------------------------*/
        //public void AppliqueDecision(string DEC)
        //{
        //    //MessageBox.Show(Joueurs[JoueurLogue].Pokerman + " " + DEC);
        //    int Relance = 0;

  //    int NiveauPourSuivre = NiveauPourSuivre()();
        //    int deltaPourSuivre = NiveauPourSuivre - Joueurs[JoueurLogue].Engagement;
  
        //    switch (DEC)
        //    {
        //      case "ABANDONNER":
        //        TrousseGlobale.AjouteHistorique(Joueurs[JoueurLogue].Pokerman + " abandonne",1);
        //        Joueurs[JoueurLogue].Decision = "ABANDONNER";
        //        Joueurs[JoueurLogue].ImagePokerman = new BitmapImage(new Uri(TrousseGlobale.PathImage + "Galerie/Abandonner.jpg")); 
        //        break;

        //      case "GRATOS":
        //        TrousseGlobale.AjouteHistorique(Joueurs[JoueurLogue].Pokerman + " y va gratos", 1);
        //        DEC = "SUIVRE";
        //        break;   

        //      case "SUIVRE":
        //        if (deltaPourSuivre >= Joueurs[JoueurLogue].Capital)
        //        {
        //           DEC  = "ALL_IN_SUIVRE";
        //           TrousseGlobale.AjouteHistorique(Joueurs[JoueurLogue].Pokerman + " suit ALL_IN", 1);
        //           Joueurs[JoueurLogue].Engagement += Joueurs[JoueurLogue].Capital;
        //           Joueurs[JoueurLogue].Capital = 0;
        //        }		   
        //        else
        //        {
        //            TrousseGlobale.AjouteHistorique(Joueurs[JoueurLogue].Pokerman + " suit", 1);
        //           Joueurs[JoueurLogue].Engagement = NiveauPourSuivre;
        //           Joueurs[JoueurLogue].Capital -= deltaPourSuivre;
        //        }
        //        updateCapital(JoueurLogue);
        //        break;
      
        //      case "RELANCER":
        //        Relance = Convert.ToInt32(laTable.LB_ValRelance.SelectionBoxItem);
        //        Joueurs[JoueurLogue].Engagement = NiveauPourSuivre + Relance;
        //        Joueurs[JoueurLogue].Capital -= deltaPourSuivre + Relance;
		
        //        updateCapital(JoueurLogue);
       
        //        if (Joueurs[JoueurLogue].Capital <= 0)
        //        {  
        //          DEC = "ALL_IN_RELANCER";
        //          TrousseGlobale.AjouteHistorique(Joueurs[JoueurLogue].Pokerman + " relance ALL_IN", 1);
        //        }		   
        //        else
        //        {
        //            TrousseGlobale.AjouteHistorique(Joueurs[JoueurLogue].Pokerman + " relance de " + Relance, 1);
        //        }
        //        break;
        //      }

        //      Joueurs[JoueurLogue].Decision = DEC;
        //      Croupier leCroupier = new Croupier(Joueurs, JoueurLogue, Etape, Bouton);
        //      ProchainJoueur = leCroupier.DetermineProchainJoueur("");
        //      UpdateMise(DEC, Joueurs[JoueurLogue].Engagement, Relance, Num_Enchere);
        //}
        
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        private void DetermineProchaineEtape()
        {
            switch (Etape)
            {
                case "PRE_FLOP":
                  setEtape("FLOP");
                  break;

                case "FLOP" :
                  setEtape ("TURN");
                  break;
               
                case "TURN":
                  setEtape("RIVER");
                  break;
                
                case  "RIVER":
                  setEtape("POST_RIVER");    
                  break;
                
                case "POST_RIVER":
                  setEtape("PRE_FLOP");
                  break;
             }
             //Num_Enchere = 1;
        }


        /*--------------------------------------------------------------
        /
        /---------------------------------------------------------------*/
        public void setEtape(string etape)
        {  
          Etape = etape;
          string upd = "update main set etape = '" + Etape + "'  where num_partie = " +
              Numero + " and num_main = " + Numero_Main;
          //MaBd.Update(upd);
      }
   

    
        /*--------------------------------------------------------------
        /
        /---------------------------------------------------------------*/
          private void UpdateNiveauEnchere()
          {
            DateTime date= DateTime.Now;
            string upd="update mises set num_enchere = num_enchere+1, date_evenement = '" + date + "'" +
                  "where Num_Partie = " + Numero  + " and " +
                        "Num_Main = " + Numero_Main + " and " +
                        "Etape = '" + Etape + "'";
            //MaBd.Update(upd);
         }        
        
        /*--------------------------------------------------------------
        /   TraitementMainTermine()
        /---------------------------------------------------------------*/
        private void TraitementMainTermine(string denouement)
        {	
          //TrousseGlobale.AjouteHistorique("Main " + Numero_Main + " terminée", Numero);

          string TypeDeFin;
          if (denouement == "DOMINATION")
           TypeDeFin = "MAIN_TERMINEE_TRAITEE";
          else
            TypeDeFin = "MAIN_TERMINEE_TRAITEE_OUVERTE";	
          determineValeur();
          //GetEngagementEtNiveauPourSuivre();
  
          Gestionnaire g = new Gestionnaire(Joueurs);
  
          Joueurs = g.TransfereLesCapitaux();
  
          UpdateFinalMises();
          UpdateMainTerminee(g.GagnantsMainExport, TypeDeFin);

          
           List<int> TabMort = CollecteLesMorts();  
           if (TabMort.Count() > 0)
             UpdateLesPerdants(TabMort);
  
          if (Terminee())
          {
            //TrousseGlobale.AjouteHistorique("PARTIE TERMINÉE", Numero);
   	        Joue("PARTIE_TERMINEE", denouement);
          }
          else
          {
              //AviseProchainBouton();
            for (int i=0; i < 6; i++)
	          Joueurs[i].Engagement = 0;
   	        Joue(TypeDeFin, denouement);
          }
       }

    /*--------------------------------------------------------------
    /
    /---------------------------------------------------------------*/
       private void determineValeur()
        {
             string Message = "";
	         int NbActif = 0;

             uneMain[] LesMains = new uneMain[6];

             for (int i=0; i<6; i++)
	         {
                LesMains[i] = new uneMain();
		        LesMains[i].mainOrigine[0] = MainDesJoueurs[i].mainOrigine[0];
                LesMains[i].mainOrigine[1] = MainDesJoueurs[i].mainOrigine[1];
                LesMains[i].mainOrigine[2] = Flop[0];
                LesMains[i].mainOrigine[3] = Flop[1];
                LesMains[i].mainOrigine[4] = Flop[2];
                LesMains[i].mainOrigine[5] = Turn;
                LesMains[i].mainOrigine[6] = River;
   
                if (Joueurs[i].Decision != "ABANDONNER" &&
		            Joueurs[i].Decision != "MORT")
	            {
                   int val = Eval.CalculeValeurPostRiver(LesMains[i].mainOrigine);
                   Joueurs[i].ValeurMainCourante = val;
                   string valF = Eval.ConvertEvalEnFrancais(val);
                   Message += Joueurs[i].Pokerman + " a " + valF + " ";
		           NbActif++;
		        }
		        else
	            {
                   Joueurs[i].ValeurMainCourante = -1;
                }
	         }
        //     if (NbActif > 1)
        //         TrousseGlobale.AjouteHistorique(Message, Numero);
        }

       /*--------------------------------------------------------------
       /
       /---------------------------------------------------------------*/
       private bool Terminee()
        {
             int nbMort = 0;
             string sel ="select dec_j0, dec_j1, dec_j2, dec_j3, dec_j4, dec_j5 from toursParole "+
	                      " where num_partie= " +  Numero + " and "+
				          " num_main =" + Numero_Main + " and "+
                          "  etape = '" +  Etape + "'" + " and " +
                          " num_tour = " + Num_Tour;	
             List<string>[] TabRes;
  
             //TabRes = MaBd.Select(sel);
             //for (int i=0; i< 6; i++)
             //{
             //  if (TabRes[0][i] == "MOURANT" || 
             //      TabRes[0][i] == "MORT"    ||
             //      TabRes[0][i] == "DERNIER_PERDANT")
             //  {
             //    nbMort++;
             //  }		 
             //}
             //if (nbMort >=5)
             //{
             //   return true;
             //}
	         return false;        
        }

        /*--------------------------------------------------------------
       /
       /---------------------------------------------------------------*/
        private void UpdateFinalMises()
        {
           for (int i=0; i < 6; i++)
	       {
              string upd = "update joueurPartie set capital = " + Joueurs[i].Capital +
                           " where position = " + i + " and numero_partie = " + Numero;
              //MaBd.Update(upd); 					
           }
       }

    /*--------------------------------------------------------------
    /
    /---------------------------------------------------------------*/
       private void UpdateMainTerminee(List<int> TabGag, string TypeDeFin)
        { 
            int NbGag = TabGag.Count;
            for (int i=0; i < NbGag; i++)
	        {
	           GagnantsMain += Joueurs[TabGag[i]].Pokerman;
	           if (NbGag > 0 && NbGag-1 != i)
	             GagnantsMain += " et ";
	        }
	        DateTime DateFin = DateTime.Now;
            string upd = "update main set fin = '" + DateFin + 
                "', valeur_j0 = " +  Joueurs[0].ValeurMainCourante      +  
                " , valeur_j1 = " +  Joueurs[1].ValeurMainCourante      +  
                " , valeur_j2 = " +  Joueurs[2].ValeurMainCourante      +  
                " , valeur_j3 = " +  Joueurs[3].ValeurMainCourante      +  
                " , valeur_j4 = " +  Joueurs[4].ValeurMainCourante      +  
                " , valeur_j5 = " +  Joueurs[5].ValeurMainCourante  +      
                ", gagnant   = ' " + GagnantsMain + "'," +
                " etape     = '" + TypeDeFin + "'" +
	            " where num_partie = " + Numero + " and num_main = " + Numero_Main;
             //MaBd.Update(upd); 

	        upd = "update etapes set etape = '" + TypeDeFin + "', prochainJoueur = -2 " + 
	              " where num_partie = " + Numero + " and num_main = " + Numero_Main + 
                  " and etape = '" + Etape + "'";  
            //MaBd.Update(upd); 

	        upd = "update toursParole set etape = '" + TypeDeFin + 
                  "' where num_partie = " + Numero + " and num_main = " + Numero_Main + 
                  " and etape = '" + Etape + "' and num_tour = " + Num_Tour;  
            //MaBd.Update(upd); 

            Etape = TypeDeFin;
	        ProchainJoueur = -2;
             
        }

 /*--------------------------------------------------------------
 /
 /---------------------------------------------------------------*/
        private List<int> CollecteLesMorts()
        {
            List<int> TabMort = new List<int>();

             int i=0;
             for(i=0; i<6; i++) 
	         {
	            if (Joueurs[i].Decision == "MORT")
		          continue;
	            if (Joueurs[i].Capital == 0)
		        {
		           TabMort.Add(i);
		           Joueurs[i].Capital = -1;
		        }
             }
             return TabMort;	
        }

 /*--------------------------------------------------------------
 /
 /---------------------------------------------------------------*/
        private void UpdateLesPerdants(List<int> ListeMort)
        { 
             EnleveDeMises(ListeMort);
             string sel = "select perdant_1, perdant_2, perdant_3, perdant_4, perdant_5 from partie where numero = Numero";

             //List<string>[] TabRes  = MaBd.Select(sel); 
             string upd = "update partie set ";	 
	         int indiceMort = 0;
             DateTime maintenant = DateTime.Now;
             //for (int i = 1; i < 6; i++)
             //{
             //   if (TabRes[0][i]== "")
             //   {
             //     upd += "perdant_" + i + " = '" + Joueurs[ListeMort[indiceMort]].Pokerman + "'," +
             //              "perdant_" + i + "_date = '" + maintenant + "'";
             //     indiceMort++;
             //     if (indiceMort == ListeMort.Count)
             //     {
             //        //MaBd.Update(upd);
             //        break;
             //     }				             		  
             //     else
             //     {
             //        upd += ",";
             //     }
             //   }
             //}
        }

 /*--------------------------------------------------------------
 /
 /---------------------------------------------------------------*/
        private void EnleveDeMises(List<int> TabMort)
        {
            for (int j = 0 ; j<TabMort.Count; j++) 
	         {
                 string upd = "update mises set dec_j" + TabMort[j] + "= 'MOURANT' where " +
		               " num_partie = " + Numero  + " and " + 
				        " num_main   = " + Numero_Main + " and " + 
				        " etape      = '" + Etape + "'";
                //MaBd.Update(upd); 
		        Joueurs[TabMort[j]].Decision = "MOURANT";
             }
        }

 
        /*--------------------------------------------------------------
        /   DernierAParler()
        /---------------------------------------------------------------*/
        private bool DernierAParler()
        {
          int DernierAParler = -1; 
          if ( Etape == "PRE_FLOP")
          {
            
            for (int i=Joueurs.Length; i>=0; i--)
	        {
	          if (Joueurs[(i+2)%6].Decision != "ABANDONNER" &&
	              Joueurs[(i+2)%6].Decision != "MORT" &&
	              Joueurs[(i+2)%6].Decision != "ALL_IN_SUIVRE")
  	          {
	             DernierAParler = (i+2) %6;
		         break;
	          }
	        }  
            if ( JoueurLogue == DernierAParler)
              return true;
	        else
              return false;
          }
          else
          {
              for (int i = Joueurs.Length - 1; i >= 0; i--)
	        {
	          if (Joueurs[(i+1)%6].Decision != "ABANDONNER" &&
	              Joueurs[(i+1)%6].Decision != "MORT" &&
	              Joueurs[(i+1)%6].Decision != "ALL_IN_RELANCER" &&
	              Joueurs[(i+1)%6].Decision != "ALL_IN_SUIVRE")
  	          {
	             DernierAParler = (i+1) %6;
		         break;
	          }
	        }  
            if (JoueurLogue == DernierAParler)
	        {	  
	          return true;
	        }
	        else
	        {
              return false;		
	        }
          }
        } 

        /*--------------------------------------------------------------
        /
        /   getEtatDeLaMise()
        /
        /---------------------------------------------------------------*/
        private string getEtatDeLaMise()
        {
          int NbAbandon=0;
          int NbSuivre=0;
          int NbAttente = 0;
	      int NbAllIn = 0;
	
	      if (ProchainJoueur == -1)
	      {
	         if (Etape == "RIVER")
	         {
	           return "MAIN_TERMINEE_DEPARTAGE";
	         }
	         return "MISE_TERMINEE_MAIN_CONTINUE";
	      }
	
          //	Recensement des catégories de décisions
          for(int i =0; i<6; i++)
          {
             string d = Joueurs[i].Decision;
             if ( d.Equals("ABANDONNER") || d.Equals("MORT") )
               NbAbandon++;
             if ( d.Equals("SUIVRE"))
               NbSuivre++;
             if ( d.Equals("ALL_IN_SUIVRE") || d.Equals("ALL_IN_RELANCER"))
               NbAllIn++;
             if ( d.Equals("Attente") || d.Equals("PETIT_BLIND") || d.Equals("GROS_BLIND"))
	         {
	           if (Joueurs[i].Capital == 0)
		          // Cas ou un blind a tout mis sur son blind
		          NbAllIn++;
	           else
                  NbAttente++;   
             }			
          }

          if (NbAbandon == 5)
          {
             return "MISE_TERMINEE_MAIN_TERMINEE_DOMINATION";
          }
    
	      if (ProchainJoueur == -2)
	      {
	         return "MISE_PARALYSEE";
	      }
    
	      if (NbAttente > 0)
	      {
	         return "MISE_CONTINUE";
	      }
	   
	      // Si le joueur qui vient de prendre une décision a décidé de relancer
	      // on ne s'obstine pas: la mise va en relance
	      if (Joueurs[JoueurLogue].Decision.Equals("RELANCER"))
	      {
	        return "MISE_CONTINUE";
	      }
	
          // On vérifie s'il y a une relance dans les dernières décisions des autres joueurs
	      int[] TabSuivre = new int[6];
	      int[] TabRelance = new int [6];
          int indTabSuivre = 0;          
          int indTabRelance = 0;
	      for (int i=0; i < 6; i++)
          {      
            // Trouver la relance la plus récente 
	        int indice = (JoueurLogue-i+6) % 6;
            if (Joueurs[indice].Decision.Equals("RELANCER") ||
	            Joueurs[indice].Decision.Equals("ALL_IN_RELANCER")) 
            {
              TabRelance[indTabRelance] = indice;
              indTabRelance++;
            }

            if (Joueurs[indice].Decision.Equals("SUIVRE"))
            {
              TabSuivre[indTabSuivre] = indice;
              indTabSuivre++;
            }
          }

          if (TabRelance.Length > 1)
	      {
	         return "MISE_CONTINUE";
	      }
          if (TabRelance.Length == 1)
	      {
              if (TabSuivre.Length > 0)
	        {
                int indiceAbsoluRelance = TabRelance[TabRelance.Length - 1];
                int indiceAbsoluSuivre = TabSuivre[TabSuivre.Length - 1];
		
		      int indiceRelatifRelance = (JoueurLogue - indiceAbsoluRelance + 6) % 6;
		      int indiceRelatifSuivre  = (JoueurLogue - indiceAbsoluSuivre + 6) % 6;
		
      	      // Si l'unique relance est plus ancienne que la plus ancienne des suivre
	          // La mise est terminée, la main continue
	          if (indiceRelatifRelance > indiceRelatifSuivre)
		      {
		        // La relance à été suivie on arrête les mises
                if (Etape.Equals("RIVER"))
	            {
	              return "MAIN_TERMINEE_DEPARTAGE";
		        }
	            return "MISE_TERMINEE_MAIN_CONTINUE";
		      }
		      else
		      {
                //t(true, "un relancer précédé d'un suivre: Mise continue",  "", 'e', false);   
                return "MISE_CONTINUE";
		      }
	        }
          }  	   
	      if (NbAllIn + NbAbandon >=5)
	      {
             //tr("Paralysie type B");   
	         return "MISE_PARALYSEE";
	      }
	   
   
          if (NbAbandon + NbSuivre + NbAllIn == 6)
          {
	         if (Etape == "RIVER")
	         {
               //tr("Main terminée:départage");
               return "MAIN_TERMINEE_DEPARTAGE";
	         }
	         else
             {
               //tr("GEDLM c: this.ProchainJoueur");
               if (Etape == "RIVER")
	           {
	             //tr("Main terminée:départage c");
                 return "MAIN_TERMINEE_DEPARTAGE";
	           }
               return "MISE_TERMINEE_MAIN_CONTINUE";
	         }	 
          }
          //tr("Mise continue: Cas non attrappé");        
          return "MISE_CONTINUE";  
        }



         /*--------------------------------------------------------------
         /
         /
         /---------------------------------------------------------------*/
          private void UpdateMise( string Decision, int montantEngage, int Relance, int Num_Enchere)
          {
             DateTime date = DateTime.Now;      
              
              string upd = "update mises set niveauPourSuivre = niveauPourSuivre +" + Relance + 
                                            ", ProchainJoueur = " + ProchainJoueur +
                                            ", dec_j" + JoueurLogue + " = '" + Decision + 
                                            "',eng_j" + JoueurLogue + " = " + montantEngage +
                                            ", date_dec_J" + JoueurLogue + "='" + date.ToString() + "'" +                     
                           "where num_partie = " + Numero + " and " +
                           "num_main  =  " + Numero_Main + " and " +
                           "etape = '" + Etape + "'"; 
                        
             //MaBd.Update(upd);
             Joueurs[JoueurLogue].Decision = Decision;
          }



        /*--------------------------------------------------------------
        /
        /---------------------------------------------------------------*/
        private void updateCapital(int i)
        {
            string upd = "update joueurPartie set capital = " + Joueurs[i].Capital + 
	                     " where position = " + i + " and numero_partie = " + Numero;
            //MaBd.Update(upd);
        }
      

        /*--------------------------------------------------------------
         /
         /---------------------------------------------------------------*/
        private void AfficheAttente()
        {
            List<string>[] res;
            string req = "select etape from main where num_Partie = " + Numero + " and num_main = " + Numero_Main;
            //res = MaBd.Select(req);
            //Etape = res[0][0];

            req = "select prochainJoueur from etapes where num_Partie = " + Numero + " and num_main = " + Numero_Main + " and Etape = '" + Etape + "'";
            //res = MaBd.Select(req);
            //string Proch_J = Joueurs[Convert.ToInt32(res[0][0])].Pokerman;

            MsgAttente = "";
            laTable.TB_MsgAttente.Text = MsgAttente;
        }
        /*--------------------------------------------------------------
         /
         /---------------------------------------------------------------*/
        private void AfficheHistorique()
        { 
            List<string>[] Resultat; 
            MsgHistorique = "\n";
            //laTable.TB_Titre.Text = "Partie " + Numero + ", main " + Numero_Main + ". Joueur: " + Joueurs[JoueurLogue].Pokerman;
            string req = "select description, date from Historique where numeroPartie = " + Numero + " order by numero_evenement";
            //Resultat = MaBd.Select(req);

            //for (int i = Resultat.Length; i > 0; i--)
            {
                 
                //MsgHistorique += i + ": " + Resultat[i-1][0] + "\n";
            }
            //laTable.TB_Historique.DataContext = this;
            //laTable.TB_Historique.Text = MsgHistorique;
        }

      
         /*--------------------------------------------------------------
         /
         /---------------------------------------------------------------*/
        public void MAJMain()
        {
            List<string>[] Resultat;

            string req = "select numero_Main from partie " +
              "where numero = " + Numero;
            //Resultat = MaBd.Select(req);
            //Numero_Main = Convert.ToInt32(Resultat[0][0]);

        }
         /*--------------------------------------------------------------
         /
         /---------------------------------------------------------------*/
        public void MAJEtape()
        {
            List<string>[] Resultat;

            string req = "select Etape from main " +
              "where num_partie = " + Numero + " and num_main = " + Numero_Main;
            //Resultat = MaBd.Select(req);
            //Etape = Resultat[0][0];
        }
        /*--------------------------------------------------------------
         /
         /---------------------------------------------------------------*/
        public void MAJProchainJoueur()
        {
            List<string>[] Resultat;

            string req = "select prochainJoueur from etapes " +
              "where num_partie = " + Numero + " and num_main = " + Numero_Main +
                   " and etape = '" + Etape + "'";
            //Resultat = MaBd.Select(req);
            ProchainJoueur = 0; // Convert.ToInt32(Resultat[0][0]);
        }
        /*--------------------------------------------------------------
         /
         /---------------------------------------------------------------*/
        private void MessageAttente()
        { 
            List<string>[] Resultat;
 
            string req = "select prochainJoueur,  date_evenement from etapes " + 
              "where num_partie = " + Numero + " and num_main = " + Numero_Main +
                   " and etape = '" + Etape + "'";
            //Resultat = MaBd.Select(req);
            //string  Depuis = Resultat[0][1];

            string msg = "";
            //if (Resultat[0][0] == "-2")
            if (true)
            {
                int ProchainBouton = GetNextBouton();
                //if (TrousseGlobale.PosJoueurLogue == ProchainBouton)
                //{
                //    msg = "A toi de passer les cartes.\n";
                //    laTable.bout_Distribuer.Visibility = Visibility.Visible;
                //    laTable.bout_Distribuer.IsEnabled = true;
                //}
                //else
                //{
                //    msg = "On attend que " + Joueurs[ProchainBouton].Pokerman + " distribue les cartes\n";
                //}
            }
            else
            {
                //msg = "On attend la décision de \n"
                // + Joueurs[Convert.ToInt32(Resultat[0][0])].Pokerman
                // + "\n depuis : " + Depuis;
            }
            TitreOptions = "Vos options:";

            MsgAttente = msg;
            //laTable.TB_MsgAttente.DataContext = this;
            laTable.TB_MsgAttente.Text = msg;
            //laTable.TB_Historique.DataContext = this; 
            //laTable.TB_Historique.Text = MsgHistorique;
            laTable.TB_Options.DataContext = this;
        }

        

        /*--------------------------------------------------------------
         /
         /---------------------------------------------------------------*/
        private void AfficheTable(string denouement)
        {
            AfficheHistorique();
            ReCharge(1964);
       
            afficheBouton();
            bool cache = true;

            for (int i = 0; i < 6; i++)
            {
                
                if (denouement == "PARALYSIE" ||
                    denouement == "DEPARTAGE")
                {
                    cache = false;
                }
                if (denouement == "Texas" || 
                    denouement == "Synchro")
                {
                    if (Etape == "MAIN_TERMINEE_TRAITEE_OUVERTE")
                    {
                        if (Joueurs[JoueurLogue].Decision == "SUIVRE" ||
                            Joueurs[JoueurLogue].Decision == "ALL_IN_SUIVRE" ||
                            Joueurs[JoueurLogue].Decision == "ALL_IN_RELANCER" ||
                            Joueurs[JoueurLogue].Decision == "RELANCER" ||
                            Joueurs[JoueurLogue].Decision == "MOURANT")
                        {
                            cache = false;
                        }
                    }
                }
                //if (Joueurs[i].Decision == "ABANDONNER")
                //{
                //    cache = true;
                //}
                //if (JoueurLogue == Joueurs[i].Position)
                //{
                //    cache = false;
                //}
                DonneDataContext(i, cache);
                cache = true;
            }
            if (OnAFranchit("FLOP"))
            {
                //laTable.CarteFlop.Visibility = Visibility.Visible;
                //laTable.CarteFlop.DataContext = MainDesJoueurs[0];
            }
            else
            {
                //laTable.CarteFlop.Visibility = Visibility.Collapsed;
            }
            if (OnAFranchit("TURN"))
            {
                //laTable.CarteTurn.Visibility = Visibility.Visible;
                //laTable.CarteTurn.DataContext = MainDesJoueurs[0];
            }
            else
            {
                //laTable.CarteTurn.Visibility = Visibility.Collapsed;
            }
            if (OnAFranchit("RIVER"))
            {
                //laTable.CarteRiver.Visibility = Visibility.Visible;
                //laTable.CarteRiver.DataContext = MainDesJoueurs[0];
            }
            else
            {
                //laTable.CarteRiver.Visibility = Visibility.Collapsed;
            }
         }

     
        /*--------------------------------------------------------------
        /
        /---------------------------------------------------------------*/
        public void Rafraichit()
        {
            AfficheHistorique();
            laTable.DataContext = this;
        }
        /*--------------------------------------------------------------
        /
        /---------------------------------------------------------------*/
        private void DonneDataContext(int j, bool Cache)
        {
            //if (Cache)
            //{
            //    MainDesJoueurs[j].mainOrigine[0].CacheImage();
            //    MainDesJoueurs[j].mainOrigine[1].CacheImage();
            //}

            //switch (j)
            //{
            //    case 0:
            //      if (Joueurs[j].Decision != "MORT")
            //      {
            //          laTable.J_A.DataContext = Joueurs[j];
            //          laTable.CarteA.DataContext = MainDesJoueurs[j];
            //      }
            //      else
            //      {
            //          laTable.J_A.Visibility = Visibility.Collapsed;
            //      }
            //      break; 

            //    case 1:
            //      if (Joueurs[j].Decision != "MORT")
            //      {
            //          laTable.J_B.DataContext = Joueurs[j];
            //          laTable.CarteB.DataContext = MainDesJoueurs[j];
            //      }
            //      else
            //      {
            //          laTable.J_B.Visibility = Visibility.Collapsed;
            //      }
            //      break; 
            //    case 2:
            //      if (Joueurs[j].Decision != "MORT")
            //      {
            //          laTable.J_C.DataContext = Joueurs[j];
            //          laTable.CarteC.DataContext = MainDesJoueurs[j];
            //      }
            //      else
            //      {
            //          laTable.J_C.Visibility = Visibility.Collapsed;
            //      }
            //      break; 
            //    case 3:
            //      if (Joueurs[j].Decision != "MORT")
            //      {
            //          laTable.J_D.DataContext = Joueurs[j];
            //          laTable.CarteD.DataContext = MainDesJoueurs[j];
            //      }
            //      else
            //      {
            //          laTable.J_D.Visibility = Visibility.Collapsed;
            //      }
            //      break; 
            //    case 4:
            //      if (Joueurs[j].Decision != "MORT")
            //      {
            //          laTable.J_E.DataContext = Joueurs[j];
            //          laTable.CarteE.DataContext = MainDesJoueurs[j];
            //      }
            //      else
            //      {
            //          laTable.J_E.Visibility = Visibility.Collapsed;
            //      }
            //      break; 
            //    case 5:
            //      if (Joueurs[j].Decision != "MORT")
            //      {
            //          laTable.J_F.DataContext = Joueurs[j];
            //          laTable.CarteF.DataContext = MainDesJoueurs[j];
            //      }
            //      else
            //      {
            //          laTable.J_F.Visibility = Visibility.Collapsed;
            //      }
            //      break; 
            //    }
        }

        /*--------------------------------------------------------------
        /
        /---------------------------------------------------------------*/
        private void afficheBouton()
        {
            laTable.Bouton_A.Visibility = Visibility.Collapsed;
            laTable.Bouton_B.Visibility = Visibility.Collapsed;
            laTable.Bouton_C.Visibility = Visibility.Collapsed;
            laTable.Bouton_D.Visibility = Visibility.Collapsed;
            laTable.Bouton_E.Visibility = Visibility.Collapsed;
            //laTable.Bouton_F.Visibility = Visibility.Collapsed;

            //switch (Bouton)
            //{
            //    case 0:
            //        laTable.Bouton_A.Visibility = Visibility.Visible;
            //        break;
            //    case 1:
            //        laTable.Bouton_B.Visibility = Visibility.Visible;
            //        break;
            //    case 2:
            //        laTable.Bouton_C.Visibility = Visibility.Visible;
            //        break;
            //    case 3:
            //        laTable.Bouton_D.Visibility = Visibility.Visible;
            //        break;
            //    case 4:
            //        laTable.Bouton_E.Visibility = Visibility.Visible;
            //        break;
            //    case 5:
            //        laTable.Bouton_F.Visibility = Visibility.Visible;
            //        break;
            //}
        }

        
 /*--------------------------------------------------------------
 /
 /---------------------------------------------------------------*/
          private bool OnAFranchit(string etape)
          {
             string sel = "select 1 from etapes where etape = '" + etape + "' and " +
	                    " num_partie = " + Numero + " and " + 
				        " num_main = " + Numero_Main;
             List<string>[] res;

            // res = MaBd.Select(sel);
             //if ( res[0][0] != "")
             //{
	            
             //   return true;
             //}
             //else
             //{
             //   string etape_ante = GetEtapePrecedente(etape);
             //   sel =  "select 1 from etapes where etape = '" + etape_ante + "' and " +
             //           " num_partie = " + Numero + " and " + 
             //           " num_main = " + Numero_Main;
                    
             //   res = MaBd.Select(sel);
             //   if (res[0][0] != "")
             //   {
             //      sel = "select 1 from etapes where (etape = 'MAIN_TERMINEE_TRAITEE' or " +
             //                                         " etape = 'MAIN_TERMINEE_TRAITEE_OUVERTE') and "+
             //              "num_partie = " + Numero + " and " +
             //              "num_main = " + Numero_Main;
             //      res = MaBd.Select(sel);
             //      if (res[0][0] != "")
             //      {
             //         return true;   
             //      }
             //   }
             //}
             return false;		
          }

/*--------------------------------------------------------------
 /
 /---------------------------------------------------------------*/
          private string GetEtapePrecedente(string etape)
          {
             if (etape == "FLOP")  return "PRE_FLOP";
             if (etape == "TURN")  return "FLOP";
             if (etape == "RIVER") return "TURN";
             if (etape == "POST_RIVER") return "RIVER";
             return "Inconnu";
          }

        private void ChargeCarte()
        {
          List<string>[] Resultat; 
          string req="select " +
                 "j0_c0_v, j0_c0_s , j0_c1_v , j0_c1_s , " +
                 "j1_c0_v ,j1_c0_s , j1_c1_v , j1_c1_s , " +
                 "j2_c0_v ,j2_c0_s , j2_c1_v , j2_c1_s ," +
                 "j3_c0_v ,j3_c0_s , j3_c1_v , j3_c1_s ," +
                 "j4_c0_v ,j4_c0_s , j4_c1_v , j4_c1_s ," +
                 "j5_c0_v ,j5_c0_s , j5_c1_v , j5_c1_s," +
                 "f_c0_v, f_c0_s, f_c1_v,f_c1_s, f_c2_v, f_c2_s," +
	             "t_v, t_s, r_v,r_s " +
                 "from main where num_partie= " + Numero  + " and num_main = " + Numero_Main;

         // Resultat = MaBd.Select(req);

          MainDesJoueurs = new uneMain[6];
          MainDesJoueurs[0] = new uneMain();
          MainDesJoueurs[1] = new uneMain();
          MainDesJoueurs[2] = new uneMain();
          MainDesJoueurs[3] = new uneMain();
          MainDesJoueurs[4] = new uneMain();
          MainDesJoueurs[5] = new uneMain();

          if (false) // Resultat[0][0] == "")
          {
              MessageBox.Show("On a pas frappé de main avec:\n" + req + "\nAttente de 5 sec");
              Thread.Sleep(5);
              //Resultat = MaBd.Select(req);
          }


          //MainDesJoueurs[0].mainOrigine[0] = new Carte(Resultat[0][0], Resultat[0][1]);
          //MainDesJoueurs[0].mainOrigine[1] = new Carte(Resultat[0][2], Resultat[0][3]);
          //MainDesJoueurs[1].mainOrigine[0] = new Carte(Resultat[0][4], Resultat[0][5]);
          //MainDesJoueurs[1].mainOrigine[1] = new Carte(Resultat[0][6], Resultat[0][7]);
          //MainDesJoueurs[2].mainOrigine[0] = new Carte(Resultat[0][8], Resultat[0][9]);
          //MainDesJoueurs[2].mainOrigine[1] = new Carte(Resultat[0][10], Resultat[0][11]);
          //MainDesJoueurs[3].mainOrigine[0] = new Carte(Resultat[0][12], Resultat[0][13]);
          //MainDesJoueurs[3].mainOrigine[1] = new Carte(Resultat[0][14], Resultat[0][15]);
          //MainDesJoueurs[4].mainOrigine[0] = new Carte(Resultat[0][16], Resultat[0][17]);
          //MainDesJoueurs[4].mainOrigine[1] = new Carte(Resultat[0][18], Resultat[0][19]);
          //MainDesJoueurs[5].mainOrigine[0] = new Carte(Resultat[0][20], Resultat[0][21]);
          //MainDesJoueurs[5].mainOrigine[1] = new Carte(Resultat[0][22], Resultat[0][23]);

          //Flop = new Carte[3];
          //Flop[0] = new Carte(Resultat[0][24], Resultat[0][25]);
          //Flop[1] = new Carte(Resultat[0][26], Resultat[0][27]);
          //Flop[2] = new Carte(Resultat[0][28], Resultat[0][29]);
          //Turn    = new Carte(Resultat[0][30], Resultat[0][31]);
          //River   = new Carte(Resultat[0][32], Resultat[0][33]);

          //// Bizarre. Mais pour afficher le FLOP il faut faire cette passe-passe
          //MainDesJoueurs[0].ImageFlop0 = Flop[0].imgCarte;
          //MainDesJoueurs[0].ImageFlop1 = Flop[1].imgCarte;
          //MainDesJoueurs[0].ImageFlop2 = Flop[2].imgCarte;

          //MainDesJoueurs[0].ImageTurn = Turn.imgCarte;
          //MainDesJoueurs[0].ImageRiver= River.imgCarte;
        }


        /*--------------------------------------------------------------
        /---------------------------------------------------------------*/

        private void distribueMains()
        {
            MainDesJoueurs = new uneMain[6];
            for (int j = 0; j < 6; j++)
            {
               MainDesJoueurs[j] = new uneMain();
               MainDesJoueurs[j].mainOrigine = new Carte[7];
               //if (Joueurs[j].Decision == "MORT")
               //{
               //   MainDesJoueurs[j].mainOrigine[0] = new Carte(-1, -1);
               //   MainDesJoueurs[j].mainOrigine[1] = new Carte(-1, -1);
               //   continue;
               //}
               MainDesJoueurs[j].mainOrigine[0] = lePaquet.donneProchaineCarte();
               MainDesJoueurs[j].mainOrigine[1] = lePaquet.donneProchaineCarte();
               MainDesJoueurs[j].mainOrigine[2] = new Carte(1, 1);
               MainDesJoueurs[j].mainOrigine[3] = new Carte(1, 1);
               MainDesJoueurs[j].mainOrigine[4] = new Carte(1, 1);
               MainDesJoueurs[j].mainOrigine[5] = new Carte(1, 1);
               MainDesJoueurs[j].mainOrigine[6] = new Carte(1, 1);
            }
            Flop = new Carte[3];
            Flop[0] = lePaquet.donneProchaineCarte();
            MainDesJoueurs[0].mainOrigine[2] = Flop[0];
            Flop[1] = lePaquet.donneProchaineCarte();
            MainDesJoueurs[0].mainOrigine[3] = Flop[1];
            Flop[2] = lePaquet.donneProchaineCarte();
            MainDesJoueurs[0].mainOrigine[4] = Flop[2];
            Turn = lePaquet.donneProchaineCarte();
            MainDesJoueurs[0].mainOrigine[5] = Turn;
            River = lePaquet.donneProchaineCarte();
            MainDesJoueurs[0].mainOrigine[6] = River;

            DateTime DateDebut = DateTime.Now;
        }
       
      
        /*--------------------------------------------------------------
        /
        /---------------------------------------------------------------*/
        private int GetNextBouton()
        {
          for (int i=1; i < 6; i++)
	      {
	         int index = (i+ Bouton) % 6;
	         if (!Joueurs[index].Decision.Equals("MOURANT") &&
		         !Joueurs[index].Decision.Equals("MORT"))
		     {
		       return index;
		     }
	       }
          return -1;
        }
    }
}

