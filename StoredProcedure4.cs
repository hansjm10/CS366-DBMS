using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class filterBySystem
    {
        public void filerSystem(string connString)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                string systemInput = "";
                Console.WriteLine("Enter system owned: ");
                systemInput = Console.ReadLine();
                
                MySqlDataAdapter da = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                MySqlParameter param;
                
                command.Connection = conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "filerSystem";
                
                param = new MySqlParameter("@systemOwned_p", systemInput);
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;
                command.Parameters.Add(param);

                da.Fill(ds,"VG_AgeandSystemFiltered");
                dt = ds.Tables["VG_AgeandSystemFiltered"];
                foreach (DataRow dr in dt.Rows)//Once we know the SP works, we can get rid of this loop.
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