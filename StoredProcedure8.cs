using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class showMoreInfo
    {
        public void showInfo(string connString, string userID, string title)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {

                MySqlDataAdapter da;
                MySqlCommand command = new MySqlCommand();
                MySqlParameter param;
                MySqlParameter param2;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();


                command.Connection = conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "showInfo";

                param = new MySqlParameter("@userID_p", userID);
                param2 = new MySqlParameter("@title_p", title);
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;
                param2.Direction = ParameterDirection.Input;
                param2.DbType = DbType.String;
                command.Parameters.Add(param);
                command.Parameters.Add(param2);
                conn.Open();
                command.ExecuteNonQuery();
                da = new MySqlDataAdapter(command);
                da.Fill(ds,"Video_Games"); //This will have to change.
                dt = ds.Tables["Video_Games"];
                foreach (DataRow dr in dt.Rows)//Once we know this works, we'll probably want to convert this
                {                              //loop to a datatable/dataframe/matrix/whatever and return that.
                    Console.WriteLine(dr["System_Name"] + " " + dr["totrevs"] + " " + dr["avgrevs"]);
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