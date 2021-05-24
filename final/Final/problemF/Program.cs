using System;

namespace problemF
{
    class Program
    {
        public static void apartment()
        {
            string[] s = Console.ReadLine().Split();
            int a = int.Parse(s[0].ToString());
            int b = int.Parse(s[1].ToString());
            int x = int.Parse(s[2].ToString());
            int y = int.Parse(s[3].ToString());
            if(Math.Min(a, b) > Math.Min(x, y))
            {
                Console.WriteLine("Impossible");
                
            }
            else if (Math.Max(a, b) > Math.Max(x, y))
            {
                Console.WriteLine("Impossible");
            }
            else 
            {
                Console.WriteLine("Thanks, Nurbek");
            }
            

        }
        static void Main(string[] args)
        {
            apartment();
        }
    }
}

