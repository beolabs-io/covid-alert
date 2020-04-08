using System;
using System.Collections.Generic;
using Covid.Database;
using Covid.Models.Entities;
using Covid.Services;
using Covid.Services.UserService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Covid.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup DI Service Collection
            ServiceCollection serviceCollection = new ServiceCollection();
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("config.json", optional: false, reloadOnChange: true).Build();
            ConfigureServices(serviceCollection, configuration);

            // Setup DI Service Provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Instanciate services for Program Main method use
            var _loggerService = serviceProvider.GetService<ILogger<Program>>();
            var _userService = serviceProvider.GetService<UserService>();

            #region - PROGRAM -

            Console.WriteLine("-- COVID19 TRACKER --");

            ConsoleKey commandKey = ConsoleKey.H;

            while (!Console.KeyAvailable)
            {
                if (commandKey == ConsoleKey.Spacebar)
                {
                    break;
                }
                else if (commandKey == ConsoleKey.H) // Help
                {
                    Console.WriteLine();
                    Console.WriteLine("\n## Help \n");
                    Console.WriteLine("> quit app         : [spacebar]");
                    Console.WriteLine("> print help       : [h]");
                    Console.WriteLine("> pull all users   : [u]"); 
                    Console.WriteLine("> pull all matches : [m]");
                    Console.WriteLine("> pull all alerts  : [a]"); 
                    Console.WriteLine("> subscribe        : [s]"); 
                    Console.WriteLine("> unsubscribe      : [x]");  
                    Console.WriteLine("> push match       : [p]");  
                    Console.WriteLine("> pull match       : [g]");    
                    Console.WriteLine("> push alert       : [e]");    
                    Console.WriteLine("> remove alert     : [c]"); 
                    Console.WriteLine("> pull alerts      : [z]");
                    Console.WriteLine("> pull infections  : [i]");
                } 
                else if (commandKey == ConsoleKey.U) // All Users
                {
                    var result = _userService.AdminPullUsers();
                    if (result.Exception == null)
                    {
                        Console.WriteLine("\n");
                        if(result.Value.Count <= 0) Console.WriteLine("No user found.");

                        foreach (var user in result.Value)
                        {
                            Console.WriteLine($" - { user.Key }");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\nUnable to list users. Error : { result.Exception.Message }");
                    }
                } 
                else if (commandKey == ConsoleKey.M) // All matches
                {
                    var result = _userService.AdminPullMatches();
                    if (result.Exception == null)
                    {
                        Console.WriteLine("\n");
                        if (result.Value.Count <= 0) Console.WriteLine("No match found.");

                        foreach (var match in result.Value)
                        {
                            Console.WriteLine($" - { match.When }, { match.UserX.Key } matched with { match.UserY.Key }");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\nUnable to list matches. Error : { result.Exception.Message }");
                    }
                } 
                else if (commandKey == ConsoleKey.A) // All alerts
                {
                    var result = _userService.AdminPullAlerts();
                    if (result.Exception == null)
                    {
                        Console.WriteLine("\n");
                        if (result.Value.Count <= 0) Console.WriteLine("No alert found.");

                        foreach (var alert in result.Value)
                        {
                            Console.WriteLine($" - { alert.When }, emitted by { alert.User.Key }");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\nUnable to list alerts. Error : { result.Exception.Message }");
                    }
                } 
                else if (commandKey == ConsoleKey.S) // Subscribe
                {
                    var result = _userService.Subscribe();
                    if (result.Exception == null)
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine($" - User Key: { result.Value.Key }");
                    }
                    else
                    {
                        Console.WriteLine($"\nSubscription failed. Error : { result.Exception.Message }");
                    }
                } 
                else if (commandKey == ConsoleKey.X)  // Unsubscribe
                {
                    Console.WriteLine("Your Key: ");
                    string key = Console.ReadLine();

                    var result = _userService.Unsubcribe(key);
                    if (result.Exception == null)
                    {
                        Console.WriteLine($"\nSuccessfully unsubscribed.");
                    }
                    else
                    {
                        Console.WriteLine($"\nUnsubscription failed. Error : { result.Exception.Message }");
                    }
                }
                else if (commandKey == ConsoleKey.P) // Push match
                {
                    Console.WriteLine("Your Key: ");
                    string userKey = Console.ReadLine();

                    Console.WriteLine("Matched Key: ");
                    string matchedKey = Console.ReadLine();

                    var result = _userService.PushMatch(userKey, matchedKey, DateTime.Now);
                    if (result.Exception == null)
                    {
                        Console.WriteLine($"\nMatch successfully pushed !");
                    }
                    else
                    {
                        Console.WriteLine($"\nPushing match failed. Error : { result.Exception.Message }");
                    }
                } 
                else if (commandKey == ConsoleKey.G) // Pull match
                {
                    Console.WriteLine("Your Key: ");
                    string key = Console.ReadLine();

                    var result = _userService.PullMatches(key);
                    if (result.Exception == null)
                    {
                        Console.WriteLine();
                        if (result.Value.Count <= 0) Console.WriteLine("No match found.");

                        foreach (var match in result.Value)
                        {
                            if (match.UserX.Key.ToString() == key)
                            { // You matched with someone

                                Console.WriteLine($" - { match.When }, You matched with { match.UserY.Key }");
                            }
                            else
                            { // Someone matched with you

                                Console.WriteLine($" - { match.When }, You matched with { match.UserX.Key }");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\nPulling matches failed. Error : { result.Exception.Message }");
                    }
                } 
                else if (commandKey == ConsoleKey.E) // Push alert
                {
                    Console.WriteLine("Your Key: ");
                    string key = Console.ReadLine();

                    var result = _userService.PushAlert(key, DateTime.Now);
                    if (result.Exception == null)
                    {
                        Console.WriteLine("\nAlert successfully emitted");
                    }
                    else
                    {
                        Console.WriteLine($"\nFailed to emit alert. Error : { result.Exception.Message }");
                    }
                } 
                else if (commandKey == ConsoleKey.C) // Remove alert
                {
                    Console.WriteLine("Your Key: ");
                    string key = Console.ReadLine();

                    var result = _userService.RemoveAlert(key);
                    if (result.Exception == null)
                    {
                        Console.WriteLine("\nAlert successfully pushed");
                    }
                    else
                    {
                        Console.WriteLine($"\nFailed to push alert. Error : { result.Exception.Message }");
                    }
                } 
                else if (commandKey == ConsoleKey.Z) // User alert
                {
                    Console.WriteLine("Your Key: ");
                    string key = Console.ReadLine();

                    var result = _userService.PullAlerts(key);
                    if (result.Exception == null)
                    {
                        Console.WriteLine();
                        if (result.Value.Count <= 0) Console.WriteLine("No alert found.");

                        foreach (var alert in result.Value)
                        {
                            Console.WriteLine($" - Active alert, emitted: { alert.When }");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\nFailed to get user alert. Error : { result.Exception.Message }");
                    }
                }
                else if (commandKey == ConsoleKey.I) // Infection alerts
                {
                    Console.WriteLine("Your Key: ");
                    string key = Console.ReadLine();

                    Console.WriteLine("\nIncubation days: ");
                    int incubation;

                    if (int.TryParse(Console.ReadLine(), out incubation))
                    {
                        var result = _userService.PullInfections(key, incubation);
                        if (result.Exception == null)
                        {
                            if (result.Value.Count > 0)
                            {
                                Console.WriteLine();
                                foreach(var infectionDate in result.Value)
                                {
                                    Console.WriteLine($" - You've crossed someone infected: { infectionDate }");
                                }

                                Console.WriteLine("\nIt would be safer to contact a doctor as soon as possible.");
                            }
                            else
                            {
                                Console.WriteLine($"It seams you've not crossed someone else at risk last { incubation } days");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"\nFailed to pull desease alerts. Error : { result.Exception.Message }");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Invalid incubation days value");
                    }
                }
                else
                {
                    Console.WriteLine("\nUnknown command.");
                }

                Console.Write("\nChoose a command: ");
                commandKey = Console.ReadKey(true).Key;
            }

            #endregion
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Register sevices
            services
                .AddLogging(configure => configure.AddConsole())
                .AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgresDB")))
                .AddTransient<UserService>()
            ;

            // Configure logger
            LogLevel minLogLevel;
            switch (configuration.GetSection("Logger")["MinLogLevel"].ToLower())
            {
                default:
                case "trace":
                    minLogLevel = LogLevel.Trace;
                    break;

                case "debug":
                    minLogLevel = LogLevel.Debug;
                    break;

                case "information":
                    minLogLevel = LogLevel.Information;
                    break;

                case "warning":
                    minLogLevel = LogLevel.Warning;
                    break;

                case "error":
                    minLogLevel = LogLevel.Error;
                    break;

                case "critical":
                    minLogLevel = LogLevel.Critical;
                    break;

                case "none":
                    minLogLevel = LogLevel.None;
                    break;
            }
            services.Configure<LoggerFilterOptions>(options => options.MinLevel = minLogLevel);
        }
    }
}
