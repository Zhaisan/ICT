using System;
using System.Collections.Generic;
using System.Timers;

namespace Snake2
{
    class Game
    {
        Timer wormTimer = new Timer(100);
        //Timer wallTimer = new Timer(100);
        Timer gameTimer = new Timer(1000);

        public static int Width { get { return 29; } }
        public static int Height { get { return 29; } }

        Worm w = new Worm('@', ConsoleColor.Green);
        Food f = new Food('$', ConsoleColor.Yellow);
        Wall wall = new Wall('#', ConsoleColor.DarkYellow, @"Levels/Level1.txt");

        public bool IsRunning { get; set; }

        bool pause = false;

        public Game()
        {
            wormTimer.Elapsed += Move2;
            wormTimer.Start();
            gameTimer.Elapsed += GameTimer_Elapsed;
            gameTimer.Start();
            //wallTimer.Start();

            
            
            //wallTimer.Start();

            pause = false;
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

        private void GameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.Title = DateTime.Now.ToLongTimeString() + " Eatten food count: " + w.eattenCount;
        }

        bool CheckCollisionFoodWithWorm()
        {
            return w.body[0].X == f.body[0].X && w.body[0].Y == f.body[0].Y;
        }

        bool CheckCollisionWallWithWorm()
        {
            for (int i = 0; i < wall.body.Count; i++)
            {
                if ((wall.body[i].X == w.body[0].X && wall.body[i].Y == w.body[0].Y))
                {
                    return true;
                }
            }
            return false;

        }

        void Move2(object sender, ElapsedEventArgs e)
        {
            w.Move();
            if (CheckCollisionFoodWithWorm())
            {
                w.Increase(w.body[0]);
                f.Generate(w.body, wall.body);
            }
            if (w.body.Count == 3)
            {
                Console.Clear();
                NewLevel("Level2");
            }
            if (CheckCollisionWallWithWorm())
            {
                IsRunning = false;
                Console.Clear();
                Console.SetCursorPosition(10, 10);
                Console.ForegroundColor = ConsoleColor.Red; 
                Console.WriteLine("GAME OVER");
                wormTimer.Stop();
                pause = true;
            }
        }

        public void KeyPressed(ConsoleKeyInfo pressedKey)
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    w.ChangeDirection(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    w.ChangeDirection(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    w.ChangeDirection(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    w.ChangeDirection(1, 0);
                    break;
                case ConsoleKey.S:
                    w.Save("worm");
                    
                    break;
                case ConsoleKey.L:

                    wormTimer.Stop();
                    w.Clear();
                    f = new Food('$', ConsoleColor.Yellow);
                    wall = new Wall('#', ConsoleColor.DarkYellow, @"Levels/Level1.txt");
                    w = Worm.Load("worm");
                    
              
                    wormTimer.Start();
                    
                    break;
                case ConsoleKey.Escape:
                    IsRunning = false;
                    // wormTimer.Stop();
                    break;
                case ConsoleKey.Spacebar:
                    if (!pause)
                    {
                        wormTimer.Stop();
                        pause = true;
                    }
                    else
                    {
                        wormTimer.Start();
                        pause = false;
                    }
                    break;
            }

        }
    }
}
