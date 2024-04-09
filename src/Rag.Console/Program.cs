using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddTransient<Chat>(); })
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddUserSecrets<Program>();
    })
    .Build();

var my = host.Services.GetRequiredService<Chat>();
await my.Start();

public class Chat
{
    private readonly IConfiguration _cofig;

    public Chat(IConfiguration cofig)
    {
        _cofig = cofig;
    }

    public async Task Start()
    {
        string gptApiKey = _cofig["gpt-api-key"];
        Console.WriteLine($"API Key: {gptApiKey}");

        Thread.Sleep(TimeSpan.MaxValue);
    }
}