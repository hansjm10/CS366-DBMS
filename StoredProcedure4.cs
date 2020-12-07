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
        public void filerSystem(string connString, string systemInput)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                MySqlParameter param;
                
                command.Connection = conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "filterSystem";
                
                param = new MySqlParameter("@systemOwned_p", systemInput);
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;
                command.Parameters.Add(param);
                da = new MySqlDataAdapter(command);

                command.CommandText = "queryFilterSystem";
                da = new MySqlDataAdapter(command);
                da.Fill(ds,"VG_AgeandSystemFiltered");
                dt = ds.Tables["VG_AgeandSystemFiltered"];
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
        }
    }
}