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
        public void filterAnswers(string connString, int rY1, int rY2, string gen, string dev)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                MySqlDataAdapter da;
                MySqlCommand command = new MySqlCommand();
                MySqlCommand cmd2 = new MySqlCommand();
                MySqlParameter param;
                MySqlParameter param2;
                MySqlParameter param3;
                MySqlParameter param4;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                command.Connection = conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "filterAnswers";

                param = new MySqlParameter("@rY1_p", rY1);
                param2 = new MySqlParameter("@rY2_p", rY2);
                param3 = new MySqlParameter("@gen_p", gen);
                param4 = new MySqlParameter("@dev_p", dev);

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

                da = new MySqlDataAdapter(command);
                da.Fill(ds,"VG_AllFiltered"); 
                dt = ds.Tables["VG_AllFiltered"];
                foreach (DataRow dr in dt.Rows)//Once we know this works, we can get rid of this loop.
                {
                    Console.WriteLine(dr["Game_ID"] + " " + dr["Title"] + " " + dr["Release_Year"] + " " 
                                    + " " + dr["Genre"] + " " + dr["Developer"]);
                }
                cmd2.Connection = conn;
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "drop table if exists VG_AgeandSystemFiltered";
                cmd2.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
        }
    }
}