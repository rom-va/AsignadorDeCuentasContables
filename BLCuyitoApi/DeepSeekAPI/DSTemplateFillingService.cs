using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace BLCuyitoApi.DeepSeekAPI
{
    /*
     * Service capable of formulating and executing the HTTP request needed to fill the templates sent by the user.
     * Returns filled templates, token usage and the HTTP status code from the HTTP request done to DeepSeek
     */
    public class DSTemplateFillingService : ITemplateFillingService
    {
        private string FilledTemplates;
        private CompletionResponse Response;

        public HttpResponseMessage FillTemplates(
            string llmModel,
            string accountingChart,
            string templateFormat,
            string loadedTemplates
            )
        {
            StringContent httpBody = BuildRequest(llmModel, templateFormat, accountingChart, loadedTemplates);
            string apiKey = GetApiKey();
            return ExecuteRequest(apiKey, httpBody);

        }
        public int GetTokenUsage()
        {
            return Response.Usage.TotalTokens;
        }
        public string GetFilledTemplates()
        {
            return FilledTemplates;
        }        

        private string GetApiKey()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            return config["apisettings:deepseekapikey"];
        }
        private StringContent BuildRequest(string llmModel, string templateFormat, string accountingChart, string loadedTemplates)
        {
            CompletionRequest requestData = BuildRequestData(llmModel, templateFormat, accountingChart, loadedTemplates);
            string json = JsonSerializeRequestData(requestData);
            StringContent httpRequest = BuildHttpRequestBody(json);
            return httpRequest;
        }

        private HttpResponseMessage ExecuteRequest(string apiKey, StringContent httpBody)
        {   
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.deepseek.com");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            var response = client.PostAsync("/chat/completions", httpBody).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                string textContent = GetResponseTextContent(responseContent);
                FilledTemplates = textContent;
                return new HttpResponseMessage(response.StatusCode);
            }
            else
            {
                return new HttpResponseMessage(response.StatusCode);
            }
        }


        private CompletionRequest BuildRequestData(string llmModel, string templateFormat, string accountingChart, string loadedTemplates)
        {
            string systemInstuctions =
               "You are an accounting bookkeeping expert well versed in current peruvian accounting practices. " +
               $"You will receive two text files containing the following data in { templateFormat } format: " +
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
                new RequestMessage { Content=accountingChart, Role="user" },
                new RequestMessage { Content=loadedTemplates, Role="user" }
            };

            CompletionRequest requestData = new CompletionRequest
            {
                Messages = messages,
                Model = llmModel,
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

        private string GetResponseTextContent(string responseContent)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            CompletionResponse response = JsonSerializer.Deserialize<CompletionResponse>(responseContent, options);

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
