using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using AsignadorDeCuentasContables.DeepSeekAPI;
using System.Net.Http.Headers;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using System.Reflection.Metadata;

namespace Tests
{
    internal class DeepSeekTests
    {
        internal static void RequestSerializationTest()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Request Serialization Test");
            Console.WriteLine("------------------------------------------");

            // Creation of the CompletionRequest Object

            List<RequestMessage> messages = new List<RequestMessage>
            {
                new RequestMessage { Content="You are an assistant that counts the numbers of messages. Please respond with the number of messages sent by the user.", Role="system" },
                new RequestMessage { Content="This is message number 1.", Role="user" },
                new RequestMessage { Content="This is message number 2.", Role="user" },
                new RequestMessage { Content="This is message number 3.", Role="user" }
            };


            CompletionRequest requestData = new CompletionRequest
            {
                Messages = messages,
                Model = "deepseek-chat",
            };

            // JSON Serialization of the CompletionRequest using System.Text.Json

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,            // camelCase
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, // All nulled attributes will be ignored when generating the JSON
                WriteIndented = true
            };

            string jsonBody = JsonSerializer.Serialize(requestData, options);
            Console.WriteLine("CompletionRequest Object serialized to JSON:\n");
            Console.WriteLine(jsonBody);

            // Handling the DeepSeek API Key

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            string apiKey = config["apisettings:deepseekapikey"];

            Console.WriteLine(apiKey);

            // Making the HTTP POST Request

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.deepseek.com");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            StringContent httpRequestContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = client.PostAsync("/chat/completions", httpRequestContent).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseContent);

                // Sending the JSON string to a plain text file for further use

                string filePath = "response.txt";
                File.WriteAllText(filePath, responseContent);
                Console.WriteLine($"Response saved to: {Path.GetFullPath(filePath)}");

            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
        }

        internal static void ResponseDeserializationTest()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Response Deserialization Test");
            Console.WriteLine("------------------------------------------");

            // Recuperating JSON string response content from Test 1

            string filePath = "response.txt";
            string responseContent = File.ReadAllText(filePath);
            Console.WriteLine(responseContent);

            // Deseralizing JSON string

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };


            var response = JsonSerializer.Deserialize<Response>(responseContent, options);

            // Printing out some text content

            Console.WriteLine("Text response:");
            foreach (Choice choice in response.Choices)
            {
                Console.WriteLine(choice.Message.Content);
            }
        }

        internal static void TemplateCompletionTest()
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Template Completion Test (Uses DeepSeek API)");
            Console.WriteLine("---------------------------------------------");

            // Opening documents containing accounting chart and transactions

            string accountingChart = File.ReadAllText("Documents/planSIRE_compras.txt");
            string transactions = File.ReadAllText("Documents/20607224375-CS-CLOUD-202504-DeekSeekShortened.txt");

            // API key handling

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
            string apiKey = configuration["apisettings:deepseekapikey"];

            // Building orchestrator

            var orchestrator = new CompletionRequestOrchestrator
            {
                LlmModelName = "deepseek-chat",
                ApiKey = apiKey,
                AccountingChart = accountingChart,
                TemplateFormat = "tab-separated values",
                LoadedTemplates = transactions
            };

            orchestrator.BuildRequest();

            // Having a look at the built request and loaded data
            
            string body = orchestrator.HttpPostRequestContent.ReadAsStringAsync().Result;

            Console.WriteLine("Body of the HTTP request contains:");
            Console.WriteLine(body);

            // Executing request (WARNING: Call to DeepSeek API)

            string result = orchestrator.ExecuteRequest();

            Console.WriteLine("Request executed. Look for the generated text documents in bin.");

            string path = "completedTemplates.txt";
            File.WriteAllText(path, orchestrator.FilledTemplates);

            path = "completedTemplatesJson.txt";
            File.WriteAllText(path, orchestrator.Response.GetJson());
        }

        static void Main(string[] args)
        {
            // Test 1 (WARNING: Includes call to API)
            //RequestSerializationTest();

            // Test 2
            //ResponseDeserializationTest();

            // Test 3(WARNING: Includes call to API)
            // TemplateCompletionTest();

            // Test 4 Tokenization test (Integration with Python module)
            //TokenCalculator.TokenCalculator calculator = new TokenCalculator.TokenCalculator();

            //string path = "Documents/20607224375-CS-CLOUD-202504-DeekSeekShortened.txt";
            //string transactions = File.ReadAllText(path);

            //path = "Documents/planSIRE_compras.txt";
            //string accountingChart = File.ReadAllText(path);

            //int tokenCount = 0;

            //tokenCount+= calculator.calculateText(accountingChart);
            //tokenCount+= calculator.calculateText(transactions);

            //Console.WriteLine($"Number of tokens in prompt: { tokenCount }");
        }        
    }
}