using System;
using System.Collections.Generic;
using System.Text;


namespace Snake2
{
    class Food: GameObject
    {
        Random rnd = new Random();
        public Food(char sign, ConsoleColor color) : base(sign, color)
        {
            Point location = new Point { X = rnd.Next(1, Game.Width), Y = rnd.Next(1, Game.Height) };
            body.Add(location);
            Draw();
        }

        bool TestAppearingFood(Point p, List<Point> points)
        {
            foreach (Point i in points)
            {
                if (p.X == i.X || p.Y == i.Y)
                {
                    return false;
                    break;
                }
            }
            return true;
        }
        public void Generate(List<Point> wormTest, List<Point> wallTest)
        {


            body.Clear();
            Random random = new Random();

            Point p = new Point { X = random.Next(0, 15), Y = random.Next(0, 15) };

            while (!TestAppearingFood(p, wormTest) || !TestAppearingFood(p, wallTest))
            {
                p = new Point { X = random.Next(0, 15), Y = random.Next(0, 15) };
            }
            body.Add(p);
            Draw();



        }
    }
}
