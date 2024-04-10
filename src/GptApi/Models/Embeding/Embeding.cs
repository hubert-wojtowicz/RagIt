using System.Text.Json.Serialization;

namespace GptApi.Models.Embeding;
public record Datum(
    [property: JsonPropertyName("object")] string Object,
    [property: JsonPropertyName("index")] int Index,
    [property: JsonPropertyName("embedding")] IReadOnlyList<double> Embedding
);

public record EmbedingResponse(
    [property: JsonPropertyName("object")] string Object,
    [property: JsonPropertyName("data")] IReadOnlyList<Datum> Data,
    [property: JsonPropertyName("model")] string Model,
    [property: JsonPropertyName("usage")] Usage Usage
);

public record Usage(
    [property: JsonPropertyName("prompt_tokens")] int PromptTokens,
    [property: JsonPropertyName("total_tokens")] int TotalTokens
);