using System;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace DeserializeBasic
{
    public class WeatherForecast
    {
        public DateTimeOffset Date { get; set; }
        public int TemperatureCelsius { get; set; }
        public string GivenCode { get; set; }
        public string? Coordinates { get; set; }
        public string? CoordinatesField;
        public string[]? SummaryWords { get; set; }
    }

        public class Program
    {
        public static void Main()
        {
            

            string fileName = "cfg.json";
            string jsonString = File.ReadAllText(fileName);
            WeatherForecast weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(jsonString)!;

            Console.WriteLine($"Date: {weatherForecast.Date}");
            Console.WriteLine($"TemperatureCelsius: {weatherForecast.TemperatureCelsius}");
            Console.WriteLing($"Coordinates: {weatherForecast.Coordinates}");
            Console.WriteLine($"Summary: {weatherForecast.Summary}");
        }
    }
}