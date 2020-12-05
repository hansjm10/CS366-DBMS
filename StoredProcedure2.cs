using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    //Test
        class filterTandM
    {
        public void filterAge13(String connString)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                da.SelectCommand = new MySqlCommand("Call filterAge13", conn);
                da.Fill(ds,"VG_AgeFiltered");
                dt = ds.Tables["VG_AgeFiltered"];
                foreach (DataRow dr in dt.Rows) //Once we know SP works, we can get rid of this loop.
                {
                    Console.WriteLine(dr["Game_ID"] + " " + dr["Title"] + " " + dr["Release_Year"] + " " 
                                    + dr["Age_Rating"] + " " + dr["Genre"] + " " + dr["Developer"]);
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
            conn.Close();
            Console.WriteLine("Done.");
        }
    }
}   