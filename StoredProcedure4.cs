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
                                     + "where m.System_Name = systemOwned;";
                
                string systemInput = "";
                Console.WriteLine("Enter system owned: ");
                systemInput = Console.ReadLine();
                
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                //I don't believe SelectCommand works like this, I believe it is just one query that needs to be
                //Sent through. It is only for initiating stored procedures.
                //We could try setting Parameters.AddWithValue(@SystemOwned,systemInput) and then ExecutingNonQuery() like how the original example did it.
                da.SelectCommand = new MySqlCommand("set @systemOwned = " + systemInput + ";");
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