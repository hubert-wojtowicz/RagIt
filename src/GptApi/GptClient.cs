using System.Net.Mime;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

namespace GptApi;

public class GptClient
{
    private readonly GptApiConfig _clientConfig;
    private static JsonSerializerOptions _serializerSettings = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public GptClient(GptApiConfig clientConfig)
    {
        _clientConfig = clientConfig;
    }

    public async Task<GptResponse> Chat(string userPrompt, string? systemPrompt = null)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _clientConfig.AuthorizationToken);


        var msg = new List<GptRequest>
        {
            new GptUserMessage(userPrompt)
        };
        if (systemPrompt != null) msg.Add(new GptSystemMessage(systemPrompt));
        string json = JsonSerializer.Serialize(new GptChatCompletionsRequest(_clientConfig.UsedModel, msg), _serializerSettings);
        var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await httpClient.PostAsync(new Uri(new Uri(_clientConfig.Host), _clientConfig.Endpoits.Chat), content);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GptResponse>(jsonResponse) ?? throw new SerializationException(nameof(jsonResponse));
        }
        else
        {
            // Print error message

            Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            throw new InvalidOperationException(nameof(jsonResponse));  // TODO
        }
            }

    public async Task<object> Embedings(string prompt, string model)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _clientConfig.AuthorizationToken);

        string json = JsonSerializer.Serialize(new
        {

        });
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(_clientConfig.Host, content);

        return _clientConfig.UsedModel;
    }
}
