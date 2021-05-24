using System;

namespace problemJ
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] a = Console.ReadLine().Split();
            string[] b = Console.ReadLine().Split();
            int[] a1 = Array.ConvertAll(a, int.Parse);
            int[] b1 = Array.ConvertAll(b, int.Parse);

            int arsenal = a1[1] + b1[0];
            int barsa = a1[0] + b1[1];

            if (barsa < arsenal)
            {
                Console.WriteLine("Arselona");
            }
            else if (barsa > arsenal)
            {
                Console.WriteLine("Barsenal");
            }
            else
            {
                if (a1[1] > b1[1])
                {
                    Console.WriteLine("Arselona");
                }
                else if (b1[1] > a1[1])
                {
                    Console.WriteLine("Barsenal");
                }
                else
                {
                    Console.WriteLine("Friendship");
                }
            }

            Console.WriteLine (barsa + " " + arsenal);
        }
    }
}
