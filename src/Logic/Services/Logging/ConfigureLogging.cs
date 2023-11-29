using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Debugging;

namespace Logic.Services.Logging;

public static class ConfigureLogging
{
    public static IHostBuilder UseLogging(this IHostBuilder hostBuilder)
    {
        _ = hostBuilder.UseSerilog((hostBuilderContext, loggerConfiguration) =>
        {
            _ = loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration);
        });

        SelfLog.Enable(Console.WriteLine);

        return hostBuilder;
    }
}
