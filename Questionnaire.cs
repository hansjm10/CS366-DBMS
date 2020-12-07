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
        public void questionnaire(string connString)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                MySqlDataAdapter daSystems = new MySqlDataAdapter();
                MySqlDataAdapter daGenres = new MySqlDataAdapter();
                MySqlDataAdapter daDevs = new MySqlDataAdapter();
                MySqlCommand cmd = new MySqlCommand();
                DataSet dsSystems = new DataSet();
                DataSet dsGenres = new DataSet();
                DataSet dsDevs = new DataSet();
                DataTable dt = new DataTable();

                filterBySystem sp4 = new filterBySystem();
                filterByAnswers sp5 = new filterByAnswers();

                Console.WriteLine("Which system would you like to look at games for?");
                string systemInput = "";
                string correctInput = "------------------------------------------------------";
                bool inputisValid = false;
                while (inputisValid == false){
                    Console.WriteLine("Enter system (for a list of systems, enter 'I'): ");
                    systemInput = Console.ReadLine();
                    daSystems.SelectCommand = new MySqlCommand("select * from Systems", conn);
                    daSystems.Fill(dsSystems,"Systems");
                    dt = dsSystems.Tables["Systems"];
                    foreach(DataRow dr in dt.Rows){
                        if(Convert.ToString(dr["System_Name"]) == systemInput){
                            correctInput = systemInput;
                            break;
                        }
                    }
                    if (systemInput == correctInput){
                        sp4.filerSystem(connString, systemInput);
                        inputisValid = true;
                    }
                    else if (systemInput == "I"){
                        Console.WriteLine("\nList of Systems:");
                        foreach (DataRow dr in dt.Rows)
                        {
                            Console.WriteLine(dr["System_Name"]);
                        }
                        Console.WriteLine("");
                    }
                    else{
                        Console.WriteLine("Input is invalid. Try again.");
                        continue;
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
                        continue;
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
                        daGenres.SelectCommand = new MySqlCommand("select distinct Genre from Video_Games", conn);
                        daGenres.Fill(dsGenres,"Video_Games");
                        dt = dsGenres.Tables["Video_Games"];
                        Console.WriteLine("\nList of Genres:");
                        foreach (DataRow dr in dt.Rows)
                        {
                            Console.WriteLine(dr["Genre"]);
                        }
                        Console.WriteLine("");
                    }
                    else{
                        Console.WriteLine("Input is invalid. Try again.");
                        continue;
                    }
                }

                Console.WriteLine("Who is your favorite developer?");
                string devInput = "";
                inputisValid = false;
                while (inputisValid == false){
                    Console.WriteLine("Enter developer (for a list of developers, enter 'I'): ");
                    genreInput = Console.ReadLine();
                    if (devInput == "Valid"){//Will need an efficient way to check if input is valid.
                        sp5.filterAnswers(connString, releaseYear1, releaseYear2, genreInput, devInput);
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
                        Console.WriteLine("Input is invalid. Try again.");
                        continue;
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
            conn.Close();
        }
    }
}