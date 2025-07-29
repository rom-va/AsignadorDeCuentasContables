using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BLCuyitoApi.DeepSeekAPI
{
    /*
    * Represents the JSON-deserializable body of an HTTP response returned from a completion request.
    */
    internal class CompletionResponse
    {
        public string Id { get; set; }
        public List<Choice> Choices { get; set; }
        public int Created { get; set; }
        public string Model { get; set; }
        [JsonPropertyName("system_fingerprint")]
        public string SystemFingerprint { get; set; }
        public string Object { get; set; }
        public Usage Usage { get; set; }

        public string GetJson()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true
            };

            return JsonSerializer.Serialize(this, options);
        }
    }
    public class Usage
    {
        [JsonPropertyName("completion_tokens")]
        public int CompletionTokens { get; set; }
        [JsonPropertyName("prompt_tokens")]
        public int PromptTokens { get; set; }
        [JsonPropertyName("prompt_cache_hit_tokens")]
        public int PromptCacheHitTokens { get; set; }
        [JsonPropertyName("prompt_cache_miss_tokens")]
        public int PromptCacheMissTokens { get; set; }
        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }
        [JsonPropertyName("completion_tokens_details")]
        public CompletionTokensDetail CompletionTokensDetails { get; set; }

        // New addition (not on official documentation yet as of 17.07.2025
        [JsonPropertyName("prompt_tokens_details")]
        public PromptTokensDetail PromptTokensDetails { get; set; }

    }
    public class PromptTokensDetail
    {
        [JsonPropertyName("cached_tokens")]
        public int CachedTokens { get; set; }
    }
    public class CompletionTokensDetail
    {
        [JsonPropertyName("reasoning_tokens")]
        public int ReasoningTokens { get; set; }
    }

    public class Choice
    {
        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }
        public int index { get; set; }
        public ResponseMessage Message { get; set; }
        [JsonPropertyName("logprobs")]
        public LogProb? LogProbs { get; set; }
    }
    public class LogProb
    {
        public List<Content>? Content { get; set; }
    }

    public class Content
    {
        public string Token { get; set; }
        [JsonPropertyName("logprob")]
        public double LogProb { get; set; }
        public int[]? Bytes { get; set; }
        [JsonPropertyName("top_logprobs")]
        public List<TopLogContent> TopLogProbs { get; set; }
    }
    public class TopLogContent
    {
        public string Token { get; set; }
        [JsonPropertyName("logprob")]
        public double LogProb { get; set; }
        public int[]? Bytes { get; set; }
    }

    public class ResponseMessage
    {
        public string? Content { get; set; }
        [JsonPropertyName("reasoning_content")]
        public string? ReasoningContent { get; set; }
        [JsonPropertyName("tool_calls")]
        public List<ToolCall>? ToolCalls { get; set; }
        public string Role { get; set; }

    }
    public class ToolCall
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public Function function { get; set; }
    }
    public class Function
    {
        public string Name { get; set; }
        public string Arguments { get; set; }
    }    
}