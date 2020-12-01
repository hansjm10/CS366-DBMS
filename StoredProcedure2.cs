using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class StoredProcedure2
    {
        public void SP2(String connString)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            // try
            // {
            //     conn.Open();
            //     String query =  "delimiter $$\n" +
            //                     "drop procedure if exists filterAge; \n" +
            //                     "create procedure filterAge\n" +
            //                         "(OUT title VARCHAR(255)\n," +
            //                         "OUT releaseYear VARCHAR(255),\n" +
            //                         "OUT ageRating VARCHAR(255),\n" +
            //                         "OUT pub VARCHAR(255),\n" +
            //                         "OUT dev VARCHAR(255))\n" +
            //                     "begin\n" +  
            //                         "select\n" + 
            //                             "Title, Release_Year, Age_Rating, Publisher, Developer into title, releaseYear, ageRating, pub, dev\n" +
            //                             "from Video_Games where (Age_Rating != 'T') and (Age_Rating != 'M');\n" +
            //                     "end $$\n" +
            //                     "delimiter ;";
            //     Console.WriteLine(query);
            //     MySqlCommand cmd = new MySqlCommand(query,conn);
            //     cmd.ExecuteNonQuery();
            //     Console.WriteLine("Executed Successfully");
            // }
            // catch (MySqlException ex)
            // {
            //     Console.WriteLine ("Error " + ex.Number + " has occurred: " + ex.Message);
            // }
            // conn.Close();
            // Console.WriteLine("Connection closed.");
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                
                string queryString = "Select Title, Release_Year, Age_Rating, Genre, Developer from Video_Games where (Age_Rating != 'T' and Age_Rating != 'M') limit 20";
                
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                da.SelectCommand = new MySqlCommand("Call filterAge13", conn);
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