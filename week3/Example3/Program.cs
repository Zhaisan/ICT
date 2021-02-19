using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Example3
{
    public class WeatherForecast
    {
        public DateTimeOffset Date { get; set; }
        public int TemperatureCelsuis { get; set; }

        public string Summary { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            F2();
        }

        private static void F1()
        {
            WeatherForecast weatherForecast = new WeatherForecast { Date = DateTime.Now, Summary = "Some Summary", TemperatureCelsuis = 32 };
            string jsonString = JsonSerializer.Serialize(weatherForecast);
            Console.WriteLine(jsonString);
        }

        private static void F2()
        {
            WeatherForecast weatherForecast = new WeatherForecast { Date = DateTime.Now, Summary = "Some Summary", TemperatureCelsuis = 32 };
            string jsonString = JsonSerializer.Serialize(weatherForecast);
            File.WriteAllText("weatherForecast.json", jsonString);
         //    Creates a new file, writes the specified string to the file using the specified
        //     encoding, and then closes the file. If the target file already exists, it is
        //     overwritten.

        }
    }
}
