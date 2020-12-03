using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class StoredProcedure6
    {
        public void SP6(string connString)
        {
            
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                conn.Open();
                MySqlDataAdapter da;
                MySqlCommand command = new MySqlCommand();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();


                command.Connection = conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "finalOutput";

                da = new MySqlDataAdapter(command);
                da.Fill(ds,"Video_Games"); 
                dt = ds.Tables["Video_Games"];
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine(dr["Title"] + " " + dr["System_Name"]+" "+ dr["Release_Year"] + " " +
                    dr["Age_Rating"] + " " + dr["Genre"] + " " + dr["Developer"] + " " + 
                    dr["avgScore"] + " " + dr["totalReviews"]);
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