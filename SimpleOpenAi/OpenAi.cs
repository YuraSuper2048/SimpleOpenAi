using SimpleOpenAi.Endpoints;

namespace SimpleOpenAi;

public static class OpenAi
{
    public static OpenAiClient ClientInstance = new();
    
    public static string ApiKey
    {
        get => ClientInstance.ApiKey;
        set => ClientInstance.ApiKey = value;
    }
    
    public static string ApiBase
    {
        get => ClientInstance.ApiBase;
        set => ClientInstance.ApiBase = value;
    }
    
    public static ChatCompletions Chat => ClientInstance.Chat;
    
    public static ImageGenerations Images => ClientInstance.Images;
    
    public static Embeddings Embeddings => ClientInstance.Embeddings;
}