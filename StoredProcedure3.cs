using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class filterM
    {
        public void filterAge17(string connString)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                da.SelectCommand = new MySqlCommand("Call filterAge17", conn);
                da.Fill(ds,"VG_AgeFiltered");
                dt = ds.Tables["VG_AgeFiltered"];
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
        }
    }
}