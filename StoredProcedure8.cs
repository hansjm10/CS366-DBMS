using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class StoredProcedure8
    {
        public void SP8(string connString)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                string queryString = "select s.System_Name, avg(r.Score) as avgsc, sum(r.Num_Reviews) as totrevs" + 
                                     "from (Made_For s inner join Metacritic_Reviews r on Game_ID) inner join Video_Games g on Game_ID" + 
                                     "where g.Title = title and g.Game_ID = r.Game_ID and g.Game_ID = s.Game_ID and g.Game_ID in (select p.Game_ID from Prefers where p.User_ID in (select u.User_ID from Users U where u.User_ID = userID))";
                
                string title = "";
                Console.WriteLine("Enter title: ");
                title = Console.ReadLine();

                MySqlDataAdapter da = new MySqlDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                da.SelectCommand = new MySqlCommand("set @title = " + title + ";");
                da.SelectCommand = new MySqlCommand("Call showInfo", conn);
                da.Fill(ds,"Video_Games"); //This will have to change.
                dt = ds.Tables["Video_Games"];
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine(dr["System_Name"] + " " + dr["Avg_Score"] + " " + dr["Total_Reviews"]);
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