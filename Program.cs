using System;
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

            getUserCredentials sp1 = new getUserCredentials();
            filterTandM sp2 = new filterTandM();
            filterM sp3 = new filterM();
            
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
            
            
            //Final Output Screen

            //Preferences List

            con.Close();
            Console.Read();
        }
    }
}
