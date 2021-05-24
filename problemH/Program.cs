using System;

namespace problemH
{
    class Program
    {
        public static void greatestMultiplier()
        {
            string[] s = Console.ReadLine().Split();
            int a = int.Parse(s[0].ToString());
            int b = int.Parse(s[1].ToString());
            int temp = 0;
            for (int i = 1; i <= b; i++)
            {
                if (i % a == 0)
                {
                    temp = i;
                }
            }
            Console.WriteLine(temp);
        }
        static void Main(string[] args)
        {
            greatestMultiplier();
        }
    }
}
