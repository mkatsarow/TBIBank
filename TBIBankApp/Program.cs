using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using TBIApp.Services.Services;
using TBIApp.Services.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.Extensions.Logging.Configuration;
using System;
using Serilog.Events;

namespace TBIBankApp
{
    public class Program
    {


        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File("TBILogger\\logger-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Application starting up!");
                CreateWebHostBuilder(args).Build().Run();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed to start!");
            }
            finally
            {
                Log.CloseAndFlush();
            }


        }



        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
        }
    }
}
