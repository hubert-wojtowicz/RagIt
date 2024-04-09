namespace GptApi;

public class GptClient
{
    private readonly GptApiConfig _clientConfig;

    public GptClient(GptApiConfig clientConfig)
	{
        _clientConfig = clientConfig;
    }

    public async Task<string> Chat(string prompt)
    {
        return _clientConfig.UsedModel;
    }
}
