namespace GptApi;

public record GptChatCompletionsRequest(string Model, List<GptRequest> Messages);

public record GptRequest(string Role, string Content);

public record GptSystemMessage(string Content) : GptRequest(MessageRole.System, Content);

public record GptUserMessage(string Content) : GptRequest(MessageRole.User, Content);

public class MessageRole
{
    public const string System = "system";
    public const string Assistant = "assistant";
    public const string User = "user";
    public const string Function = "function";
}