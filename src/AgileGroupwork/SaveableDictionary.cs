using System;

using System.IO;

using System.Collections.Generic;



namespace Groupwork

{

    public class SaveableDictionary

    {


        List<Data> list = new List<Data>();

        private string file;

        public SaveableDictionary()

        {

            // this.dictionary = new List<Tuple<string, string, int>>();
            this.list = new List<Data>();



        }



        public SaveableDictionary(string file) : this()

        {

            this.file = file;

        }



        public void Add(string name, string hobby, string score)
        {
            string s = Convert.ToString(score);
            this.list.Add(new Data { Name = name, Hobby = hobby, Score = s });


        }



        public bool Load()
        {

            try

            {

                string[] lines = File.ReadAllLines(this.file);

                foreach (string line in lines)
                {

                    string[] parts = line.Split(":");

                    Add(parts[0], parts[1], parts[2]);

                }

                return true;

            }

            catch (Exception e)

            {
                //Don´t mind this (error);
                // Console.WriteLine(e.Message);

                return false;

            }

        }


        public bool Save()

        {

            try

            {

                StreamWriter writer = new StreamWriter(this.file);





                foreach (Data s in list)

                {

                    // If a word or its translation is not in the list, add to list and file



                    writer.WriteLine($"{s}");



                }

                writer.Close();

                return true;

            }

            catch (Exception)

            {

                return false;

            }

        }





    }



}

