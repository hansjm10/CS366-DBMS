using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class finalOutputFromPreferences
    {
        public void filterPreferences(string connString, string userID, string rY, string ageRating, string gen, string dev)
        {
           MySqlConnection conn = new MySqlConnection(connString);

            try
            {

                MySqlDataAdapter da;
                MySqlCommand command = new MySqlCommand();
                MySqlParameter param;
                MySqlParameter param2;
                MySqlParameter param4;
                MySqlParameter param5;
                MySqlParameter param6;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();


                command.Connection = conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "filterPreferences";

                param = new MySqlParameter("@userID_p", userID);
                param2 = new MySqlParameter("@rY_p", rY);
                param4 = new MySqlParameter("@ageRating_p", ageRating);
                param5 = new MySqlParameter("@gen_p", gen);
                param6 = new MySqlParameter("@dev_p", dev);

                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;
                command.Parameters.Add(param);

                param2.Direction = ParameterDirection.Input;
                param2.DbType = DbType.String;
                command.Parameters.Add(param2);
                
                param4.Direction = ParameterDirection.Input;
                param4.DbType = DbType.String;
                command.Parameters.Add(param4);
                
                param5.Direction = ParameterDirection.Input;
                param5.DbType = DbType.String;
                command.Parameters.Add(param5);
                
                param6.Direction = ParameterDirection.Input;
                param6.DbType = DbType.String;
                command.Parameters.Add(param6);

                da = new MySqlDataAdapter(command);
                da.Fill(ds,"Video_Games"); 
                dt = ds.Tables["Video_Games"];
                Console.WriteLine("\nWe think these games are right for you!");
                Console.WriteLine("Title\tSystem\tRelease Year\tAge Rating\tGenre\tDeveloper\tMetacritic Score\t# Reviews\tGameID");
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
                foreach (DataRow dr in dt.Rows){
                    Console.WriteLine(dr["Title"] + "\t" + dr["System_Name"]+"\t"+ dr["Release_Year"] + "\t" +
                    dr["Age_Rating"] + "\t" + dr["Genre"] + "\t" + dr["Developer"] + "\t" + 
                    dr["Score"] + "\t\t" + dr["Num_of_Reviews"]+ "\t" + dr["Game_ID"]);
                }
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
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