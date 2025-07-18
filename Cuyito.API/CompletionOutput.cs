using System.Text.Json.Serialization;

namespace Cuyito.API
{
    public class CompletionOutput
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("total_tokens_used")]
        public int TotalTokensUsed { get; set; }        
    }
}
