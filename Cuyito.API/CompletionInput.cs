using System.Text.Json.Serialization;

namespace Cuyito.API
{
    public class CompletionInput
    {
        [JsonPropertyName("api_key")]
        public string ApiKey { get; set; }
        [JsonPropertyName("model_name")]
        public string LlmModelName { get; set; }
        [JsonPropertyName("template_format")]
        public string TemplateFormat { get; set; }
        [JsonPropertyName("accounting_chart")]
        public string AccountingChart { get; set; }
        [JsonPropertyName("transactions")]
        public string Transactions { get; set; }
    }
}
