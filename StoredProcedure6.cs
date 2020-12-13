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
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                command.Connection = conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "finalOutput";

                da = new MySqlDataAdapter(command);
                da.Fill(ds,"VG_AllFiltered"); 
                dt = ds.Tables["VG_AllFiltered"];
                Console.WriteLine("\nWe think these games are right for you!");
                Console.WriteLine("Title\t\tSystem\tRelease Year\tAge Rating\tGenre\tDeveloper\tMetacritic Score\t# Reviews\tGameID");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------");
                foreach (DataRow dr in dt.Rows){
                    Console.WriteLine(dr["Title"] + "\t\t" + dr["System_Name"]+"\t"+ dr["Release_Year"] + "\t" +
                    dr["Age_Rating"] + "\t" + dr["Genre"] + "\t" + dr["Developer"] + "\t" + 
                    dr["Score"] + "\t\t" + dr["Num_of_Reviews"]+ "\t\t" + dr["Game_ID"]);
                }
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
        }
    }
}