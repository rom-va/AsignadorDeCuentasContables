using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DACuyitoApi.DBConnection
{
    internal class WindowsConnParams
    {
        [JsonPropertyName("server")]
        public string Server { get; set; }
        [JsonPropertyName("database")]
        public string Database { get; set; }
        [JsonPropertyName("encrypt")]
        public bool Encrypt { get; set; }
        [JsonPropertyName("trustedconnection")]
        public bool TrustedConnection { get; set; }
    }
}
