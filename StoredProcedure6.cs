using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class finalGamesOutput
    {
        public void finalOutput(string connString)
        {
            
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                MySqlDataAdapter da;
                MySqlCommand command = new MySqlCommand();
                MySqlCommand cmd2 = new MySqlCommand();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                command.Connection = conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "finalOutput";

                da = new MySqlDataAdapter(command);
                da.Fill(ds,"VG_AllFiltered"); 
                dt = ds.Tables["VG_AllFIltered"];
                Console.WriteLine("We think these games are right for you!");
                Console.WriteLine("-------------------------------------------------------------------------------");
                foreach (DataRow dr in dt.Rows){
                    Console.WriteLine(dr["Title"] + " " + dr["System_Name"]+" "+ dr["Release_Year"] + " " +
                    dr["Age_Rating"] + " " + dr["Genre"] + " " + dr["Developer"] + " " + 
                    dr["avgScore"] + " " + dr["totalReviews"]);
                }
                Console.WriteLine("-------------------------------------------------------------------------------");
                cmd2.Connection = conn;
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "drop table if exists VG_AllFiltered";
                cmd2.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
        }
    }
}