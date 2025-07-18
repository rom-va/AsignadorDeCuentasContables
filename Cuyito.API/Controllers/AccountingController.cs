using AsignadorDeCuentasContables.DeepSeekAPI;
using Microsoft.AspNetCore.Mvc;

namespace Cuyito.API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class AccountingController : ControllerBase
    {
        [HttpPost("completion")]
        public IActionResult Completion([FromBody] CompletionInput input)
        {
            var orchestrator = new CompletionRequestOrchestrator
            {
                LlmModelName = input.LlmModelName,
                ApiKey = input.ApiKey,
                AccountingChart = input.AccountingChart,
                TemplateFormat = input.TemplateFormat,
                LoadedTemplates = input.Transactions
            };
            orchestrator.BuildRequest();
            //string result = orchestrator.ExecuteRequest();

            //var response = new CompletionOutput
            //{
            //    Content = result,
            //    TotalTokensUsed = orchestrator.Response.Usage.TotalTokens
            //};

            var response = new CompletionOutput
            {
                Content = "This is the content." +
                $"{orchestrator.LlmModelName} \n" +
                $"{orchestrator.ApiKey} \n" +
                $"{orchestrator.AccountingChart} \n" +
                $"{orchestrator.TemplateFormat} \n" +
                $"{orchestrator.LoadedTemplates}  \n",
                TotalTokensUsed = 11111
            };
            
            return Ok(response);
        }
    }
}
