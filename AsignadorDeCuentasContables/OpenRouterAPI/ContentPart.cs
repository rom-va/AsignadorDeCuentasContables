using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AsignadorDeCuentasContables.OpenRouterAPI
{
    internal abstract class ContentPart
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    internal class TextContent : ContentPart
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

    }
    internal class ImageContentPart : ContentPart
    {
        [JsonPropertyName("image_url")]
        public ImageUrl ImageUrl { get; set; }
    }
    internal class ImageUrl
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("detail")]
        public string? Detail { get; set; }
    }
}
