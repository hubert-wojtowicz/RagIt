using Microsoft.Extensions.Logging;
using GptApi;

public class ConsoleChatService
{
    private readonly ILogger<ConsoleChatService> _logger;
    private readonly GptClient _client;

    public ConsoleChatService(
        ILogger<ConsoleChatService> logger,
        GptClient client)
    {
        _logger = logger;
        _client = client;
    }

    public async Task Start()
    {
        bool @continue = true;
        do
        {
            try
            {
                @continue = await Next();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed this time :( ...");
            }
        } while (@continue);
    }

    public async Task<bool> Next()
    {
        Console.Write("> ");
        string prompt = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(prompt)) return false;
        var resp = await _client.Chat(prompt);
        Console.WriteLine(resp.Choices.First().Message.Content);

        return true;
    }
}