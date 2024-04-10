namespace GptApi;

public record GptApiConfig(string Host, string UsedModel, GptEndpoits Endpoits, string? AuthorizationToken = null);

public record GptEndpoits(string Embeddings, string Chat);