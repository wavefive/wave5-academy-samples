using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wave5.AcademyServices;
using Wave5.AcademyServices.ConsoleApp;
using Wave5.AcademyServices.Data;


var builder = new HostBuilder();
builder.ConfigureAppConfiguration(context => {
    context.AddJsonFile("appsettings.json");
});

builder.ConfigureLogging((context, builder) => {
    builder.AddConsole();
});

builder.ConfigureServices((context, services) => {

    services.AddAcademyServicesLogicProviders();
    services.AddAcademyServicesDataProviders(); // NOTE: we have added a reference to Wave5.AcademyServices.MockDataProviders 
    services.AddTransient<StudentConsoleWriterProvider>();
});

// ------------------------------------------------------------------
var host = builder.Build();
var service = host.Services;
var loggerFactory = service.GetService<ILoggerFactory>()!;
var logger = loggerFactory.CreateLogger("Main");
// ------------------------------------------------------------------

Console.WriteLine("Wave5 Academy Service Console.");

var studentWriter = service.GetService<StudentConsoleWriterProvider>()!;
await studentWriter.WriteAllAsync();

Console.WriteLine("Done");
Console.ReadLine();