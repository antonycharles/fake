using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Configurations;

namespace Accounts.API.Configurations
{
    public static class ConfigurationRoot
    {
        public static void AddConfigurationRoot(this WebApplicationBuilder builder)
        {
            #if RELEASE
                builder.Configuration.AddJsonFile("secrets/appsettings.json", false, true);
            #else
                builder.Configuration.AddJsonFile("appsettings.json", false, true);
            #endif

            builder.Configuration.AddEnvironmentVariables();

            builder.Services
                .AddOptions<AccountsSettings>()
                .BindConfiguration(nameof(AccountsSettings))
                .ValidateDataAnnotations()
                .ValidateOnStart(); 
        }

        public static AccountsSettings GetSettings(this WebApplicationBuilder builder)
        {
            return builder.Configuration
                .GetSection(nameof(AccountsSettings))
                .Get<AccountsSettings>();
        }
    }
}