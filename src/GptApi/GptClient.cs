using System.Text;
using System.Text.Json;

namespace GptApi;

public class GptClient
{
    private readonly GptApiConfig _clientConfig;

    public GptClient(
        GptApiConfig clientConfig)
    {
        _clientConfig = clientConfig;
    }

    public async Task<string> Chat(string userPrompt, string systemPrompt = null)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _clientConfig.AuthorizationToken);

        string json = JsonSerializer.Serialize(new GptMessage[]
        {
            new GptSystemMessage(systemPrompt),
            new GptUserMessage(userPrompt)
        });
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(_clientConfig.Host, content);

        return _clientConfig.UsedModel;
    }

    public async Task<object> Embedings(string prompt, string model)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _clientConfig.AuthorizationToken);

        string json = JsonSerializer.Serialize(new {
        
        });
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(_clientConfig.Host, content);

        return _clientConfig.UsedModel;
    }
}
