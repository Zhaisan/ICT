using System;

namespace problemE
{
    class Program
    {
        static bool palindrome(string[] s)
        {
            for (int i = 0; i < s.Length / 2; i++)
            {
                if (s[i] != s[s.Length - i - 1])
                {
                    return false;
                }
            }
            return true;
        }
        static void Main(string[] args)
        {
            Console.ReadLine();
            string[] n = Console.ReadLine().Split();
            if (palindrome(n) == true)
            {
                Console.WriteLine("Palindrome");
            }
            else
            {
                Console.WriteLine("Not palindrome");
            }
        }
    }
}
