using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MediaStore.Demo.API
{
    public class Program
    {
        public static void Main(string[] args) => BuildWebHost(args).Run();

        public static IWebHost BuildWebHost(string[] args)
        {
            var config = GetServerUrlsFromCommandLine(args);

            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel(options =>
                {
                    options.Limits.MaxConcurrentConnections = 10000;
                    options.Limits.MaxConcurrentUpgradedConnections = 1000;
                    options.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(30);
                    options.Limits.MaxRequestBodySize = 500000 * 1024;
                    options.Limits.MinRequestBodyDataRate =
                        new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(30));
                    options.Limits.MinResponseDataRate =
                        new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(30));
                })
                .UseConfiguration(config)
                .UseContentRoot(Directory.GetCurrentDirectory())
                ////.UseIISIntegration()
                .ConfigureAppConfiguration((hostingContext, c) =>
                {
                    c.AddJsonFile("api-appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.SetMinimumLevel(LogLevel.Error);
                })
                .UseStartup<Startup>()
                .Build();
        }

        public static IConfigurationRoot GetServerUrlsFromCommandLine(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();
            var serverport = config.GetValue<int?>("port") ?? 5001;
            var serverurls = config.GetValue<string>("server.urls") ?? string.Format("http://*:{0}", serverport);

            var configDictionary = new Dictionary<string, string>
                                   {
                                       { "server.urls", serverurls },
                                       { "port", serverport.ToString() },
                                   };

            return new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddInMemoryCollection(configDictionary)
                .Build();
        }
    }
}
