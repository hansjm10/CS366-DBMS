using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class getUserCredentials
    {
        public Tuple<string, string, string> getUserCred(string connString, string userID)
        {
            string uID_return = "", userName_return = "", userAge = "";
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
                command.CommandText = "getUserCred";
                
                param = new MySqlParameter("@userID_p", userID);
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;
                command.Parameters.Add(param);
                
                da = new MySqlDataAdapter(command);
                da.Fill(ds,"Users");
                dt = ds.Tables["Users"];
                foreach (DataRow dr in dt.Rows)
                {
                    uID_return = Convert.ToString(dr["User_ID"]);
                    userName_return = Convert.ToString(dr["User_Name"]);
                    userAge = Convert.ToString(dr["Age"]);
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
            return new Tuple<string, string, string> (uID_return, userName_return, userAge);
        }
    }
}