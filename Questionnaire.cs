using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnection
{
    class Questionnaire
    {
        public void questionnaire(string connString, string userID)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            var watch = new System.Diagnostics.Stopwatch();
            try
            {
                MySqlDataAdapter daSystems = new MySqlDataAdapter();
                MySqlDataAdapter daOwns = new MySqlDataAdapter();
                MySqlDataAdapter daGenres = new MySqlDataAdapter();
                MySqlDataAdapter daDevs = new MySqlDataAdapter();
                MySqlCommand cmd = new MySqlCommand();
                DataSet dsSystems = new DataSet();
                DataSet dsOwns = new DataSet();
                DataSet dsGenres = new DataSet();
                DataSet dsDevs = new DataSet();
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();

                filterBySystem sp4 = new filterBySystem();
                filterByAnswers sp5 = new filterByAnswers();

                Console.WriteLine("\nWhich system do you own?");
                string systemInput = "";
                string correctInputS = "------------------------------------------------------";
                bool inputisValid = false;
                while (inputisValid == false){
                    Console.WriteLine("Enter system (enter 'I' for a list of systems, or 'O' for a list of systems you own.): ");
                    systemInput = Console.ReadLine();
                    daSystems.SelectCommand = new MySqlCommand("select * from Systems", conn);
                    daSystems.Fill(dsSystems,"Systems");
                    dt = dsSystems.Tables["Systems"];
                    foreach(DataRow dr in dt.Rows){
                        if(Convert.ToString(dr["System_Name"]) == systemInput){
                            correctInputS = systemInput;
                            break;
                        }
                    daOwns.SelectCommand = new MySqlCommand(("select System_Name from Owns where User_ID = " + userID), conn);
                    daOwns.Fill(dsOwns,"Owns");
                    dt2 = dsOwns.Tables["Systems"];
                    }
                    if (systemInput == correctInputS){
                        cmd.Parameters.Clear();
                        cmd.CommandText = "INSERT INTO Owns(User_ID,System_Name) VALUES(?User_ID,?System_Name)";
                        cmd.Parameters.Add("?User_ID", MySqlDbType.VarChar).Value = userID;
                        cmd.Parameters.Add("?Game_ID", MySqlDbType.VarChar).Value = systemInput;
                        cmd.ExecuteNonQuery();
                        inputisValid = true;
                    }
                    else if (systemInput == "I"){
                        Console.WriteLine("\nList of Systems:");
                        foreach (DataRow dr in dt.Rows)
                        {
                            Console.WriteLine(dr["System_Name"]);
                        }
                    }
                    else if (systemInput == "O"){
                        foreach (DataRow dr in dt2.Rows){
                            Console.WriteLine(dr["System_Name"]);
                        }
                    }
                    else{
                        Console.WriteLine("\nInput is invalid. Try again.");
                        continue;
                    }
                }
                
                watch.Start();
                sp4.filterSystem(connString, systemInput);
                watch.Stop();
                Console.WriteLine("Executed query in: {watch.ElapsedMilliseconds}");
                Console.WriteLine("\nWhich range of release years do you want your games from?");
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
                        Console.WriteLine("\nInput is invalid. Try again.");
                        continue;
                    }
                }

                Console.WriteLine("\nWhat is your favorite genre?");
                string genreInput = "";
                inputisValid = false;
                while (inputisValid == false){
                    Console.WriteLine("Enter genre (for a list of genres, enter 'I'): ");
                    genreInput = Console.ReadLine();
                    if (genreInput == "Sports" || genreInput == "Platform" || genreInput == "Racing" || genreInput == "Role-Playing" 
                        || genreInput == "Puzzle" || genreInput == "Misc" || genreInput == "Shooter" || genreInput == "Simulation"
                        || genreInput == "Action" || genreInput == "Fighting" || genreInput == "Adventure" || genreInput == "Strategy"){
                        inputisValid = true;
                    }
                    else if (genreInput == "I"){
                        daGenres.SelectCommand = new MySqlCommand("select distinct Genre from Video_Games", conn);
                        daGenres.Fill(dsGenres,"Video_Games");
                        dt = dsGenres.Tables["Video_Games"];
                        Console.WriteLine("\nList of Genres:");
                        foreach (DataRow dr in dt.Rows)
                        {
                            Console.WriteLine(dr["Genre"]);
                        }
                    }
                    else{
                        Console.WriteLine("\nInput is invalid. Try again.");
                        continue;
                    }
                }

                Console.WriteLine("\nWho is your favorite developer?");
                string devInput = "";
                string correctInputD = "-------------------------------------";
                inputisValid = false;
                while (inputisValid == false){
                    Console.WriteLine("Enter developer (for a list of developers, enter 'I'): ");
                    devInput = Console.ReadLine();
                    daDevs.SelectCommand = new MySqlCommand("select distinct Developer from Video_Games", conn);
                    daDevs.Fill(dsDevs,"Video_Games");
                    dt = dsDevs.Tables["Video_Games"];
                    foreach(DataRow dr in dt.Rows){
                        if(Convert.ToString(dr["Developer"]) == devInput){
                            correctInputD = devInput;
                            break;
                        }
                    }
                    if (devInput == correctInputD){
                        watch.Restart();
                        sp5.filterAnswers(connString, inputRY1, inputRY2, genreInput, devInput);
                        watch.Stop();
                        Console.WriteLine("Executed Query in: {watch.ElapsedMilliseconds}");
                        inputisValid = true;
                    }
                    else if (devInput == "I"){
                        daDevs.SelectCommand = new MySqlCommand("select distinct Developer from Video_Games", conn);
                        daDevs.Fill(dsDevs,"Video_Games");
                        dt = dsDevs.Tables["Video_Games"];
                        Console.WriteLine("\nList of Developers:");
                        foreach (DataRow dr in dt.Rows)
                        {
                            Console.WriteLine(dr["Developer"]);
                        }
                        Console.WriteLine("");
                    }
                    else{
                        Console.WriteLine("\nInput is invalid. Try again.");
                        continue;
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
        }
    }
}