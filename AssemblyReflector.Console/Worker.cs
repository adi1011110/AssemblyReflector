using AssemblyReflector.Library.Models;
using AssemblyReflector.Library.Service;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AssemblyReflector.Console;

public class Worker : BackgroundService
{
    private readonly IAssemblyReflectorService _assemblyReflectorService;
    private readonly ILogger<Worker> _logger;
    private readonly AssemblyConfig _assemblyConfig;

    public Worker(IAssemblyReflectorService assemblyReflectorService,
        ILogger<Worker> logger,
        IOptions<AssemblyConfig> assemblyConfigOptions)
    {
        _assemblyReflectorService = assemblyReflectorService;
        _logger = logger;
        _assemblyConfig = assemblyConfigOptions.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker started");

        var result = _assemblyReflectorService.Reflect(_assemblyConfig);

        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(2000, cancellationToken);
        }

        _logger.LogInformation("Logger stopped");
    }
}
