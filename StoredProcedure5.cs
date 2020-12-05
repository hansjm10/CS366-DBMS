using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class filterByAnswers
    {
        public void filterAnswers(string connString)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                conn.Open();
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
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                command.Connection = conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "filterAnswers";

                param = new MySqlParameter("@rY1_p", rY1);
                param2 = new MySqlParameter("@rY2_p", rY2);
                param3 = new MySqlParameter("@ageRating_p", ageRating);
                param4 = new MySqlParameter("@gen_p", gen);
                param5 = new MySqlParameter("@dev_p", dev);

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

                da = new MySqlDataAdapter(command);
                da.Fill(ds,"Video_Games"); 
                dt = ds.Tables["Video_Games"];
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine(dr["Game_ID"] + " " + dr["Title"] + " " + dr["Release_Year"] + " " 
                                    + dr["Age_Rating"] + " " + dr["Genre"] + " " + dr["Developer"]);
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