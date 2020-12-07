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
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                showPrefsList sp7 = new showPrefsList();
                showMoreInfo sp8 = new showMoreInfo();
                finalOutputFromPreferences sp9 = new finalOutputFromPreferences();
                string input = "";
                string title = "";
                string delID = "";
                bool isDone = false;
                
                sp7.showPreferences(connString, userID);
                while(isDone == false){
                    Console.WriteLine("To get more info about a title, enter 'I'.");
                    Console.WriteLine("To delete a title from your Preferences List, enter 'D'.");
                    Console.WriteLine("To get more reccommendations based on your Preferences List, enter 'R'");
                    Console.WriteLine("To go back to the options screen, enter 'O'.");
                    Console.WriteLine("To log out, enter 'L'.");
                    if(input == "I"){
                        Console.WriteLine("Enter the title of the game you want more info on: ");
                        title = Console.ReadLine();
                        sp8.showInfo(connString, userID, title);
                    }
                    else if(input == "D"){
                        Console.WriteLine("Enter the ID of the game you want to delete: ");
                        delID = Console.ReadLine();
                        cmd.CommandText = "delete from Prefers";
                    }
                    else if(input == "R"){
                        sp9.filterPreferences(connString);
                        isDone = true;
                    }
                    else if(input == "O"){
                        isDone = true;
                    }
                    else if(input == "L"){
                        return true; 
                    }
                    else{
                        Console.WriteLine("Input is invalid. Try again.");
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