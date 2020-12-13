using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class PreferencesList
    {
        public bool prefsList(string connString, string userID)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Prefers WHERE Game_ID = @gameID_p AND User_ID = @userID_p", conn);
                showPrefsList sp7 = new showPrefsList();
                showMoreInfo sp8 = new showMoreInfo();
                finalOutputFromPreferences sp9 = new finalOutputFromPreferences();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                cmd.Connection = conn;
                string input = "", title = "", delID = "", moreID = "";
                bool isDone = false;
                
                while(isDone == false){
                    Console.WriteLine("\n***************************************");
                    Console.WriteLine("             PREFERENCES");
                    Console.WriteLine("****************************************");
                    sp7.showPreferences(connString, userID);
                    Console.WriteLine("\nTo get more info about a title, enter 'I'.");
                    Console.WriteLine("To delete a title from your Preferences List, enter 'D'.");
                    Console.WriteLine("To get more reccommendations based on your Preferences List, enter 'R'.");
                    Console.WriteLine("To go back to the options screen, enter 'O'.");
                    Console.WriteLine("To log out, enter 'L'.");
                    input = Console.ReadLine();
                    if(input == "I"){
                        Console.WriteLine("Enter the title of the game you want more info on: ");
                        title = Console.ReadLine();
                        
                        sp8.showInfo(connString, userID, title);
                    }
                    else if(input == "D"){
                        Console.WriteLine("Enter the ID of the game you want to delete: ");
                        delID = Console.ReadLine();
                        string deletePrefers = "DELETE FROM Prefers WHERE User_ID = '" + userID + "' AND Game_ID = '" + delID + "'";
                        cmd.CommandText = deletePrefers;
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Game successfully deleted.");
                    }
                    else if(input == "R"){
                        string system = "", rY = "", ageRating = "", gen = "", dev = "";
                        Console.WriteLine("Enter the ID of the game you want to base more input on: ");
                        moreID = Console.ReadLine();
                        da.SelectCommand = new MySqlCommand(("select* from Video_Games g inner join Made_For s on s.Game_ID = g.Game_ID "   
                                                            + "where g.Game_ID = '" + moreID + "'"), conn);
                        da.Fill(ds,"Video_Games");
                        dt = ds.Tables["Video_Games"];
                        foreach(DataRow dr in dt.Rows){
                            system = Convert.ToString(dr["System_Name"]);
                            rY = Convert.ToString(dr["Release_Year"]);
                            ageRating = Convert.ToString(dr["Age_Rating"]);
                            gen = Convert.ToString(dr["Genre"]);
                            dev = Convert.ToString(dr["Developer"]);
                        }
                        sp9.filterPreferences(connString, userID, system, rY, ageRating, gen, dev);
                        isDone = true;
                    }
                    else if(input == "O"){
                        isDone = true;
                    }
                    else if(input == "L"){
                        Console.WriteLine("\nBye! See you again soon!");
                        return true; 
                    }
                    else{
                        Console.WriteLine("\nInput is invalid. Try again.");
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
            return false;
        }
    }
}