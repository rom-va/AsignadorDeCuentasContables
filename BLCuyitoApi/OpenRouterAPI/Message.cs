using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BLCuyitoApi.OpenRouterAPI
{
    public class Message
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("content")]
        public object Content { get; set; } // Can be string or List<ContentPart>
        [JsonPropertyName("tool_call_id")]
        public string? ToolCallId { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
