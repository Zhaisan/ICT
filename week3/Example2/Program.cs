using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Example2
{
    public class University
    {
        public University()
        {
            Students = new List<Student>();
        }
        public List<Student> Students { get; set; }
    }
    public class Student
    {
        public Student()
        {

        }
        public Student(string name, double gpa)
        {
            Name = name;
            GPA = gpa;
        }
        //[XmlIgnore]
        public double GPA { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name + " " + GPA;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            F6();
        }
        static void F1()
        {
            FileStream fs = new FileStream("student.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite); //Create and read xml file
            StreamWriter sw = new StreamWriter(fs); //To have an access to change file from here
            sw.WriteLine("test"); //message to xml file
            sw.Close();
            fs.Close();
        }
        static void F0()
        {
            FileStream fs = new FileStream("student.xml", FileMode.Append, FileAccess.Write); //Just adding text by append
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("test2"); 
            sw.Close();
            fs.Close();
        }

        static void F2() //To read the new changes 
        {
            FileStream fs = new FileStream("student.xml", FileMode.Open, FileAccess.Read); 
            StreamReader sr = new StreamReader(fs);
            Console.WriteLine(sr.ReadToEnd());
            sr.Close();
            fs.Close();
        }

        static void F3() //More optimized form of prev function
        {
            using (FileStream fs = new FileStream("student.xml", FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    Console.WriteLine(sr.ReadToEnd());
                } //automatically calls close()
            }
            
            
        }
        static void F4()
        {
            Student s = new Student ("Bob", 3.4 );

            FileStream fs = new FileStream("student.xml", FileMode.OpenOrCreate, FileAccess.Write);


            XmlSerializer xs = new XmlSerializer(typeof(Student));

            xs.Serialize(fs, s); //Serialize kuda and chto
            
            fs.Close();
        }

        static void F5()
        {

            FileStream fs = new FileStream("student.xml", FileMode.Open, FileAccess.Read);


            XmlSerializer xs = new XmlSerializer(typeof(Student));

            Student s = xs.Deserialize(fs) as Student;

            fs.Close();

            Console.WriteLine(s);

            //The advantage of xml is that we can serialize our xml file on java and deserialize on c# and vise versa and so on.
        }

        static void F6()
        {
            Student s = new Student ("Bob1", 3.4);
            Student s2 = new Student ("Bob2", 3);

            University u = new University();
            u.Students.Add(s);
            u.Students.Add(s2);

            FileStream fs = new FileStream("student.xml", FileMode.OpenOrCreate, FileAccess.Write);


            XmlSerializer xs = new XmlSerializer(typeof(University));

            xs.Serialize(fs, u);

            fs.Close();

        
        }

    }
}
