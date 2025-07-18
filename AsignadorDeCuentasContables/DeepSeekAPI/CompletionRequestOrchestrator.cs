using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using System.Net.Http.Headers;

namespace AsignadorDeCuentasContables.DeepSeekAPI
{
    public class CompletionRequestOrchestrator
    {
        public string LlmModelName { get; set; }
        public string ApiKey { get; set; }
        public string AccountingChart { get; set; }
        public string TemplateFormat { get; set; }
        public string LoadedTemplates { get; set; }
        public string FilledTemplates { get; set; }

        public StringContent HttpPostRequestContent { get; set; }
        public Response Response { get; set; }

        public void BuildRequest()
        {
            CompletionRequest requestData = BuildRequestData();
            string json = JsonSerializeRequestData(requestData);
            StringContent httpRequest = BuildHttpRequestBody(json);
            HttpPostRequestContent = httpRequest;
        }

        public string ExecuteRequest()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.deepseek.com");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
            var response = client.PostAsync("/chat/completions", HttpPostRequestContent).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                string textContent = getResponseTextContent(responseContent);
                FilledTemplates = textContent;
                return textContent;
            }
            else
            {
                return "Error: " + response.StatusCode;
            }
        }


        private CompletionRequest BuildRequestData()
        {
            string systemInstuctions =
               "You are an accounting bookkeeping expert well versed in current peruvian accounting practices. " +
               $"You will receive two text files containing the following data in { TemplateFormat } format: " +
               "1. One chart of accounts that follows the peruvian Plan Contable General Empresarial. " +
               "2. A series of rows containing transactions." +
               "I need you to fill the column named \"Cuenta\" of each transaction with " +
               "the account that precisely describes it. Use only the chart of accounts provided and do not" +
               "modify any other column. Always select an account with the largest number of digits" +
               "in its class." +
               "Do not answer in natural language. Provide only the filled rows using the same " +
               "format in which it was sent.";

            List<RequestMessage> messages = new List<RequestMessage>
            {
                new RequestMessage { Content=systemInstuctions, Role="system" },
                new RequestMessage { Content=AccountingChart, Role="user" },
                new RequestMessage { Content=LoadedTemplates, Role="user" }
            };

            CompletionRequest requestData = new CompletionRequest
            {
                Messages = messages,
                Model = LlmModelName,
            };

            return requestData;
        }

        private string JsonSerializeRequestData(CompletionRequest requestData)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,            // camelCase
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, // All nulled attributes will be ignored when generating the JSON
                WriteIndented = true
            };

            string jsonBody = JsonSerializer.Serialize(requestData, options);
            return jsonBody;
        }

        private StringContent BuildHttpRequestBody(string jsonBody)
        {
            StringContent httpRequestContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            return httpRequestContent;
        }

        private string getResponseTextContent(string responseContent)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            Response response = JsonSerializer.Deserialize<Response>(responseContent, options);

            string result = "";
            if (response != null)
            {
                Response = response;                
                foreach (Choice choice in response.Choices)
                {
                    result += choice.Message.Content;
                }                
            }
            
            return result;
        }

    }
}
