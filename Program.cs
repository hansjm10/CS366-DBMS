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

            //Declares class variables for classes that access the stored procedures.
            getUserCredentials sp1 = new getUserCredentials();
            filterTandM sp2 = new filterTandM();
            filterM sp3 = new filterM();
            filterBySystem sp4 = new filterBySystem();
            filterByAnswers sp5 = new filterByAnswers();
            finalGamesOutput sp6 = new finalGamesOutput();
            showPrefsList sp7 = new showPrefsList();
            showMoreInfo sp8 = new showMoreInfo();
            finalOutputFromPreferences sp9 = new finalOutputFromPreferences();
            
            //Login System
            Console.WriteLine("Type '1' to register as new user, press '2' to log in.");
            string loginSelect = Console.ReadLine();
            string userName, userID, inputAge; 
            int userAge = 0;
            bool loggedIn = false;
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
                    Console.WriteLine("Please enter your username and password.");
                    Console.WriteLine("Enter username: ");
                    userName = Console.ReadLine();
                    Console.WriteLine("Enter password: ");
                    userID = Console.ReadLine();
                    Tuple<string, string, string> userCreds = sp1.getUserCred(connString, userID);
                    bool loginCorrect = false;
                    while (loginCorrect == false){
                        if (userID == userCreds.Item1 && userName == userCreds.Item2){
                            Console.WriteLine("Welcome back, " + userName + "!");
                            loginCorrect = true;
                        }
                        else{
                            Console.WriteLine("Login failed. Try again.");
                        }
                    }
                    userAge = Convert.ToInt32(userCreds.Item3);
                    loggedIn = true; 
                }
                else {
                    Console.WriteLine("Invalid input, try again.");
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
            Console.WriteLine("Which system would you like to look at games for?");
            string systemInput = "";
            bool inputisValid = false;
            while (inputisValid == false){
                Console.WriteLine("Enter system (for a list of systems, enter 'I'): ");
                systemInput = Console.ReadLine();
                if (systemInput == "Valid"){//Will need an efficient way to check if input is valid.
                    sp4.filerSystem(connString, systemInput);
                    inputisValid = true;
                }
                else if (systemInput == "I"){
                    cmd.CommandText = "select * from Systems";
                    cmd.ExecuteNonQuery();
                }
                else{
                    Console.WriteLine("Input is invalid. Try again.");
                }
            }

            Console.WriteLine("Which range of release years do you want your games from?");
            int releaseYear1 = 0, releaseYear2 = 0;
            string inputRY1 = "", inputRY2 = "";
            inputisValid = false;
            while(inputisValid == false){
                Console.WriteLine("Enter earliest release year (earliest possible is 1980): ");
                inputRY1 = Console.ReadLine();
                Int32.TryParse(inputRY1, out releaseYear1);
                Console.WriteLine("");
                Console.WriteLine("Enter latest release year (latest possible is 2020): ");
                inputRY2 = Console.ReadLine();
                Int32.TryParse(inputRY2, out releaseYear2);
                if (releaseYear1 >= 1980 && releaseYear2 <= 2020){
                    inputisValid = true;
                }
                else {
                    Console.WriteLine("Input is invalid. Try again.");
                }
            }

            Console.WriteLine("What is your favorite genre?");
            string genreInput = "";
            inputisValid = false;
            while (inputisValid == false){
                Console.WriteLine("Enter genre (for a list of genres, enter 'I'): ");
                genreInput = Console.ReadLine();
                if (genreInput == "Valid"){//Will need an efficient way to check if input is valid.
                    inputisValid = true;
                }
                else if (genreInput == "I"){
                    cmd.CommandText = "select distinct Genre from Video_Games";
                    cmd.ExecuteNonQuery();
                }
                else{
                    Console.WriteLine("Input is invalid. Try again.");
                }
            }

            Console.WriteLine("Who is your favorite developer?");
            string devInput = "";
            inputisValid = false;
            while (inputisValid == false){
                Console.WriteLine("Enter developer (for a list of developers, enter 'I'): ");
                genreInput = Console.ReadLine();
                if (devInput == "Valid"){//Will need an efficient way to check if input is valid.
                    inputisValid = true;
                }
                else if (devInput == "I"){
                    cmd.CommandText = "select distinct Developer from Video_Games";
                    cmd.ExecuteNonQuery();
                }
                else{
                    Console.WriteLine("Input is invalid. Try again.");
                }
            }
            
            //Final Output Screen
            DataTable dt = new DataTable(); 
            dt = sp6.finalOutput(connString);

            //Preferences List

            con.Close();
            Console.Read();
        }
    }
}
