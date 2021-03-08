using System;
using System.Collections.Generic;
using System.Text;

namespace Example1
{
    class Game
    {
        public static int Width { get { return 29; } }
        public static int Height { get { return 29; } }

        Worm w = new Worm('@', ConsoleColor.Green);
        Food f = new Food('$', ConsoleColor.Yellow);
        Wall wall = new Wall('#', ConsoleColor.DarkYellow, @"Levels/Level1.txt");



        public bool IsRunning { get; set; }
        public Game()
        {
            IsRunning = true;
            Console.CursorVisible = false;
            Console.SetWindowSize(Width, Width);
            Console.SetBufferSize(Width, Width);
        }

        public void NewLevel(String level)
        {
            this.wall = new Wall('#', ConsoleColor.DarkYellow, @$"Levels/{level}.txt");
            this.w = new Worm('@', ConsoleColor.Green);
            this.f = new Food('$', ConsoleColor.Yellow);
            
        }

        bool CheckCollisionFoodWithWorm()
        {
            return w.body[0].X == f.body[0].X && w.body[0].Y == f.body[0].Y;
        }

        bool CheckCollisionWallWithWorm()
        {
            for (int i = 0; i < wall.body.Count; i++)
            {
                if((wall.body[i].X == w.body[0].X && wall.body[i].Y == w.body[0].Y))
                {
                    return true;
                }
            }
            return false;
            
        }
      
        public void KeyPressed(ConsoleKeyInfo pressedKey)
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    w.Move(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    w.Move(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    w.Move(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    w.Move(1, 0);
                    break;
                case ConsoleKey.Escape:
                    IsRunning = false;
                    break;
            }

            
            if (CheckCollisionFoodWithWorm())
            {
                w.Increase(w.body[0]);
                f.Generate(w.body, wall.body);
               
                
            }
            if(w.body.Count == 5)
            {
                Console.Clear();
                NewLevel("Level2");
            }
            if (CheckCollisionWallWithWorm())
            {
                IsRunning = false;
            }
        }
    }
}
