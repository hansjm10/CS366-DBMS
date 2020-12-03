using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class StoredProcedure1
    {
        public void SP1(string connString)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                
                
                conn.Open();
                Console.WriteLine("Enter user ID: ");
                string userID = Console.ReadLine();
                
                MySqlDataAdapter da = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                MySqlParameter param;

                command.Connection = conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getUserCred";
                
                param = new MySqlParameter("@userID_p", userID);
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;
                command.Parameters.Add(param);
                
                da.Fill(ds,"Users");
                dt = ds.Tables["Users"];
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine(dr["User_ID"] + " " + dr["User_Name"] + " " + dr["Age"] + " " + dr["Genre"] + " " + dr["Developer"]);
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