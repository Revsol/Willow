using System;

namespace Client
{
    using System.IO;

    using Microsoft.Extensions.Configuration;

    using RestSharp;

    using Server.TransportModels;

    class Program
    {
        private static IConfigurationRoot _configuration;
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true); ;

            _configuration = builder.Build();
            while (true)
            {
                SendData();
            }
        }

        static void SendData()
        {
            Console.WriteLine("Vor Data");
            var data = new Data
            {
                DeviceName = Environment.MachineName,
                Temperature = Monitoring.GetTemperature()
            };
            Console.WriteLine("Nach Data");
            Console.WriteLine($"DeviceName: {data.DeviceName}");
            Console.WriteLine($"Temperature: {data.Temperature}");

            var url = _configuration["ServerUrl"];
            Console.WriteLine($"Url: {url}");
            var client = new RestClient(url);
            var request = new RestRequest("api/Data", Method.POST);
            request.AddJsonBody(data);
            var response = client.Execute(request);
            if (!response.IsSuccessful)
            {
                Console.WriteLine("Fehler:");
                Console.WriteLine(response.ErrorMessage);
            }

            Console.WriteLine($"Gesendet: {DateTimeOffset.UtcNow}");
        }
    }
}
