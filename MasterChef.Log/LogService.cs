using Serilog;
using Microsoft.Extensions.Hosting;

namespace MasterChef.Log
{
    public static class LogService
    {
        public static void Configure(IHostBuilder builderHost)
        {
            builderHost.UseSerilog((ctx, lc) => lc
              .Enrich.WithMachineName()
              .Enrich.WithEnvironmentUserName()
              .Enrich.WithAssemblyVersion()
              .Enrich.WithMemoryUsage()
              .WriteTo.Async(w =>
              {
                  w.Console(Serilog.Events.LogEventLevel.Verbose);
                  w.File(
                      Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "logs", "log_.txt"),
                      rollingInterval: RollingInterval.Day,
                      fileSizeLimitBytes: 1024 * 1024 * 20,
                      buffered: true,
                      flushToDiskInterval: TimeSpan.FromSeconds(1),
                      rollOnFileSizeLimit: true,
                      outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u4}] | {AssemblyVersion} | {MemoryUsage} | {Message:l}{NewLine}{Exception}"
                   );
              }, bufferSize: 500)
            );
        }

        public static void CloseAndFlush()
        {
            Serilog.Log.CloseAndFlush();
        }

    }
}