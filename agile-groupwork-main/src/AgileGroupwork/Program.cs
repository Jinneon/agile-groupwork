using System;
using System.IO;
using System.Data.SQLite;

namespace Groupwork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // At the moment, destroy the database and begin anew
            if (File.Exists("MyDatabase.sqlite"))
            {
                File.Delete("MyDatabase.sqlite");
            }
            SQLiteConnection.CreateFile("MyDatabase.sqlite");
            Console.WriteLine("Created a database!");

            // Open a database connection
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            // Create a table
            string sql = "CREATE TABLE Highscores (name TEXT, score INTEGER)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            Console.WriteLine("How many players?");
            int input = Convert.ToInt32(Console.ReadLine());
            int count = 0;
            while (true)
            {
                if (count < input)
                {
                    Console.WriteLine("Player name?");
                    string personName = Console.ReadLine();

                    sql = "INSERT INTO Highscores (name, score) " +
              "VALUES (@someValue, @someOtherValue);";
                    int defaultScore = 0;

                    using (var cmd = new SQLiteCommand(sql, m_dbConnection))
                    {
                        cmd.Parameters.AddWithValue("@someValue", personName);
                        //Default value for score is zero;
                        cmd.Parameters.AddWithValue("@someOtherValue", defaultScore);
                        cmd.ExecuteNonQuery();
                    }
                    count++;

                }
                else
                {
                    break;
                }

            }
            //Console.WriteLine("Add score for a person: Write 1"); LNL: Didn't dare to delete
            Console.WriteLine("Give person's score: Write 1"); //LNL: Not sure this is what I was expected to do
            Console.WriteLine("Show all scores: Write 2");
            Console.WriteLine("Show a persons score: Write 3");
            //  Console.WriteLine("Extras options: Write ex");
            Console.WriteLine("Add score for a person: Write quit");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 2)
            {
                Console.WriteLine("");
                sql = "SELECT * FROM Highscores ORDER BY score DESC";
                command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
                }

            // }
            // else if (choice == 1) //LNL: same here - didn't dare to delete
            // {
                //Add feature add score for a person
            }

            // Insert dummy data
            /*  sql = "INSERT INTO Highscores (name, score) VALUES ('Me', 9001)";
              command = new SQLiteCommand(sql, m_dbConnection);
              command.ExecuteNonQuery();
              sql = "INSERT INTO Highscores (name, score) VALUES ('Myself', 6000)";
              command = new SQLiteCommand(sql, m_dbConnection);
              command.ExecuteNonQuery();
              sql = "INSERT INTO Highscores (name, score) VALUES ('And I', 9001)";
              command = new SQLiteCommand(sql, m_dbConnection);
              command.ExecuteNonQuery();*/
            m_dbConnection.Close();
        }


    }
}
