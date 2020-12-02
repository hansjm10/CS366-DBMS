using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class StoredProcedure9
    {
        public void SP9(string connString)
        {
            MySqlConnection conn = new MySqlConnection(connString);
try
            {
                string queryString = "select g.Title, g.Release_Year, g.Age_Rating, g.Publisher, g.Developer" + "from Video_Games g" + 
                                    "where ((g.Release_Year >= rY1 and g.Release_Year < rY2) or (g.Developer = dev) or (g.Publisher = pub)) and g.Game_ID not in (select p.Game_ID from Prefers where " +
                                    "p.User_ID in (select u.User_ID from Users u where u.User_ID = userID));";
                
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                da.SelectCommand = new MySqlCommand("showPreferences", conn);
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