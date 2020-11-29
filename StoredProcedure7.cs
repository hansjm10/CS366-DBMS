using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class StoredProcedure7
    {
        static void SP7(MySqlConnection conn)
        {
            
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "delimiter $$ drop procedure if exists showPreferences;";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "create procedure showPreferences(IN userID varchar(255), OUT title" +
                                        "varchar(255), OUT releaseYear varchar(255), OUT ageRating" +
                                        "varchar(255), OUT pub varchar(255), OUT dev varchar(255))" +
                                   "begin" +
                                    "select g.Title, g.Release_Year, g.Age_Rating, g.Publisher," + 
                                        "g.Developer into title, releaseYear, ageRating, pub, dev" +
                                    "from Video_Games g" +
                                    "where g.Game_ID in (select p.Game_ID from Prefers p where" +
                                        "p.User_ID in (select u.User_ID from Users u))" + 
                                    "end $$" + 
                                    "delimiter ;";
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine ("Error " + ex.Number + " has occurred: " + ex.Message);
            }
            conn.Close();
            Console.WriteLine("Connection closed.");
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = "add_emp";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@lname", "Jones");
                cmd.Parameters["@lname"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@fname", "Tom");
                cmd.Parameters["@fname"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@bday", "1940-06-07");
                cmd.Parameters["@bday"].Direction = ParameterDirection.Input;

                cmd.Parameters.Add("@empno", MySqlDbType.Int32);
                cmd.Parameters["@empno"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                Console.WriteLine("Employee number: "+cmd.Parameters["@empno"].Value);
                Console.WriteLine("Birthday: " + cmd.Parameters["@bday"].Value);
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