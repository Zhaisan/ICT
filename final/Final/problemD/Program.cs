using System;
using System.Text.RegularExpressions;

namespace problemD
{
    class Program
    {
        public static bool friday(string s)
        {
            return Regex.IsMatch(s, @"^[a-z]+[@][a-z]+[.][a-z]+$");
        }

        public static void Main(string[] args)
        {
            String s = Console.ReadLine();

            if (friday(s) == false)
            {
                Console.WriteLine("No");
            }
            else
            {
                Console.WriteLine("Yes");
            }
        }
    }
}
