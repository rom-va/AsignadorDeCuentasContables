using BLCuyitoApi;
using BLCuyitoApi.DeepSeekAPI;
using CuyitoApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace CuyitoApi.Controllers
{
    [ApiController]
    [Route("accounting")]
    public class AccountingController : ControllerBase
    {
        [HttpPost]
        public IActionResult FillAccounts([FromBody] TemplateFillingRequest input)
        {
            if (input.HasAnyNullAttributes()) return BadRequest();

            // Verify user and credits [TO-DO]

            var result = TemplateFillingOrchestrator.ExecuteFillingRequest(new DSTemplateFillingService(), input);
            
            if (result.CompletionHttpStatusCode < 200 || result.CompletionHttpStatusCode >=300)
            {
                return StatusCode(result.CompletionHttpStatusCode);
            }            

            return Ok(result);
        }
    }
}
