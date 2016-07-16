///////////////////////////////////////////////////////////////////////////////
// class BaseDeDonnee.BDService 
// 
// Auteur: Alain Martel
// Date:   Juillet 2013
// Desciption: Cette classe offre plusieurs services d'accès à une BD mysql: 
//             connexion, insertion, mise à jour, et selection
// Modifications
// 2015-jun-16  Adaptation pour Nirvana
///////////////////////////////////////////////////////////////////////////////


using System;
using System.Windows;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Configuration;


namespace PokerNirvana_MVVM_ORM.ViewModel
{
    public class BDService
    {


        private MySqlConnection connexion;

        //--------------------------------------
        //
        //--------------------------------------
        public BDService()
        {
            try
            {
                string connexionString;
                connexionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
                connexion = new MySqlConnection(connexionString);
            }
            catch (Exception e)
            {
                MessageBox.Show("Connexion défectueuse:" + e.ToString());
            }
        }

        //--------------------------------------
        //
        //--------------------------------------
        private bool OpenConnection()
        {
            try
            {
                connexion.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show("Impossible d'ouvrir la connexion:" + ex.ToString());
                return false;
            }
        }

        //--------------------------------------
        //
        //--------------------------------------
        private bool CloseConnection()
        {
            try
            {
                connexion.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return false;
            }
        }

        //--------------------------------------
        //
        //--------------------------------------
        public void truncate(string table)
        {
            try
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand("truncate table " + table, connexion);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur dans le truncate:\n" + e.ToString());
            }

        }

        //--------------------------------------
        //
        //--------------------------------------
        public long Insert(string insertQuery)
        {
            long retVal = 0;
            try
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(insertQuery, connexion);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    retVal = cmd.LastInsertedId;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur dans le insert:\n" + e.ToString());
            }
            return retVal;
        }

        //--------------------------------------
        //
        //--------------------------------------
        public int Update(string query)
        {
            int NbLigneMAJ = 0;
            try
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = connexion;

                    NbLigneMAJ = cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                return NbLigneMAJ;
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur dans le update:\n" + e.ToString());
            }
            return 0;
        }

        //--------------------------------------
        //
        //--------------------------------------
        public int Delete(string DelQuery)
        {
            try
            {
                int NbLigneDetruite = 0;
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(DelQuery, connexion);
                    NbLigneDetruite = cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                return NbLigneDetruite;
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur dans le Delete:\n" + e.ToString());
            }
            return 0;
        }

        //--------------------------------------
        //
        //--------------------------------------
        public List<string>[] Select(string query)
        {
            List<string> InfoBrut = new List<string>();
            int NbLigneTrouvees = 0;
            int NbChamps = 0;
            try
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connexion);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    NbChamps = dataReader.FieldCount;
                    while (dataReader.Read())
                    {
                        ///On a le record courant qui contient NbChamps
                        for (int i = 0; i < NbChamps; i++)
                        {
                            InfoBrut.Add(dataReader[i] + "");
                        }
                        NbLigneTrouvees++;
                    }
                    dataReader.Close();
                    this.CloseConnection();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("(198) Erreur lors d'un select:\n" + e.ToString());
                return null;
            }

            // On construit le tableau des données à retourner : Un tableau de liste de string
            List<string>[] ListeEnregistrements;
            if (NbLigneTrouvees == 0)
            {
                // Cas où le select ne retourne rien
                ListeEnregistrements = new List<string>[1];
                ListeEnregistrements[0] = new List<string>();
                ListeEnregistrements[0].Add("");
            }
            else
            {
                ListeEnregistrements = new List<string>[NbLigneTrouvees];
                //Chaque case du tableau est un enregistrement sous forme de List<string>. 
                // Les string de la liste sont les champs de l'enregistrement
                for (int i = 0; i < NbLigneTrouvees; i++)
                {
                    ListeEnregistrements[i] = new List<string>();
                    for (int j = 0; j < NbChamps; j++)
                    {
                        int indice = j + (i * NbChamps);
                        ListeEnregistrements[i].Add(InfoBrut[indice]);
                    }
                }
            }
            return ListeEnregistrements;
        }
    }
}
