using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class StoredProcedure7
    {
        public void SP7(string connString)
        {
            
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                conn.Open();
                Console.WriteLine("Enter user ID: ");
                string userID = Console.ReadLine();

                MySqlDataAdapter da;
                MySqlCommand command = new MySqlCommand();
                MySqlParameter param;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();


                command.Connection = conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "showPreferences";

                param = new MySqlParameter("@userID_p", userID);

                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;
                command.Parameters.Add(param);

                da = new MySqlDataAdapter(command);
                da.Fill(ds,"Video_Games"); 
                dt = ds.Tables["Video_Games"];
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine(dr["Title"] + " " + dr["Release_Year"] + " " +
                    dr["Age_Rating"] + " " + dr["Genre"] + " " + dr["Developer"]);
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