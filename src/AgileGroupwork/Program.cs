﻿using System;
using System.IO;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Groupwork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // SaveableDictionary dictionary = new SaveableDictionary("database.txt");
            //Open database connection
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            // Create a table
            string sql = "CREATE TABLE Highscores (name TEXT, hobby TEXT ,score INTEGER,uname TEXT, pw TEXT )";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            //            command.ExecuteNonQuery();


            Console.WriteLine("Welcome to team Perfect's program, how would you like to proceed?");
            Console.WriteLine(" ");
            Console.WriteLine("Login: Write *");
            Console.WriteLine("Create a new user account: Write +");
            Console.WriteLine("Exit program: Write any other key");

            List<Helper> usernameList = new List<Helper>();
            string sql2 = "SELECT uname FROM Highscores ORDER BY uname DESC";
            command = new SQLiteCommand(sql2, m_dbConnection);
            SQLiteDataReader readerList = command.ExecuteReader();
            List<string> realList = new List<string>();

            while (readerList.Read())
            {
                Helper country = new Helper();

                country.Username = readerList.GetValue(0).ToString();
                usernameList.Add(country);
            }
            foreach (Helper i in usernameList)
            {
                //  Console.WriteLine(i);
                string b = i.ToString();
                realList.Add(b);
                //   Console.WriteLine(b + "b");
            }

            //  readerList.Close();
            // command.Connection.Close();

            string username = "";
            string password = "";
            string pattern = @"^\w+$";
            string proceed = Console.ReadLine();
            ///Logic for login code start
            if (proceed != "*" && proceed != "+")
            {
                Console.WriteLine("");
                Console.WriteLine("Bye!");
                m_dbConnection.Close();
                Environment.Exit(0);
            }
            bool program = false;


            if (proceed == "*" || proceed == "+")
            {
                program = true;
            }
            if (realList.Count == 0)
            {
                Console.WriteLine("No users yet create account");
                proceed = "+";
            }




            //Login end
            if (program == true)
            {
                if (proceed == "+")
                {

                    Console.WriteLine("Creating new account: \n---------------------\nEnter username:");
                    //string pattern = @"^\w+$";

                    Regex regex = new Regex(pattern);

                    username = Console.ReadLine();
                    while (realList.Contains(username))
                    {
                        Console.WriteLine("Username is not available");
                        username = Console.ReadLine();
                        if (!realList.Contains(username))
                        {
                            Console.WriteLine("Your username is " + username);
                            break;

                        }
                    }
                    if (!Regex.Match(username, pattern).Success)
                    {
                        Console.WriteLine("Only numbers, letters or _ permitted!\nEnter username:");
                        username = Console.ReadLine();
                    }

                    Console.WriteLine("Enter password:");

                    password = Console.ReadLine();
                    if (!Regex.Match(password, pattern).Success)
                    {
                        Console.WriteLine("Only numbers, letters or _ permitted!\nEnter password:");
                        password = Console.ReadLine();
                    }

                }





            }
            if (proceed == "*")
            {
                Console.WriteLine("Login:");
                Console.WriteLine("--------");
                Console.WriteLine("Enter username:");
                //string pattern = @"^\w+$";

                Regex regex = new Regex(pattern);

                username = Console.ReadLine();
                if (!Regex.Match(username, pattern).Success)
                {
                    Console.WriteLine("Only numbers, letters or _ permitted!\nEnter username:");
                    username = Console.ReadLine();
                }
                Console.WriteLine("Enter password:");

                password = Console.ReadLine();
                if (!Regex.Match(password, pattern).Success)
                {
                    Console.WriteLine("Only numbers, letters or _ permitted!\nEnter password:");
                    password = Console.ReadLine();
                }


                Console.WriteLine("User " + username + " has logged in");
            }
            int count = 0;
            Console.WriteLine("How many persons to add?");

            var input = Console.ReadLine();
            int sco2;

            while (!int.TryParse(input, out sco2))
            {
                Console.WriteLine("This is not a number!\nHow many persons to add?");
                input = Console.ReadLine();
            }

            while (true)
            {
                if (count < sco2)
                {
                    Console.WriteLine("Name for a person?");
                    string personName = Console.ReadLine();
                    while (string.IsNullOrEmpty(personName))
                    {
                        Console.WriteLine("Name can't be empty!\nInput persons name");
                        personName = Console.ReadLine();
                    }
                    Console.WriteLine("Hobby?");
                    string inputHobby = Console.ReadLine();
                    while (string.IsNullOrEmpty(inputHobby))
                    {
                        Console.WriteLine("Hobby can't be empty!\nHobby?");
                        inputHobby = Console.ReadLine();
                    }



                    sql = "INSERT INTO Highscores (name, hobby ,score, uname, pw) " +
              "VALUES (@someValue, @hobby, @someOtherValue, @username, @password);";

                    Console.WriteLine("Score for a person?");
                    var score = Console.ReadLine();
                    int sco;

                    while (!int.TryParse(score, out sco))
                    {
                        try
                        {
                            Console.WriteLine("This is not a number!");
                            score = Console.ReadLine();
                        }
                        catch
                        {

                        }

                    }


                    if (sco < 0)
                    {
                        Console.WriteLine("Can't add negative scores!\nScore for a person?");
                        score = Console.ReadLine();

                    }


                    using (var cmd = new SQLiteCommand(sql, m_dbConnection))
                    {
                        cmd.Parameters.AddWithValue("@someValue", personName);
                        //Default value for score is zero;
                        cmd.Parameters.AddWithValue("@hobby", inputHobby);
                        cmd.Parameters.AddWithValue("@someOtherValue", score);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.ExecuteNonQuery();
                    }



                    string s = Convert.ToString(score);
                    //  dictionary.Load();
                    //   dictionary.Add(personName, inputHobby, s, username);
                    count++;
                    //  dictionary.Save();

                }
                else
                {
                    break;
                }

            }
            bool loop = false;
            while (loop == false)
            {
                string currentUser = username;
                Console.WriteLine("Logged in user is " + username);
                Console.WriteLine("");
                Console.WriteLine("Add more persons: Write 1");
                Console.WriteLine("Show all scores: Write 2");
                Console.WriteLine("Show score for person: Write 3");
                //  Console.WriteLine("Extras options: Write ex");
                Console.WriteLine("Quit program: Write 4");
                Console.WriteLine("Reset scores for your usernames persons: Write 5");
                Console.WriteLine("Logoff/Change user: write 6");

                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 1)
                {
                    Console.WriteLine("How many persons?");
                    int count1 = 0;
                    var input1 = Console.ReadLine();
                    //int count1 = 0;
                    int sco3;

                    while (!int.TryParse(input1, out sco3))
                    {
                        Console.WriteLine("This is not a number!");
                        input1 = Console.ReadLine();
                    }
                    while (true)
                    {
                        if (count1 < Convert.ToInt32(input1))
                        {
                            Console.WriteLine("Name for a person?");
                            string personName = Console.ReadLine();
                            while (string.IsNullOrEmpty(personName))
                            {
                                Console.WriteLine("Name can't be empty! Input persons name");
                                personName = Console.ReadLine();
                            }

                            Console.WriteLine("Hobby?");
                            string inputHobby = Console.ReadLine();
                            while (string.IsNullOrEmpty(inputHobby))
                            {
                                Console.WriteLine("Hobby can't be empty! Input hobby");
                                inputHobby = Console.ReadLine();
                            }

                            sql = "INSERT INTO Highscores (name, hobby, score, uname) " +
                      "VALUES (@someValue, @hobby, @someOtherValue, @uname);";
                            Console.WriteLine("Score for a person?");

                            var score = Console.ReadLine();
                            int sco;

                            while (!int.TryParse(score, out sco))
                            {
                                Console.WriteLine("This is not a number!\nScore for a person?");
                                score = Console.ReadLine();
                            }

                            if (sco < 0)
                            {
                                Console.WriteLine("Can't add negative scores!");
                                score = Console.ReadLine();

                            }
                            using (var cmd = new SQLiteCommand(sql, m_dbConnection))
                            {
                                cmd.Parameters.AddWithValue("@someValue", personName);
                                //Default value for score is zero;
                                cmd.Parameters.AddWithValue("@hobby", inputHobby);

                                cmd.Parameters.AddWithValue("@someOtherValue", score);
                                cmd.Parameters.AddWithValue("@uname", username);
                                cmd.ExecuteNonQuery();
                            }
                            string s = Convert.ToString(score);

                            //   dictionary.Add(personName, inputHobby, s, username);

                            // dictionary.Save();
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
                        Console.WriteLine("Name: " + reader["name"] + "\tHobby: " + reader["hobby"] + "\tScore: " + reader["score"]
                       + "\tUsername: " + reader["uname"]);
                    }

                }
                else if (choice == 3)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Choose a person");
                    string person = Console.ReadLine();
                    if (string.IsNullOrEmpty(person))
                    {
                        Console.WriteLine("Person can't be empty!\nInput persons name");
                        person = Console.ReadLine();
                    }
                    Console.WriteLine("");

                    sql = "SELECT COUNT(*) FROM Highscores WHERE (name = @user)";
                    SQLiteCommand check_User_Name = new SQLiteCommand(sql, m_dbConnection);
                    check_User_Name.Parameters.AddWithValue("@user", person);
                    int UserExist = (int)(long)check_User_Name.ExecuteScalar();

                    if (UserExist > 0)
                    {
                        sql = "SELECT name,hobby ,score FROM Highscores WHERE name = '" + person + "'";
                        command = new SQLiteCommand(sql, m_dbConnection);
                        SQLiteDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            //  Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
                            Console.WriteLine("Name: " + reader["name"] + "\tHobby: " + reader["hobby"] + "\tScore: " + reader["score"]);
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
                else if (choice == 5)
                {
                    // ask for confirmation by the user
                    Console.WriteLine("Do you want to reset all scores for your username? Input y for yes, n for no");
                    //Testing
                    sql = "SELECT COUNT(*) FROM Highscores WHERE (uname = @username)";
                    SQLiteCommand check_User_Name = new SQLiteCommand(sql, m_dbConnection);
                    check_User_Name.Parameters.AddWithValue("@username", username);
                    int UserExist = (int)(long)check_User_Name.ExecuteScalar();
                    if (currentUser != username)
                    {
                        Console.WriteLine("Cannot delete other users data");
                        continue;
                    }

                    if (UserExist > 0)
                    {
                        sql = "SELECT name,hobby ,score, uname FROM Highscores WHERE uname = '" + username + "'";
                        command = new SQLiteCommand(sql, m_dbConnection);
                        SQLiteDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            //  Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
                            Console.WriteLine("Name: " + reader["name"] + "\tHobby: " + reader["hobby"] + "\tScore: " + reader["score"]
                            + "\tUsername: " + reader["uname"]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Person doesn't exist");
                    }
                    string resetConfirmation = Convert.ToString(Console.ReadLine());
                    if (resetConfirmation == "y")
                    {
                        // Resets the scores for every user
                        sql = "UPDATE  Highscores SET score = '0' WHERE score  > 0 AND uname = '" + username + "'";

                        command = new SQLiteCommand(sql, m_dbConnection);
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        continue;
                    }
                }
                #region Dont delete



                /*     else if (choice == 6)
                     {
                         //reset the score of a specific user
                         Console.WriteLine("Enter the name of the user you want to reset:");
                         string user2 = Convert.ToString(Console.ReadLine());
                         if (string.IsNullOrEmpty(user2))
                         {
                             Console.WriteLine("Name can't be empty! Input persons name");
                             user2 = Console.ReadLine();
                         }
                         sql = "SELECT COUNT(*) FROM Highscores WHERE (name = @user)";
                         SQLiteCommand check_User_Name1 = new SQLiteCommand(sql, m_dbConnection);
                         check_User_Name1.Parameters.AddWithValue("@user", user2);
                         int UserExist = (int)(long)check_User_Name1.ExecuteScalar();

                         if (UserExist > 0)
                         {
                             sql = "UPDATE  Highscores SET score = '0' WHERE score  > 0 and name  ='" + user2 + "';";

                             command = new SQLiteCommand(sql, m_dbConnection);
                             command.ExecuteNonQuery();
                         }
                         else
                         {
                             Console.WriteLine("Person doesn't exist");
                             continue;
                         }

                     }
                     else if (choice == 7)
                     {
                         Console.WriteLine("Are you sure you want to delete everything? Input y for yes, n for no");
                         string resetConfirmation = Convert.ToString(Console.ReadLine());
                         if (resetConfirmation == "y")
                         {
                             // Deletes all inputs
                             sql = "DELETE FROM Highscores;";
                             command = new SQLiteCommand(sql, m_dbConnection);
                             command.ExecuteNonQuery();
                         }
                         else
                         {
                             continue;
                         }
                     }
                     //for testing only
                     else if (choice == 8)
                     {
                         Console.WriteLine("");
                         sql = "SELECT * FROM LOGIN ORDER BY uname DESC";
                         command = new SQLiteCommand(sql, m_dbConnection);
                         SQLiteDataReader reader = command.ExecuteReader();
                         while (reader.Read())
                         {
                             Console.WriteLine("Username: " + reader["uname"] + "\tPassword: " + reader["pw"]);
                         }

                     }*/
                #endregion

                else if (choice == 6)
                {
                    Console.WriteLine(currentUser + " logged off");
                    Console.WriteLine("Enter username:");
                    pattern = @"^\w+$";

                    Regex regex2 = new Regex(pattern);

                    username = Console.ReadLine();
                    if (!Regex.Match(username, pattern).Success)
                    {
                        Console.WriteLine("Only numbers, letters or _ permitted!\nEnter username:");
                        username = Console.ReadLine();
                    }
                    Console.WriteLine("Enter password:");

                    password = Console.ReadLine();
                    if (!Regex.Match(password, pattern).Success)
                    {
                        Console.WriteLine("Only numbers, letters or _ permitted!\nEnter password:");
                        password = Console.ReadLine();
                    }

                    Console.WriteLine("How many persons?");

                    var input2 = Console.ReadLine();
                    int sco3;

                    while (!int.TryParse(input2, out sco3))
                    {
                        Console.WriteLine("This is not a number!\nHow many persons?");
                        input2 = Console.ReadLine();
                    }

                    int count2 = 0;
                    while (true)
                    {
                        if (count2 < sco3)
                        {
                            Console.WriteLine("Name for a person?");
                            string personName = Console.ReadLine();
                            if (string.IsNullOrEmpty(personName))
                            {
                                Console.WriteLine("Name can't be empty!\nInput persons name:");
                                personName = Console.ReadLine();
                            }
                            Console.WriteLine("Hobby?");
                            string inputHobby = Console.ReadLine();
                            if (string.IsNullOrEmpty(inputHobby))
                            {
                                Console.WriteLine("Hobby can't be empty!\nHobby?");
                                inputHobby = Console.ReadLine();
                            }



                            sql = "INSERT INTO Highscores (name, hobby ,score, uname, pw) " +
                      "VALUES (@someValue, @hobby, @someOtherValue, @username, @password);";

                            Console.WriteLine("Score for a person?");
                            var score = Console.ReadLine();
                            int sco;

                            while (!int.TryParse(score, out sco))
                            {
                                Console.WriteLine("This is not a number!\nScore for a person?");
                                score = Console.ReadLine();
                            }

                            if (sco < 0)
                            {
                                Console.WriteLine("Can't add negative scores!\nScore for a person?");
                                score = Console.ReadLine();

                            }


                            using (var cmd = new SQLiteCommand(sql, m_dbConnection))
                            {
                                cmd.Parameters.AddWithValue("@someValue", personName);
                                //Default value for score is zero;
                                cmd.Parameters.AddWithValue("@hobby", inputHobby);
                                cmd.Parameters.AddWithValue("@someOtherValue", score);
                                cmd.Parameters.AddWithValue("@username", username);
                                cmd.Parameters.AddWithValue("@password", password);
                                cmd.ExecuteNonQuery();
                            }



                            string s = Convert.ToString(score);
                            //  dictionary.Load();
                            // dictionary.Add(personName, inputHobby, s, username);
                            count2++;
                            //  dictionary.Save();

                        }
                        else
                        {
                            break;
                        }

                    }
                }

            }
        }

    }

}