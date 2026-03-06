using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using DotNetEnv;

namespace Test
{
    public class TesteApiFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var directory = Directory.GetCurrentDirectory();
            while (directory != null && !File.Exists(Path.Combine(directory, ".env.test")))
            {
                directory = Directory.GetParent(directory)?.FullName;
            }

            if (directory == null)
                throw new FileNotFoundException("Arquivo .env.test não encontrado em nenhum diretório pai.");

            var envPath = Path.Combine(directory, ".env.test");

            foreach (var line in File.ReadAllLines(envPath))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue;

                var parts = line.Split('=', 2);
                if (parts.Length == 2)
                {
                    var key = parts[0].Trim();
                    var value = parts[1].Trim().Trim('"');
                    Environment.SetEnvironmentVariable(key, value);
                }
            }

            builder.ConfigureAppConfiguration((context, config) =>
            {
                var connectionString =
                Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__DEFAULTCONNECTION");

                if (string.IsNullOrEmpty(connectionString))
                    throw new InvalidOperationException("CONNECTIONSTRINGS__DEFAULTCONNECTION não foi carregada do .env.test");


                var settings = new Dictionary<string, string?>
                {
                    ["ConnectionStrings:DefaultConnection"] = connectionString

                };

                config.AddInMemoryCollection(settings);
            });
        }
    }

}
