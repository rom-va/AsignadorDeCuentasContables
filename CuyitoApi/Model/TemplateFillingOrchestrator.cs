using BLCuyitoApi;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuyitoApi.Model
{ 
    public class TemplateFillingOrchestrator
    {
        public static TemplateFillingResponse ExecuteFillingRequest(ITemplateFillingService service, TemplateFillingRequest request)
        {            
            HttpResponseMessage httpResponse = service.FillTemplates("deepseek-chat", request.AccountingChart, request.TemplateFormat, request.IncompleteTemplates);
            string filledTemplates = service.GetFilledTemplates();
            int tokensUsed = service.GetTokenUsage();

            return new TemplateFillingResponse {
                FilledTemplates = filledTemplates,
                TotalTokensUsed = tokensUsed,
                CompletionHttpStatusCode = (int) httpResponse.StatusCode
            };
        }

    }
}
