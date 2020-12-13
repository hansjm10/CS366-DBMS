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
            try{
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
                Console.WriteLine("\n***************************************");
                Console.WriteLine("       VIDEO GAME RECOMMENDER");
                Console.WriteLine("****************************************");
                Console.WriteLine("Enter '1' to register as new user; enter '2' to log in.");
                string loginSelect = Console.ReadLine();
                string userName = "", userID = "", inputAge = ""; 
                int userAge = 0;
                bool loggedIn = false;
                Tuple<string, string, string> userCreds;
                while(loggedIn == false){
                    if (loginSelect == "1"){ //New user registration.
                        Console.WriteLine("\nPlease enter your username, password, and age.");
                        Console.WriteLine("Enter username: ");
                        userName = Console.ReadLine();
                        Console.WriteLine("Enter password: ");
                        userID = Console.ReadLine();
                        Console.WriteLine("Enter age: ");
                        inputAge = Console.ReadLine();
                        Int32.TryParse(inputAge, out userAge);
                        cmd.Parameters.Clear();
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
                        Console.WriteLine("\nPlease enter your username and password.");
                        Console.WriteLine("Enter username: ");
                        userName = Console.ReadLine();
                        Console.WriteLine("Enter password: ");
                        userID = Console.ReadLine();
                        while (loginCorrect == false){
                            userCreds = sp1.getUserCred(connString, userID);
                            if (userID == userCreds.Item1 && userName == userCreds.Item2){
                                Console.WriteLine("\nWelcome back, " + userName + "!");
                                userID = userCreds.Item1;
                                userAge = Convert.ToInt32(userCreds.Item3);
                                loginCorrect = true;
                                loggedIn = true; 
                            }
                            else{
                                Console.WriteLine("\nLogin failed. Try again.");
                                Console.WriteLine("Enter username: ");
                                userName = Console.ReadLine();
                                Console.WriteLine("Enter password: ");
                                userID = Console.ReadLine();
                                continue;
                            }
                        }
                    }
                    else {
                        Console.WriteLine("\nInvalid input, try again.");
                        Console.WriteLine("\nType '1' to register as new user, press '2' to log in.");
                        loginSelect = Console.ReadLine();
                    }
                }
        
                //Filter by age
                if(userAge < 13){
                    Console.WriteLine("\nStrict parental controls enabled. Only games rated E and E10+ will be reccomended to you.");
                    sp2.filterAge13(connString);
                }
                else if(userAge >= 13 && userAge < 17){
                    Console.WriteLine("\nModerate parental controls enabled. No M-rated games will be reccomended to you.");
                    sp3.filterAge17(connString);
                }
            
                //Questionnaire
                string qChoice = "";
                bool qDone = false;
                Console.WriteLine("\nWould you like to do the questionnaire (enter 'Y' for yes or 'N' for no)?");
                qChoice = Console.ReadLine();
                while (qDone == false){
                    if (qChoice == "Y"){
                        Console.WriteLine("\n***************************************");
                        Console.WriteLine("           QUESTIONNAIRE");
                        Console.WriteLine("****************************************");
                        q.questionnaire(connString, userID);
                        sp6.finalOutput(connString);
                        qDone = true;
                    }
                    else if(qChoice == "N"){
                        Console.WriteLine("\nOkay!  Straight to the options screen it is!");
                        qDone = true;
                    }
                    else{
                        Console.WriteLine("\nInvalid input, try again.");
                        Console.WriteLine("\nWould you like to do the questionnaire (enter 'Y' for yes or 'N' for no)?");
                        qChoice = Console.ReadLine();
                    }
                }
                
                //Options Screen
                string newInput = "", saveID = "", closeChoice = "";
                bool isDone = false;
                while(isDone == false){
                    Console.WriteLine("\n***************************************");
                    Console.WriteLine("             OPTIONS");
                    Console.WriteLine("****************************************");
                    Console.WriteLine("\nTo save a game to your Preferences List, enter 'S'.");
                    Console.WriteLine("To go to your Preferences List, enter 'P'.");
                    Console.WriteLine("To redo the questionnaire, enter 'Q'.");
                    Console.WriteLine("To log out, enter 'L'.");
                    Console.WriteLine("To close your account, enter 'C'.");
                    newInput = Console.ReadLine();
                    if(newInput == "S"){
                        Console.WriteLine("\nEnter the ID of the game you want to save: ");
                        saveID = Console.ReadLine();
                        var watch = new System.Diagnostics.Stopwatch();
                        watch.Start();
                        cmd.Parameters.Clear();
                        cmd.CommandText = "INSERT IGNORE INTO Prefers(User_ID,Game_ID) VALUES(?User_ID,?Game_ID)";
                        cmd.Parameters.Add("?User_ID", MySqlDbType.VarChar).Value = userID;
                        cmd.Parameters.Add("?Game_ID", MySqlDbType.VarChar).Value = saveID;
                        cmd.ExecuteNonQuery();
                        watch.Stop();
                        Console.WriteLine("\nGame saved! In: " + "{watch.ElapsedMilliseconds} ms");

                    }
                    else if(newInput == "P"){ //Go to preferences list screen.
                        isDone = p.prefsList(connString, userID);
                    }
                    else if(newInput == "Q"){ //Redo questionnaire
                        Console.WriteLine("\n***************************************");
                        Console.WriteLine("           QUESTIONNAIRE");
                        Console.WriteLine("****************************************");
                        q.questionnaire(connString, userID);
                        sp6.finalOutput(connString);
                    }
                    else if(newInput == "L"){ //Logout
                        Console.WriteLine("\nBye! See you again soon!");
                        isDone = true;
                    }
                    else if(newInput == "C"){ //Close account 
                        Console.WriteLine("\nAre you sure you want to de-register (enter 'Y' for yes or 'N' for no).");
                        closeChoice = Console.ReadLine();
                        if (closeChoice == "Y"){
                            Console.WriteLine("\nWhatever you say...");
                            string delUser = "", delPrefs = "", delOwns = "";
                            delUser = "DELETE FROM Users WHERE User_ID = '" + userID + "'";
                            delPrefs = "DELETE FROM Prefers WHERE User_ID = '" + userID + "'";
                            delOwns = "DELETE FROM Owns WHERE User_ID = '" + userID + "'";
                            cmd.CommandText = delUser;  
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = delPrefs;  
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = delOwns;  
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("\nWe're sad to see you go...");
                            isDone = true;
                        }
                        else if (closeChoice == "N"){
                            Console.WriteLine("\nWhew! That was a close one!");
                        }
                        else{
                            Console.WriteLine("\nInput is invalid, but we'll take it as a 'No'");
                        }
                    }
                    else{
                        Console.WriteLine("Input is invalid. Try again.");
                    }
                }
                con.Close();
            }
            catch(MySql.Data.MySqlClient.MySqlException ex){
                Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
        }
    }
}
