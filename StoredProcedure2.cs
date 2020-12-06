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
                
                MySqlDataAdapter da = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                
                command.Connection = conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "filterAge13";

                da = new MySqlDataAdapter(command);
                da.Fill(ds,"Video_Games");
                dt = ds.Tables["Video_Games"];
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