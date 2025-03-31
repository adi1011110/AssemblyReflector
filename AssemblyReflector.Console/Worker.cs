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

        try
        {
            var result = _assemblyReflectorService.Reflect(_assemblyConfig);

            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)
                .Parent.Parent.Parent.FullName;

            string filePath = Path.Combine(projectDirectory, "output.json");

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(result);
            }
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex.Message);
        }
        finally
        {
            _logger.LogInformation("Worker stopped");
        }
    }
}
