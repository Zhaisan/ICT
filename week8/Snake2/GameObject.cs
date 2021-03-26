using System;
using System.Collections.Generic;
using System.Text;

namespace Snake2
{
    public abstract class GameObject
    {
        public char sign;
        public ConsoleColor color;
        public List<Point> body;

        public GameObject()
        {

        }
        public GameObject(char sign, ConsoleColor color)
        {
            this.sign = sign;
            this.color = color;
            this.body = new List<Point>();
        }
        public void Draw()
        {
            Console.ForegroundColor = color;
            for (int i = 0; i < body.Count; ++i)
            {
                Console.SetCursorPosition(body[i].X, body[i].Y);
                Console.Write(sign);
            }
        }
        public void Clear()
        {
            for (int i = 0; i < body.Count; ++i)
            {
                Console.SetCursorPosition(body[i].X, body[i].Y);
                Console.Write(' ');
            }
        }
    }
}
