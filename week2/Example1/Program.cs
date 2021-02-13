using System;
using System.IO;
//6.02.2021

namespace Example1
{
    class Program
    {
        static void Main(string[] args)
        {
            F4();
        }
        static void PrintFolderInfo(string path, string prefix)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            try
            {
                foreach (FileSystemInfo fi in dir.GetFileSystemInfos())
                {
                    Console.WriteLine(prefix + " " + fi.Name);
                    if (fi.GetType() == typeof(DirectoryInfo))
                    {
                        PrintFolderInfo(fi.FullName, prefix + "+");
                    }
                }
            }catch(Exception)
            {

            }
            //getting all files from ... with отступ 
        }
        private static void F4()
        {
            PrintFolderInfo(@"C:\Games\FlatOut2\Savegame","");
           
           

        }
        private static void F3()
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\Games\FlatOut2\Savegame");
            foreach (FileSystemInfo fi in dir.GetFileSystemInfos())
            {
                Console.WriteLine(fi.Name);
            }
            //Returns an array of strongly typed FileSystemInfo entries representing all the files and subdirectories in a directory.


        } 
        private static void F2()
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\Games\FlatOut2\Savegame");
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                Console.WriteLine(d.Name);
            }
            //Returns the subdirectories of the current directory.
            foreach (FileInfo f in dir.GetFiles())
            {
                Console.WriteLine(f.Name);
            }
            //Returns a file list from the current directory matching the specified search pattern and enumeration options.
        }

        private static void F1()
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\Games\FlatOut2\Savegame");
            //DirectoryInfo dir = new DirectoryInfo(@"C:\");

            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                Console.WriteLine(d.Name);
            }
        }
        private static void F0()
        {
            foreach (string l in Environment.GetLogicalDrives())
            {
                Console.WriteLine(l);
            }
        }
    }
}
