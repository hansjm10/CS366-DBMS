using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class StoredProcedure5
    {
        public void SP5(string connString)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                string queryString = "Select Title, Release_Year, Age_Rating, Genre, Developer into title, releaseYear, ageRating, pub, dev" +
                                        "from Video_Games" +
                                        "where (Release_Year >= rY1 and Release_Year < rY2 or (Developer = dev) or (Publisher = pub)";
                
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                da.SelectCommand = new MySqlCommand("Call filterAnswers", conn);
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