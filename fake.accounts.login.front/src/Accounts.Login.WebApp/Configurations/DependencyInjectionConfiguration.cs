using Accounts.Login.Core.Handlers.Interfaces;
using Accounts.Login.Core.Repositories;
using Accounts.Login.Application.Handlers;
using Refit;
using Accounts.Login.Infrastructure.Repositories.External;
using Accounts.Login.Infrastructure.Repositories;
using Accounts.Login.Core.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Accounts.Login.WebApp.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddOptions<ApiSettings>()
            .BindConfiguration("ApiSettings")
            .ValidateDataAnnotations()
            .ValidateOnStart();
            
            builder.Services.AddAuthentication();
            builder.AddRedisDependencyInjection();
            builder.AddRepositoryExternalDependencyInjection();
            builder.Services.AddRepositoryDependencyInjection();
            builder.Services.AddHandlerDependencyInjection();
        }

        private static void AddAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
                {
                    config.Cookie.Name = "FakeAccountsLoginCookie";
                    config.LoginPath = "/login";
                    config.AccessDeniedPath = "/";
                });
        }

        private static void AddHandlerDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<ILoginHandler,LoginHandler>();
        }

        private static void AddRepositoryDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<IUserAuthenticationRepository,UserAuthenticationRepository>();
            services.AddSingleton<IClientAuthorizationRepository, ClientAuthorizationRepository>();
        }

        private static void AddRepositoryExternalDependencyInjection(this WebApplicationBuilder builder)
        {
            var fakeAccountsApi = builder.Configuration.GetValue<string>("ApiSettings:FakeAccountsApiURL");

            builder.Services.AddRefitClient<IUserAuthenticationApiRepository>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new  Uri(fakeAccountsApi);
            });
            
            builder.Services.AddRefitClient<IClientAuthorizationApiRepository>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new  Uri(fakeAccountsApi);
            });
        }

        private static void AddRedisDependencyInjection(this WebApplicationBuilder builder)
        {
            var redisURL = builder.Configuration.GetValue<string>("ApiSettings:RedisURL");
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisURL;
            });
        }
    }
}