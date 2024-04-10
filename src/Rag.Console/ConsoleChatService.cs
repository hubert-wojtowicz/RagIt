using Microsoft.Extensions.Logging;
using GptApi;

public class ConsoleChatService
{
    private readonly ILogger<ConsoleChatService> _logger;
    private readonly GptClient _client;

    public ConsoleChatService(
        ILogger<ConsoleChatService> logger,
        GptClient client        )
    {
        _logger = logger;
        _client = client;
    }

    public async Task Start()
    {
        while (true)
        {
            Console.Write("> ");
            string prompt = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(prompt)) break;

            Console.WriteLine(await Chat(prompt));
        }
    }

    private Task<string> Chat(string prompt)
    {
        return _client.Chat(prompt);
    }
}