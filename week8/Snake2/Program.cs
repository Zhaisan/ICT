using System;
using System.Collections.Generic;
namespace Snake2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.SetCursorPosition(10, 10);
            Console.WriteLine("WELCOME TO THE SNAKE GAME");
            Console.SetCursorPosition(10, 12);
            Console.WriteLine("PRESS 'G' TO PLAY");
            string key = Console.ReadLine();
            if(key == "G")
            {
                Console.Clear();
                Game game = new Game();
                while (game.IsRunning)
                {
                    game.KeyPressed(Console.ReadKey());
                }
            }
            

            
        }
    }
}
