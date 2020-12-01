using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class StoredProcedure3
    {
        public void SP3(string connString)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            // try
            // {
            //     // Console.WriteLine("Connecting to MySQL...");
            //     // conn.Open();
            //     // cmd.Connection = conn;
            //     // cmd.CommandText = "delimiter $$ drop procedure if exists filterAge17;";
            //     // cmd.ExecuteNonQuery();
            //     // cmd.CommandText = "create procedure filterAge17(" +
            //     //                   "OUT title varchar(255), OUT releaseYear varchar(255), OUT ageRating varchar(255), OUT pub varchar(255), OUT dev varchar(255))" +
            //     //                   "begin select Title, Release_Year, Age_Rating, Publisher, Developer into title, releaseYear, ageRating, pub, dev from Video_Gameswhere (Age_Rating != ‘M’);" +
            //     //                   "end $$delimiter ;";

            //     // cmd.ExecuteNonQuery();
            // }
            // catch (MySqlException ex)
            // {
            //     Console.WriteLine ("Error " + ex.Number + " has occurred: " + ex.Message);
            // }
            // conn.Close();
            // Console.WriteLine("Connection closed.");
            try
            {
                string queryString = "Select Title, Release_Year, Age_Rating, Genre, Developer from Video_Games where Age_Rating != 'M' limit 20";
                
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                da.SelectCommand = new MySqlCommand("Call filterAge17", conn);
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