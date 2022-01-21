using System;
using System.IO;
using System.Collections.Generic;



namespace Groupwork
{
    public class Data
    {
        public string Name { get; set; }
        public string Hobby { get; set; }
        public string Score { get; set; }
        public string Username { get; set; }


        public override string ToString()
        {
            //  Console.WriteLine("works");
            return Name + " " + Hobby + " " + Score + " " + Username;
        }
    }
}