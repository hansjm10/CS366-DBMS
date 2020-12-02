using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class StoredProcedure4
    {
        public void SP4(string connString)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {   
                conn.Open();     
                Console.WriteLine("Enter system owned: ");
                string systemInput = Console.ReadLine();
                
                MySqlDataAdapter da;
                MySqlCommand command = new MySqlCommand();
                MySqlParameter param;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                //I don't believe SelectCommand works like this, I believe it is just one query that needs to be
                //Sent through. It is only for initiating stored procedures.
                //We could try setting Parameters.AddWithValue(@SystemOwned,systemInput) and then ExecutingNonQuery() like how the original example did it.

                command.Connection = conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "filterSystem";

                param = new MySqlParameter("@systemOwned_p", systemInput);

                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;
                command.Parameters.Add(param);

                da = new MySqlDataAdapter(command);

                da.Fill(ds,"Video_Games");
                dt = ds.Tables["Video_Games"];
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine(dr["Title"] + " " + dr["Release_Year"] + " " + dr["Age_Rating"] + " " + dr["Genre"] + " " + dr["Developer"]);
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