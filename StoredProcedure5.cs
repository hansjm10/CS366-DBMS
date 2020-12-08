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
        public void filterAnswers(string connString, string rY1, string rY2, string gen, string dev)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                MySqlParameter param;
                MySqlParameter param2;
                MySqlParameter param3;
                MySqlParameter param4;

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
                conn.Open();
                command.ExecuteNonQuery();
                da = new MySqlDataAdapter(command);

                command.CommandText = "queryAllFiltered";
                da = new MySqlDataAdapter(command);
                da.Fill(ds,"VG_AllFiltered");
                dt = ds.Tables["VG_AllFiltered"];
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
        }
    }
}