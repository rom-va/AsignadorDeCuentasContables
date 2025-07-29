using System.Text.Json.Serialization;

namespace CuyitoApi.Model
{
    public class TemplateFillingResponse
    {
        [JsonPropertyName("filled_templates")]
        public string FilledTemplates { get; set; }
               
        [JsonPropertyName("total_tokens_used")]
        public int TotalTokensUsed { get; set; }
        [JsonPropertyName("completion_http_status_code")]
        public int CompletionHttpStatusCode { get; set; }

    }
}
