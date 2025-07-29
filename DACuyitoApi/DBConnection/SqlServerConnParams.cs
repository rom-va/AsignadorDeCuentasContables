using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DACuyitoApi.DBConnection
{
    internal class SqlServerConnParams
    {
        [JsonPropertyName("server")]
        public string Server { get; set; }
        [JsonPropertyName("database")]
        public string Database { get; set; }
        [JsonPropertyName("userid")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("minpoolsize")]
        public string MinPoolSize { get; set; }
        [JsonPropertyName("maxpoolsize")]
        public string MaxPoolSize { get; set; }       
        [JsonPropertyName("connecttimeout")]
        public string ConnectTimeout { get; set; }
        [JsonPropertyName("persistsecurityinfo")]
        public bool PersistSecurityInfo { get; set; }
    }

}
