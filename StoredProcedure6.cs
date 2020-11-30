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
        public void SP6(MySqlConnection conn)
        {
            
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "delimiter $$ drop procedure if exists finalOutput;";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "create procedure showPreferences(OUT title varchar(255)," +
                                  "INOUT system varchar(255), OUT releaseYear varchar(255)," +
                                    "INOUT ageRating varchar(255), INOUT pub varchar(255)," +
                                    "INOUT dev varchar(255))" +
                                    "begin" + 
                                        "select g.Title, s.System_Name, g.Release_Year, g.Age_Rating," +
                                            "g.Publisher, g.Developer, avg(r.Score) as avgScore," + 
                                    "sum(r.Num_Reviews) as totalReviews into title, system," + 
                                        "releaseYear, ageRating, pub, dev, avgScore, numReviews" +
                                    "from (Made_For s inner join Metacritic_Reviews r on Game_ID)" + 
                                    "inner join Video_Games g on Game_ID" +
                                    "where g.Game_ID = r.Game_ID and g.Game_ID = s.Game_ID" +
                                    "order by averageScore desc limit 10;" +
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