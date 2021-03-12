using System;

using System.Collections.Generic;
using System.IO;

namespace FileManager
{
    class Layer
    {
        public DirectoryInfo dir
        {
            get;
            set;
        }
        public int pos
        {
            get;
            set;
        }
        public List<FileSystemInfo> content
        {
            get;
            set;
        }
       
        public Layer(DirectoryInfo dir, int pos)
        {
            this.dir = dir;
            this.pos = pos;
            this.content = new List<FileSystemInfo>();

            content.AddRange(this.dir.GetDirectories()); 
            content.AddRange(this.dir.GetFiles());
        }
        public void PrintInfo()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;
            int cnt = 0;
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                if (cnt == pos)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                Console.WriteLine(d.Name);
                cnt++;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (FileInfo f in dir.GetFiles())
            {
                if (cnt == pos)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                Console.WriteLine(f.Name);
                cnt++;
            }

        }
        public void ReadFile()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            string read_file = File.ReadAllText(content[pos].FullName);
            Console.WriteLine(read_file);
            Console.ReadKey();

        }
        double size = 0.0d;
        public void GetSize(string path)
        {
            if(GetCurrentObject().GetType() == typeof(DirectoryInfo))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                foreach(FileSystemInfo fsi in di.GetFileSystemInfos())
                {
                    if(fsi.GetType() == typeof(DirectoryInfo))
                    {
                        GetSize(fsi.FullName);
                    }
                    else
                    {
                        FileInfo fi = fsi as FileInfo;
                        this.size += fi.Length;
                    }
                }
            }
            else
            {
                FileInfo FI = new FileInfo(content[pos].FullName);
                this.size += FI.Length;
            }
        }

        
        public void PrintSize()
        {
          
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Blue;
            this.size = 0;
            GetSize(GetCurrentObject().FullName);
            Console.WriteLine(this.size + " " + "Bytes");
        }

        public void Rename()
        {
            string parent = new DirectoryInfo(content[pos].FullName).Parent.FullName;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            
            string name = Console.ReadLine();
            if (content[pos] is DirectoryInfo)
            {
                Directory.Move(content[pos].FullName, parent + '/' + name);
            }
            else
            {
                File.Move(content[pos].FullName, parent + '/' + name);
            }


        }
        public FileSystemInfo GetCurrentObject()
        {
            return content[pos];
        }
        public void SetNewPosition(int d)
        {
            if (d > 0)
            {
                pos++;

            }
            else
            {
                pos--;
            }
            if (pos >= content.Count)
            {
                pos = 0;
            }
            else if (pos < 0)
            {
                pos = content.Count - 1;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            F3();
        }
        private static void PrintInfo(string path, int pos)
        {

        }
        private static void F3()
        {
            Stack<Layer> history = new Stack<Layer>();
            history.Push(new Layer(new DirectoryInfo(@"C:\Users\Жаркын\Saved Games\Desktop\C#"), 0));

            bool escape = false;
            bool temp = false;
           

            while (!escape)
            {
                Console.Clear();
                history.Peek().PrintInfo();
                if (temp) {
                    history.Peek().PrintSize();
                }
                
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                

                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        if (history.Peek().GetCurrentObject().GetType() == typeof(DirectoryInfo))
                        {
                            history.Push(new Layer(history.Peek().GetCurrentObject() as DirectoryInfo, 0));
                        }
                        else
                        {
                            history.Peek().ReadFile();
                        }
                        
                        break;
                    case ConsoleKey.Q:
                        temp = !temp;
                        break;
                    case ConsoleKey.R:
                        history.Peek().Rename();
                       
                        ConsoleKeyInfo consoleKey = Console.ReadKey();
                       
                        break;

                    case ConsoleKey.UpArrow:
                        history.Peek().SetNewPosition(-1);
                        temp = false;

                        break;
                    case ConsoleKey.DownArrow:
                        history.Peek().SetNewPosition(1);
                        temp = false;
                        break;
                    case ConsoleKey.Escape:
                        history.Pop();
                        break;

                }


            }
        }
       
    }
}
