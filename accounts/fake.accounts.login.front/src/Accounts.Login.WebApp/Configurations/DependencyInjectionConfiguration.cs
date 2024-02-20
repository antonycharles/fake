using Accounts.Login.Core.Handlers.Interfaces;
using Accounts.Login.Core.Repositories;
using Accounts.Login.Application.Handlers;
using Refit;
using Accounts.Login.Infrastructure.Repositories.External;
using Accounts.Login.Infrastructure.Repositories;
using Accounts.Login.Core.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Accounts.Login.Core.Handlers;
using Accounts.Login.Application.Cache;

namespace Accounts.Login.WebApp.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this WebApplicationBuilder builder, LoginSettings settings)
        {

            builder.Services.AddAuthentication();
            builder.AddRedisDependencyInjection(settings);
            builder.Services.AddRepositoryDependencyInjection();
            builder.Services.AddHandlerDependencyInjection();
            builder.AddRepositoryExternalDependencyInjection(settings);
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
            services.AddSingleton<ICacheKeyProvider,CacheKeyProvider>();

            services.AddSingleton<ILoginHandler,LoginHandler>();
            services.AddSingleton<ILoginTokenHandler,LoginTokenHandler>();
            services.AddSingleton<IClientAuthorizationHandler,ClientAuthorizationHandler>();
        }

        private static void AddRepositoryDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<IUserAuthenticationRepository,UserAuthenticationRepository>();
            services.AddSingleton<IClientAuthorizationRepository, ClientAuthorizationRepository>();
            services.AddSingleton<ICacheRepository,CacheRepository>();
        }

        private static void AddRepositoryExternalDependencyInjection(this WebApplicationBuilder builder, LoginSettings settings)
        {
            builder.Services.AddSingleton<ClientAuthorizationDelegatingHandler>();

            builder.Services.AddRefitClient<IUserAuthenticationApiRepository>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new  Uri(settings.FakeAccountsApiURL);
            })
            .AddHttpMessageHandler<ClientAuthorizationDelegatingHandler>();
            
            builder.Services.AddRefitClient<IClientAuthorizationApiRepository>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new  Uri(settings.FakeAccountsApiURL);
            });
        }

        private static void AddRedisDependencyInjection(this WebApplicationBuilder builder, LoginSettings settings)
        {
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = settings.RedisURL;
            });
        }
    }
}