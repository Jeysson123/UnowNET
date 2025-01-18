using Apis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        try
        {
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Application stopped due to an exception");
            throw;
        }
        finally
        {
            NLog.LogManager.Shutdown();  // Ensure proper shutdown of NLog
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseNLog(); // Use NLog as the logging provider

}
