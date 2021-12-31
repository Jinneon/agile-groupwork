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

            Console.WriteLine("How many persons?");
            int input = Convert.ToInt32(Console.ReadLine());
            int count = 0;
            while (true)
            {
                if (count < input)
                {
                    Console.WriteLine("Person name?");
                    string personName = Console.ReadLine();

                    sql = "INSERT INTO Highscores (name, score) " +
              "VALUES (@someValue, @someOtherValue);";
                    Console.WriteLine(" Score for a person?");
                    int score = Convert.ToInt32(Console.ReadLine());

                    using (var cmd = new SQLiteCommand(sql, m_dbConnection))
                    {
                        cmd.Parameters.AddWithValue("@someValue", personName);
                        //Default value for score is zero;
                        cmd.Parameters.AddWithValue("@someOtherValue", score);
                        cmd.ExecuteNonQuery();
                    }
                    count++;

                }
                else
                {
                    break;
                }

            }
            bool loop = false;
            while (loop == false)
            {

                Console.WriteLine("");
                Console.WriteLine("Add more persons: Write 1");
                Console.WriteLine("Show all scores: Write 2");
                Console.WriteLine("Show score for person: Write 3");
                //  Console.WriteLine("Extras options: Write ex");
                Console.WriteLine("Quit program: Write 4");
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 1)
                {
                    Console.WriteLine("How many persons?");
                    int input1 = Convert.ToInt32(Console.ReadLine());
                    int count1 = 0;
                    while (true)
                    {
                        if (count1 < input1)
                        {
                            Console.WriteLine("Person name?");
                            string personName = Console.ReadLine();

                            sql = "INSERT INTO Highscores (name, score) " +
                      "VALUES (@someValue, @someOtherValue);";
                            Console.WriteLine(" Score for a person?");
                            int score = Convert.ToInt32(Console.ReadLine());

                            using (var cmd = new SQLiteCommand(sql, m_dbConnection))
                            {
                                cmd.Parameters.AddWithValue("@someValue", personName);
                                //Default value for score is zero;
                                cmd.Parameters.AddWithValue("@someOtherValue", score);
                                cmd.ExecuteNonQuery();
                            }
                            count1++;
                        }
                        else
                        {
                            count1 = 0;
                            break;
                        }
                    }
                }
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

                }
                else if (choice == 3)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Choose a person");
                    string person = Console.ReadLine();
                    Console.WriteLine("");

                    sql = "SELECT COUNT(*) FROM Highscores WHERE (name = @user)";
                    SQLiteCommand check_User_Name = new SQLiteCommand(sql, m_dbConnection);
                    check_User_Name.Parameters.AddWithValue("@user", person);
                    int UserExist = (int)(long)check_User_Name.ExecuteScalar();

                    if (UserExist > 0)
                    {
                        sql = "SELECT name, score FROM Highscores WHERE name = '" + person + "'";
                        command = new SQLiteCommand(sql, m_dbConnection);
                        SQLiteDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Person doesn't exist");
                    }
                }
                else if (choice == 4)
                {
                    //Add code for quitting program
                    Console.WriteLine("");
                    Console.WriteLine("Bye!");
                    loop = true;
                    m_dbConnection.Close();
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
            }


        }
    }
}
