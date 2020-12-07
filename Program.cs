using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            String netID = "hansjm10";
            String password = "jh4570";
            String server = "washington.uww.edu";
            String database = "cs366-2207_hansjm10";
            string connString = "Server="+server + ";" + "Database=" + database + ";" + "User ID=" + netID + ";" + "Password=" + password;

            MySqlConnection con = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            Console.WriteLine("Connecting...");
            con.Open();

            //Declares class variables for classes that access the Questionnaire, PrefsList, and SPs.
            Questionnaire q = new Questionnaire();
            PreferencesList p = new PreferencesList();
            getUserCredentials sp1 = new getUserCredentials();
            filterTandM sp2 = new filterTandM();
            filterM sp3 = new filterM();
            finalGamesOutput sp6 = new finalGamesOutput();
            
            //User Registration and Login System
            Console.WriteLine("Type '1' to register as new user, press '2' to log in.");
            string loginSelect = Console.ReadLine();
            string userName = "", userID = "", inputAge = ""; 
            int userAge = 0;
            bool loggedIn = false;
            Tuple<string, string, string> userCreds;
            while(loggedIn == false){
                if (loginSelect == "1"){ //New user registration.
                    Console.WriteLine("Please enter your username, password, and age.");
                    Console.WriteLine("Enter username: ");
                    userName = Console.ReadLine();
                    Console.WriteLine("Enter password: ");
                    userID = Console.ReadLine();
                    Console.WriteLine("Enter age: ");
                    inputAge = Console.ReadLine();
                    Int32.TryParse(inputAge, out userAge);
                    cmd.CommandText = "INSERT INTO Users(User_ID,User_Name,Age) VALUES(?User_ID,?User_Name,?Age)";
                    cmd.Parameters.Add("?User_ID", MySqlDbType.VarChar).Value = userID;
                    cmd.Parameters.Add("?User_Name", MySqlDbType.VarChar).Value = userName;
                    cmd.Parameters.Add("?Age", MySqlDbType.Int32).Value = userAge;
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Welcome, " + userName + "!");
                    loggedIn = true;
                }
                else if (loginSelect == "2"){ //Login for existing users.
                    bool loginCorrect = false;
                    Console.WriteLine("Please enter your username and password.");
                    Console.WriteLine("Enter username: ");
                    userName = Console.ReadLine();
                    Console.WriteLine("Enter password: ");
                    userID = Console.ReadLine();
                    while (loginCorrect == false){
                        userCreds = sp1.getUserCred(connString, userID);
                        if (userID == userCreds.Item1 && userName == userCreds.Item2){
                            Console.WriteLine("Welcome back, " + userName + "!");
                            userID = userCreds.Item1;
                            userAge = Convert.ToInt32(userCreds.Item3);
                            loginCorrect = true;
                            loggedIn = true; 
                        }
                        else{
                            Console.WriteLine("Login failed. Try again.");
                            Console.WriteLine("Enter username: ");
                            userName = Console.ReadLine();
                            Console.WriteLine("Enter password: ");
                            userID = Console.ReadLine();
                            continue;
                        }
                    }
                }
                else {
                    Console.WriteLine("Invalid input, try again.");
                    Console.WriteLine("Type '1' to register as new user, press '2' to log in.");
                    loginSelect = Console.ReadLine();
                }
            }
        
            //Filter by age
            if(userAge < 13){
                Console.WriteLine("Strict parental controls enabled. Only games rated E and E10+ will be reccomended to you.");
                sp2.filterAge13(connString);
            }
            else if(userAge >= 13 && userAge < 17){
                Console.WriteLine("Moderate parental controls enabled. No M-rated games will be reccomended to you.");
                sp3.filterAge17(connString);
            }
            
            //Questionnaire
            Console.WriteLine("");
            Console.WriteLine("Now it's time for the questionnaire!");
            q.questionnaire(connString);
            
            //Final Output Screen
            DataTable dt = new DataTable(); 
            sp6.finalOutput(connString);
            string newInput = "";
            string saveID = "";
            bool isDone = false;
            while(isDone == false){
                Console.WriteLine("\nTo save a game to your Preferences List, enter 'S'");
                Console.WriteLine("To go to your Preferences List, enter 'P'");
                Console.WriteLine("To redo the questionnaire, enter 'Q'");
                Console.WriteLine("To log out, enter L");
                newInput = Console.ReadLine();
                if(newInput == "S"){
                    Console.WriteLine("Enter the ID of the game you want to save: ");
                    saveID = Console.ReadLine();
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT INTO Prefers(Game_ID,User_ID) VALUES(?Game_ID,?User_ID)";
                    cmd.Parameters.Add("?Game_ID", MySqlDbType.VarChar).Value = saveID;
                    cmd.Parameters.Add("?User_ID", MySqlDbType.VarChar).Value = userID;
                }
                else if(newInput == "P"){ //Go to preferences list screen.
                    isDone = p.prefsList(connString, userID);
                }
                else if(newInput == "Q"){ //Redo questionnaire
                    q.questionnaire(connString);
                    sp6.finalOutput(connString);
                }
                else if(newInput == "L"){ //Logout
                    isDone = true;
                }
                else{
                    Console.WriteLine("Input is invalid. Try again.");
                }
            }
            
            Console.WriteLine("Bye! See you again soon!");
            cmd.Connection = con;
            cmd.CommandText = "drop table if exists VG_AllFiltered";
            cmd.ExecuteNonQuery();
            con.Close();
            Console.Read();
        }
    }
}
