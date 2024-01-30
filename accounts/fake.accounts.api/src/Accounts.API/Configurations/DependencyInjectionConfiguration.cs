using Accounts.Application.Builders;
using Accounts.Application.Handlers;
using Accounts.Application.Providers;
using Accounts.Core.Builders;
using Accounts.Core.Handlers;
using Accounts.Core.Providers;
using Accounts.Core.Repositories;
using Accounts.Core.Repositories.Base;
using Accounts.Infrastructure.Repositories;
using Accounts.Infrastructure.Repositories.Base;

namespace Accounts.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddProviderDependencyInjection();
            
            services.AddRepositoryDependencyInjection();
            
            services.AddBuilderDependencyInjection();

            services.AddHandlerDependencyInjection();
        }


        private static void AddBuilderDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserPaginationResponseBuilder,UserPaginationResponseBuilder>();
        }

        private static void AddHandlerDependencyInjection(this IServiceCollection services)
        {
            //GERA-COMMANDS-ADD-HANDLER
            services.AddScoped<IClientAuthorizationHandler,ClientAuthorizationHandler>();
            services.AddSingleton<ITokenKeyHandler,TokenKeyHandler>();
            services.AddScoped<ITokenHandler,TokenHandler>();
            services.AddScoped<IUserAuthorizationHandler,UserAuthorizationHandler>();
            services.AddScoped<IUserHandler,UserHandler>();
            services.AddScoped<IUserProfileHandler, UserProfileHandler>();
           // services.AddScoped<AppHandler>();
           // services.AddScoped<RoleHandler>();
        }


        private static void AddRepositoryDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAppRepository,AppRepository>();
            services.AddScoped<IProfileRepository,ProfileRepository>();
            services.AddScoped<IRoleRepository,RoleRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
        }

        private static void AddProviderDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IPasswordProvider, PasswordProvider>();
        }
    }
}