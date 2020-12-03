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
                Console.WriteLine("Enter system name: ");
                string systemName = Console.ReadLine();
                Console.WriteLine("Enter release year 1: ");
                string rY1 = Console.ReadLine();
                Console.WriteLine("Enter release year 2: ");
                string rY2 = Console.ReadLine();
                Console.WriteLine("Enter age rating: ");
                string ageRating = Console.ReadLine();
                Console.WriteLine("Enter genre: ");
                string gen = Console.ReadLine();
                Console.WriteLine("Enter developer: ");
                string dev = Console.ReadLine();

                MySqlDataAdapter da;
                MySqlCommand command = new MySqlCommand();
                MySqlParameter param;
                MySqlParameter param2;
                MySqlParameter param3;
                MySqlParameter param4;
                MySqlParameter param5;
                MySqlParameter param6;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();


                command.Connection = conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "finalOutput";

                param = new MySqlParameter("@systemName_p", systemName);
                param2 = new MySqlParameter("@rY1_p", rY1);
                param3 = new MySqlParameter("@rY2_p", rY2);
                param4 = new MySqlParameter("@ageRating_p", ageRating);
                param5 = new MySqlParameter("@gen_p", gen);
                param6 = new MySqlParameter("@dev_p", dev);

                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;
                command.Parameters.Add(param);

                param2.Direction = ParameterDirection.Input;
                param2.DbType = DbType.String;
                command.Parameters.Add(param2);
                
                param3.Direction = ParameterDirection.Input;
                param3.DbType = DbType.String;
                command.Parameters.Add(param3);
                
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