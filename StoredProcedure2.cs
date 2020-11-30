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
                
                string queryString = "Select Title, Release_Year, Age_Rating, Publisher, Developer from Video_Games where (Age_Rating != 'T' and Age_Rating != 'M')";
                MySqlCommand cmd = new MySqlCommand(queryString,conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Console.WriteLine(String.Format("{0},{1},{2},{3},{4}",
                    reader["Title"], reader["Release_Year"], reader["Age_Rating"], reader["Publisher"], reader["Developer"]));
                }
                // cmd.Connection = conn;
                // //Call Stored Procedure Name
                // cmd.CommandText = "filterAge";
                // cmd.CommandType = CommandType.StoredProcedure;

                // cmd.Parameters.Add("@title_param", MySqlDbType.VarChar);
                // cmd.Parameters["@title_param"].Direction = ParameterDirection.Output;

                // cmd.Parameters.Add("@releaseYear_param", MySqlDbType.VarChar);
                // cmd.Parameters["@releaseYear_param"].Direction = ParameterDirection.Output;

                // cmd.Parameters.Add("@ageRating_param", MySqlDbType.VarChar);
                // cmd.Parameters["@ageRating_param"].Direction = ParameterDirection.Output;

                // cmd.Parameters.Add("@pub_param", MySqlDbType.VarChar);
                // cmd.Parameters["@pub_param"].Direction = ParameterDirection.Output;

                // cmd.Parameters.Add("@dev_param", MySqlDbType.VarChar);
                // cmd.Parameters["@dev_param"].Direction = ParameterDirection.Output;

                // cmd.ExecuteNonQuery();

                // Console.WriteLine("Title: "+cmd.Parameters["@title_param"].Value);
                // Console.WriteLine("Release Year: " + cmd.Parameters["@releaseYear_param"].Value);
                // Console.WriteLine("Rating: "+ cmd.Parameters["@ageRating_param"].Value);
                // Console.WriteLine("Publisher: " + cmd.Parameters["@pub_param"].Value);
                // Console.WriteLine("Developer: " + cmd.Parameters["@dev_param"].Value);
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