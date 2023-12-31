# Embeddings

https://platform.openai.com/docs/api-reference/embeddings

Differences from OpenAI doc:
- `encoding_format` parameter is decided by `CreateAsync` (base64) and `CreateFloatsAsync` (float)
  - `CreateAsync` will still have a float array to use
- Shortcuts for results, `result.Data[0].Embedding` can be replaced with `result.Embedding`


## Examples:
Single:
```csharp
using SimpleOpenAi;
using Newtonsoft.Json;

var result = await OpenAi.Embeddings.CreateAsync("Meow!");

var embeddingJson = JsonConvert.SerializeObject(result.Embedding);
Console.WriteLine(embeddingJson);
```
Multiple:
```csharp
using SimpleOpenAi;
using Newtonsoft.Json;

var result = await OpenAi.Embeddings.CreateAsync(new [] {"Meow!", "A cat"});

foreach(var data in result.Data)
{
    var embeddingJson = JsonConvert.SerializeObject(data.Embedding);
    Console.WriteLine(embeddingJson);
}
```