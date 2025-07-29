using System.Text.Json.Serialization;

namespace CuyitoApi.Model
{
    public class TemplateFillingRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("template_format")]
        public string TemplateFormat { get; set; }
        [JsonPropertyName("accounting_chart")]
        public string AccountingChart { get; set; }
        [JsonPropertyName("transactions")]
        public string IncompleteTemplates { get; set; }

        public bool HasAnyNullAttributes()
        {
            if (Username == null) return true;
            if (Password == null) return true;
            if (TemplateFormat == null) return true;
            if (AccountingChart == null) return true;
            if (IncompleteTemplates == null) return true;

            return false;
        }
    }
}
