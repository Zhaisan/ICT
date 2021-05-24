using System;

namespace problemA
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] a = Console.ReadLine().Split();
            String[] u = Console.ReadLine().Split();

            for (int i = a.Length - 1; i >= 0; i--)
            {
                if(int.Parse(u[i]) > int.Parse(a[i]))
                {
                    Console.WriteLine("Yes");
                    return;
                }
                else if (int.Parse(u[i]) < int.Parse(a[i]))
                {
                    Console.WriteLine("No");
                    return;
                }

            }
            Console.WriteLine("No");
        }
    }
}
