# Chat completions

https://platform.openai.com/docs/api-reference/chat

Differences from OpenAI doc:
- `stream` parameter is decided by either `CreateStreaming` or `CreateAsync`
- `tool_choice` can only be a string
- Vision is not currently supported
- Shortcuts for results, `result.Choices[0].Message.Content` can be replaced with `result.Content` or `(string)result`

## Examples:
Streaming:
```csharp
using SimpleOpenAi;

var messages = new ChatMessage[]
{
    new("user", "hi")
};

var stream = OpenAi.Chat.CreateStreaming(messages, model: "gpt-4-1106-preview");

await foreach (var chunk in stream)
{
    Console.Write(chunk.Content); // or chunk.Choices[0].Delta.Content
}
```
Non-streaming:
```csharp
using SimpleOpenAi;

var messages = new ChatMessage[]
{
    new("user", "hi")
};

var result = await OpenAi.Chat.CreateAsync(messages, model: "gpt-4-1106-preview");

Console.Write(result.Content); // or result.Choices[0].Message.Content
```
Function calls (non-streaming, but CreateStreaming supports it too):
```csharp
using SimpleOpenAi;
using SimpleOpenAi.Endpoints;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;

var messages = new ChatMessage[]
{
    new("user", "whats the weather in la")
};

var tools = new ChatCompletions.ToolDeclaration[]
{
    new(Type: "function", FunctionDeclaration: 
        new("get_current_weather",
            "Get the current weather in a given location",
            JSchema.Parse("""
            {
               "type": "object",
               "properties": {
                   "location": {
                       "type": "string",
                       "description": "The city and state, e.g. San Francisco, CA"
                   },
                   "unit": {
                       "type": "string",
                       "enum": [
                       "celsius",
                       "fahrenheit"
                       ]
                   }
               },
               "required": [
               "location"
               ]
           }
           """)))
};

var result = await OpenAi.Chat.CreateAsync(messages, 
    model: "gpt-4-1106-preview", tools: tools);

var resultJson = JsonConvert.SerializeObject(result.Choices[0].Message, Formatting.Indented);
Console.WriteLine(resultJson);
```