using System;

namespace problemC
{
    class Program
    {
        static void Main(string[] args)

        {
            int n = int.Parse(Console.ReadLine());
            string[] s = Console.ReadLine().Split();
            int cnt = 1;
            for (int i = 0; i < s.Length; i++)
            {
                Console.Write(int.Parse(s[i]) + cnt + " ");
                cnt++;
            }
        }
    }
}
