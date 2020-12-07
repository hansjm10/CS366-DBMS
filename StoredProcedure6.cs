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
        public DataTable finalOutput(string connString)
        {
            
            MySqlConnection conn = new MySqlConnection(connString);
            DataTable dt = new DataTable();

            try
            {
                MySqlDataAdapter da;
                MySqlCommand command = new MySqlCommand();
                DataSet ds = new DataSet();

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
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
            return dt;
        }
    }
}