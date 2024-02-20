using System.Security.Claims;
using Accounts.Login.Application.Cache;
using Accounts.Login.Core.Handlers;
using Accounts.Login.Core.Handlers.Interfaces;
using Accounts.Login.Core.Models.Login;
using Accounts.Login.Core.Models.Register;
using Accounts.Login.Core.Models.Token;
using Accounts.Login.Core.Repositories;

namespace Accounts.Login.Application.Handlers
{
    public class LoginHandler : ILoginHandler
    {
        private readonly IUserAuthenticationRepository _userAuthenticationRepository;
        private readonly ILoginTokenHandler _loginTokenHandler;

        public LoginHandler(
            IUserAuthenticationRepository userAuthenticationRepository, 
            ILoginTokenHandler loginTokenHandler)
        {
            _userAuthenticationRepository = userAuthenticationRepository ?? throw new ArgumentNullException(nameof(userAuthenticationRepository));
            _loginTokenHandler = loginTokenHandler ?? throw new ArgumentNullException(nameof(loginTokenHandler));
        }

        public async Task<AppTokenResponse> AuthenticationAsync(LoginRequest request)
        {
            var authetication = await _userAuthenticationRepository.AuthenticationAsync(request);
            authetication.AccessToken = await _loginTokenHandler.SaveTokenAsync(authetication);
            return authetication;  
        }

        public async Task<AppTokenResponse> RefrashAsync(Guid appId, Guid userId)
        {
            RefrashRequest request = GetRefrashRequest(appId, userId);

            var authetication = await _userAuthenticationRepository.RefrashAsync(request);
            authetication.AccessToken = await _loginTokenHandler.SaveTokenAsync(authetication);
            return authetication;
        }

        public async Task<AppTokenResponse> RegisterAsync(RegisterRequest request)
        {
            var authetication = await _userAuthenticationRepository.RegisterAsync(request);
            authetication.AccessToken = await _loginTokenHandler.SaveTokenAsync(authetication);
            return authetication;
        }

        private RefrashRequest GetRefrashRequest(Guid appId, Guid userId)
        {
            return new RefrashRequest()
            {
                AppId = appId,
                UserId = userId
            };
        }
    }
}