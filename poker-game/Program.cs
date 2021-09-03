using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace poker_game
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();

            //Adding the appsettings.json files to the configurationBuilder 
            BuildConfiguration(builder);

            //Executing the builder, reading the configuration and adding Logging.
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
            Log.Logger.Information("***Starting (environment: {environment})***",
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "default");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(
                (context, services) =>
                {
                    services.AddTransient<IPokerService, PokerService>();
                    services.AddTransient<IPlayer, Player>();
                    services.AddTransient<IDeck, Deck>();
                    services.AddTransient<IEvaluator, Evaluator>();
                }
                )
                .UseSerilog()
                .Build();
            Log.Logger.Information("Press 'Y' to start a new game.");
            Log.Logger.Information("Press ESC to stop");
            do
            {
                while (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    Log.Logger.Information("\n+++++++++++++++++++++++++++++++++++++" +
                                           "\n++++++++++++ New Game +++++++++++++++" +
                                           "\n+++++++++++++++++++++++++++++++++++++");

                    var service = ActivatorUtilities.CreateInstance<PokerService>(host.Services);
                    service.Run();
                    Log.Logger.Information("Press 'Y' to start a new game.");
                }
            } while ((Console.ReadKey(true).Key != ConsoleKey.Escape));
        }


        /// <summary>
        /// Setting up talking to the appsettings files: 
        /// Adding the ability to talk to the appsettings.production.json and appsettings.development.json.
        /// Environment variables can override the appsettings files.
        /// </summary>
        /// <param name="builder"></param>
        static void BuildConfiguration(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{ Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}
