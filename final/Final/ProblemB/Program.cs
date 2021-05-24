using System;

namespace ProblemB
{
    class Program
    {
        static void Main(string[] args)
        {
            String s = Console.ReadLine();
            String temp = "";
            for (int i = 0; i < s.Length; i++)
            {
                bool check = false;
                for (int j = 0; j < temp.Length; j++)
                {
                    if (s[i] == temp[j])
                    {
                        check = true;
                        break;
                    }
                }
                if (check != true)
                {
                    temp += s[i];
                }
            }
            Console.WriteLine(temp);
        }
    }
}
