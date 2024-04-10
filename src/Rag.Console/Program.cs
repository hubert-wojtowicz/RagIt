using GptApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<ConsoleChatService>();
        services.AddTransient<GptClient>();

        services.AddSingleton(typeof(GptApiConfig), sp =>
        {
            var gptApiConfig = hostContext.Configuration.GetSection("GptApiConfig").Get<GptApiConfig>() ?? throw new NullReferenceException(nameof(GptApiConfig));
            var config = sp.GetService<IConfiguration>() ?? throw new NullReferenceException(nameof(IConfiguration));
            string token = config["gpt-api-key"] ?? throw new NullReferenceException(nameof(token));
            gptApiConfig = gptApiConfig with { AuthorizationToken = token };

            return gptApiConfig;
        });

    })
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddUserSecrets<Program>();
    })
    .ConfigureLogging((hostContext, logging) =>
    {
        logging.AddConsole();
    })
    .Build();

var my = host.Services.GetRequiredService<ConsoleChatService>();
await my.Start();
