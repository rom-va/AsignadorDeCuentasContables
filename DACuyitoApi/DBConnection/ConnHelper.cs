using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

using Microsoft.Extensions.Configuration;

namespace DACuyitoApi.DBConnection
{    
    public static class ConnHelper
    {
        private static string _jsonFilePath = "dbconnconfig.json";
        public static string? GetConnectionString()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(_jsonFilePath)
                .Build();

            if (config["authenticationtype"] == "windowsauthentication")
            {
                var parameters = config
                    .GetSection("windowsauthenticationparams")
                    .Get<WindowsConnParams>();
                string connString =
                    $"Server={parameters.Server};" +
                    $"Database={parameters.Database};" +
                    $"Encrypt={parameters.Encrypt};" +
                    $"Trusted_Connection={parameters.TrustedConnection};"
                    ;
                return connString;
            }
            if (config["authenticationtype"] == "sqlserverauthentication")
            {
                var parameters = config
                    .GetSection("sqlserverauthenticationparams")
                    .Get<SqlServerConnParams>();
                string connString =
                    $"Data Source={parameters.Server};" +
                    $"Initial Catalog={parameters.Database};" +
                    $"UID={parameters.Username};" +
                    $"Password={parameters.Password};" +
                    $"Min Pool Size={parameters.MinPoolSize};" +
                    $"Max Pool Size={parameters.MaxPoolSize};" +
                    $"Connect Timeout={parameters.ConnectTimeout};" +
                    $"Persist Security Info={parameters.PersistSecurityInfo};"
                    ;
                return connString;
            }

            return null;
        }
    }
}
