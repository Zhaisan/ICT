using System;

namespace problemG
{
    class Program
    {
        public static int greatestMultiplier(int a, int b)
        {
            int temp = 0;
            for (int i = 1; i <= b; i++)
            {
                if (i % a == 0)
                {
                    temp = i;
                }
            }
            return temp;
        }

        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split();
            int a = int.Parse(s[0].ToString());
            int b = int.Parse(s[1].ToString());
            Console.WriteLine(greatestMultiplier(a, b));
        }
    }
}
