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
                MySqlCommand cmd = new MySqlCommand();
                filterBySystem sp4 = new filterBySystem();
                filterByAnswers sp5 = new filterByAnswers();
                
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
                        sp5.filterAnswers(connString, releaseYear1, releaseYear2, genreInput, devInput);
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
            }
            catch (MySql.Data.MySqlClient.MySqlException ex){
                Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
        }
    }
}