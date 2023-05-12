using Golden.Raspberry.Awards.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Golden.Raspberry.Awards.Api
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
            => CreateHostBuilder(args).Build().Seed().Run();

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder)
                    => builder
                        .AddEnvironmentVariables()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true))
                .ConfigureLogging((hostingContext, logging)
                    => logging
                        .SetMinimumLevel(Enum.Parse<LogLevel>(hostingContext.Configuration["LogLevel"])))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseStartup<Startup>()
                    .UseKestrel((context, options) =>
                    {
                        if (context.HostingEnvironment.IsDevelopment()) return;

                        options.AddServerHeader = false;
                        options.ListenAnyIP(context.Configuration.GetValue<int>("ApplicationPort"), configure => configure.Protocols = HttpProtocols.Http1AndHttp2);
                    });
                });
    }
}
