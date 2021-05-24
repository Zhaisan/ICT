using System;

namespace problemI
{
    class Program
    {
        public static void smallDivisor()
        {
            int x = Convert.ToInt32(Console.ReadLine());
            for (int i = 2; i <= x; i++)
            {
                if(x % i == 0)
                {
                    Console.WriteLine(i);
                    break;
                }
            }
        }
        static void Main(string[] args)
        {
            smallDivisor();
        }
    }
}
