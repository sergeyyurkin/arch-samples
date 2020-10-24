using System;
using Microsoft.Extensions.Configuration;

namespace Ordering.Migrator
{
    class Program
    {
        private static bool _quietMode;

        static void Main(string[] args)
        {
            ParseArgs(args);

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var migrationSucceeded = new MigrateDbExecuter(configuration["ConnectionString"]).Run();

            if (_quietMode)
            {
                var exitCode = Convert.ToInt32(!migrationSucceeded);
                Environment.Exit(exitCode);
            }
            else
            {
                Console.WriteLine("Press ENTER to exit...");
                Console.ReadLine();
            }
        }

        private static void ParseArgs(string[] args)
        {
            if (args == null)
            {
                return;
            }

            foreach (var arg in args)
            {
                switch (arg)
                {
                    case "-q": _quietMode = true; break;
                }
            }
        }
    }
}
