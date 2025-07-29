using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLCuyitoApi
{
    public interface ITemplateFillingService
    {
        public HttpResponseMessage FillTemplates(
            string llmModel,            
            string accountingChart,
            string templateFormat,
            string loadedTemplates
            );
        public string GetFilledTemplates();
        public int GetTokenUsage();
    }
}
