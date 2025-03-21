var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<IAssemblyReflectorService, AssemblyReflectorService>();
        services.AddHostedService<Worker>();
        services.Configure<AssemblyConfig>(context.Configuration.GetSection(nameof(AssemblyConfig)));
    })
    .Build();

await host.RunAsync();