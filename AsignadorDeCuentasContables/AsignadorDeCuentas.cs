namespace AsignadorDeCuentasContables
{
    public class AsignadorDeCuentas
    {
        private string LlmName;
        private string ApiKey;
        private string ApiPassword;
        private string PlanContable;
        private string TemplateFormat;
        private string LoadedTemplates;
        private string FilledTemplates;

        public AsignadorDeCuentas(
            string llmName,
            string apiKey,
            string apiPassword,
            string planContable,
            string templateFormat,
            string loadedTemplates)
        {
            LlmName = llmName;
            ApiKey = apiKey;
            ApiPassword = apiPassword;
            PlanContable = planContable;
            TemplateFormat = templateFormat;
            LoadedTemplates = loadedTemplates;
        }

        public void CargarAsignador(String loadedTemplates)
        {
            LoadedTemplates = loadedTemplates;
        }

        public void AsignarCuentas()
        {
            // 1. Make prompt
            // 2. Send prompt
            // 3. Return prompt
        }

    }
}
