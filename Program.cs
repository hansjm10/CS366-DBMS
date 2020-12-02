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
            

            //Test adding a line.
            Console.WriteLine("Heyo!");
            Console.WriteLine("Connecting...");
            
            //StoredProcedure2 sp2 = new StoredProcedure2();
            //sp2.SP2(connString);
            // StoredProcedure3 sp3 = new StoredProcedure3();
            // sp3.SP3(connString);
            // StoredProcedure4 sp4 = new StoredProcedure4();
            // sp4.SP4(connString);
            // StoredProcedure7 sp7 = new StoredProcedure7();
            // sp7.SP7(connString);
               StoredProcedure8 sp8 = new StoredProcedure8();
               sp8.SP8(connString);

            // try
            // {
            //     con.Open();
            //     Console.WriteLine("Connection Succesful");
            //     var cmd = new MySqlCommand(stm, con);
            //     var version  = cmd.ExecuteScalar().ToString();
            //     Console.WriteLine(version);
            // }
            // catch (Exception e)
            // {
            //     Console.WriteLine("Error: " + e.Message);
            // }
            Console.Read();
        }
    }
}
