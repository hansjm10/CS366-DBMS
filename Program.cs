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
            
            Console.WriteLine("Connecting...");
            
            //Login System
            Console.WriteLine("Type '1' to register as new user, press '2' to log in.");
            string loginSelect = Console.ReadLine();
            string userName, userID, inputAge; 
            int userAge;
            if (loginSelect == "1"){
                Console.WriteLine("Please enter your username, password, and age.");
                userName = Console.ReadLine();
                userID = Console.ReadLine();
                inputAge = Console.ReadLine();
                Int32.TryParse(inputAge, out userAge);
            }
            else if (loginSelect == "2"){
                
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
