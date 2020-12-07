using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class PreferencesList
    {
        public bool prefsList(string connString)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                showPrefsList sp7 = new showPrefsList();
                showMoreInfo sp8 = new showMoreInfo();
                finalOutputFromPreferences sp9 = new finalOutputFromPreferences();
                
                
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
            return false;
        }
    }
}