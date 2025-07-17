using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AsignadorDeCuentasContables.DeepSeekAPI
{
    public class CompletionRequest
    {
        public List<RequestMessage> Messages { get; set; }
        public string Model { get; set; }

        [JsonPropertyName("frequency_penalty")]
        public double? FrequencyPenalty { get; set; }
        [JsonPropertyName("max_tokens ")]
        public int? MaxTokens { get; set; }
        [JsonPropertyName("presence_penalty")]
        public double? PresencePenalty { get; set; }
        [JsonPropertyName("response_format")]
        public ResponseFormat? ResponseFormat { get; set; }
        public object? Stop { get; set; }
        public double? Temperature { get; set; }
        [JsonPropertyName("top_p")]
        public double? TopP { get; set; }

        // Not all attributes have been implemented. To add funcionality, refer to the documentation

    }
    public class ResponseFormat
    {
        public string Type { get; set; }
    }
    public class RequestMessage
    {
        public string Content { get; set; }
        public string Role { get; set; }
        public string? Name { get; set; }
    }
}
