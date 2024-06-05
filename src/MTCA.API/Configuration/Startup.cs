namespace MTCA.API.Configuration;

/// <summary>
/// Startup
/// </summary>
public static class Startup
{
    /// <summary>
    /// AddConfigurations
    /// </summary>
    /// <param name="host"></param>
    /// <returns></returns>
    public static ConfigureHostBuilder AddConfigurations(this ConfigureHostBuilder host)
    {
        host.ConfigureAppConfiguration((context, config) =>
        {
            const string configurationsDirectory = "Configuration";
            var env = context.HostingEnvironment;

            config
            .AddJsonFile($"{configurationsDirectory}/appsettings.json", optional: false, true)
            .AddJsonFile($"{configurationsDirectory}/logger.json", optional: false, true)
            .AddJsonFile($"{configurationsDirectory}/cache.json", optional: false, true);

        });



        return host;
    }
}

