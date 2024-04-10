using System.Net.Mime;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using GptApi.Models.Chat;
using GptApi.Models.Embeding;
using Microsoft.Extensions.Logging;

namespace GptApi;

public class GptClient
{
    private readonly GptApiConfig _clientConfig;
    private readonly ILogger<GptClient> _log;
    private static JsonSerializerOptions _serializerSettings = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public GptClient(GptApiConfig clientConfig, ILogger<GptClient> log)
    {
        _clientConfig = clientConfig;
        _log = log;
    }

    public async Task<GptResponse> Chat(string userPrompt, string? systemPrompt = null)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _clientConfig.AuthorizationToken);


        var msg = new List<Chat>
        {
            new ChatUserMessage(userPrompt)
        };
        if (systemPrompt != null) msg.Add(new ChatSystemMessage(systemPrompt));
        string json = JsonSerializer.Serialize(new ChatCompletionsRequest(_clientConfig.UsedModel, msg), _serializerSettings);
        var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await httpClient.PostAsync(new Uri(new Uri(_clientConfig.Host), _clientConfig.Endpoits.Chat), content);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GptResponse>(jsonResponse) ?? throw new SerializationException(nameof(jsonResponse));
        }
        else
        {
            _log.LogError($@"Error: {response.StatusCode} - {response.ReasonPhrase}.");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(jsonResponse);
        }
    }

    public async Task<EmbedingResponse> Embedings(string prompt, string model)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _clientConfig.AuthorizationToken);

        string json = JsonSerializer.Serialize(new
        {
            input = prompt,
            model
        });
        var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await httpClient.PostAsync(new Uri(new Uri(_clientConfig.Host), _clientConfig.Endpoits.Embeddings), content);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EmbedingResponse>(jsonResponse) ?? throw new SerializationException(nameof(jsonResponse));
        }
        else
        {
            _log.LogError($@"Error: {response.StatusCode} - {response.ReasonPhrase}.");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(jsonResponse);
        }
    }
}
