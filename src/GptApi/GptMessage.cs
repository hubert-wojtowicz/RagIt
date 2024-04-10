namespace GptApi;
public record GptMessage(string Role, string Message);

public record GptSystemMessage(string Message) : GptMessage("System", Message);

public record GptUserMessage(string Message) : GptMessage("User", Message);