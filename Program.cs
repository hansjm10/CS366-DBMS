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
            
            //Login System
            Console.WriteLine("Type '1' to register as new user, press '2' to log in.");
            string loginSelect = Console.ReadLine();
            string userName, userID, inputAge; 
            int userAge;
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

            }
            else{
                Console.WriteLine("Invalid input, try again.");
                loginSelect = Console.ReadLine();
            }
        

            //Questionnaire

            //Final Output Screen

            //Preferences List


            Console.Read();
        }
    }
}
