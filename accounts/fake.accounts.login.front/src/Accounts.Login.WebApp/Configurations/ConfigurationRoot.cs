using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Core.Settings;

namespace Accounts.Login.WebApp.Configurations
{
    public static class ConfigurationRoot
    {
        private const string pathSettingsRelease = "secrets/appsettings.json";
        private const string pathSettingsDebug = "appsettings.json";

        public static void AddConfigurationRoot(this WebApplicationBuilder builder)
        {
            #if RELEASE
                builder.Configuration.AddJsonFile(pathSettingsRelease, false, true);
            #else
                builder.Configuration.AddJsonFile(pathSettingsDebug, false, true);
            #endif

            builder.Configuration.AddEnvironmentVariables();

            builder.Services.AddOptions<LoginSettings>()
                .BindConfiguration(nameof(LoginSettings))
                .ValidateDataAnnotations()
                .ValidateOnStart();
        }

        public static LoginSettings GetSettings(this WebApplicationBuilder builder)
        {
            return builder.Configuration
                .GetSection(nameof(LoginSettings))
                .Get<LoginSettings>();
        }
    }
}