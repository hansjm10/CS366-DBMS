using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class PStoredProcedure4
    {
        public void SP4(string connString)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                string queryString = "select g.Title, g.Release_Year, g.Age_Rating, g.Genre, g.Developer " + 
                                     "from Video_Games g where g.Game_ID in (select m.Game_ID from Made_For m "
                                     + "where m.System_Name in (select o.System_Name from Owns o where o.SystemOwned = systemOwned));";
                
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                da.SelectCommand = new MySqlCommand("Call filterSystem", conn);
                da.Fill(ds,"Video_Games");
                dt = ds.Tables["Video_Games"];
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine(dr["Title"] + " " + dr["Release_Year"] + " " + dr["Age_Rating"] + " " + dr["Genre"] + " " + dr["Developer"]);
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