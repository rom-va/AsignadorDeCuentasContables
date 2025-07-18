using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Python.Runtime;

namespace Tests.TokenCalculator
{
    public class TokenCalculator
    {
        private string PythonModule = "Tokenizer";
        
        public int calculateText(string text)
        {
            // Tokenizer.py, tokenizer.json and tokenizer_configuration.json must be on the "bin/Debug/net8.0" directory
            // Be sure that the location of the python.dll is accurate

            int total_tokens;
            Runtime.PythonDLL = "C:\\Users\\roth5\\AppData\\Local\\Programs\\Python\\Python313\\python313.dll";
            PythonEngine.Initialize();

            using (Py.GIL()) // Global Interpreter Lock
            {
                dynamic module = Py.Import(PythonModule);       // Import python module
                dynamic result = module.calculate_text(text);   // Use methods
                total_tokens = result;
            }

            AppContext.SetSwitch("System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization", true);
            PythonEngine.Shutdown();
            AppContext.SetSwitch("System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization", false);

            return total_tokens;
        }
        

    }
}
