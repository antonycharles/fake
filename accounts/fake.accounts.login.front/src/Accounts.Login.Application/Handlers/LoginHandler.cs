using System.Security.Claims;
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

        public LoginHandler(IUserAuthenticationRepository userAuthenticationRepository)
        {
            _userAuthenticationRepository = userAuthenticationRepository;
        }

        public async Task<AppTokenResponse> AuthenticationAsync(LoginRequest request)
        {
            var authetication = await _userAuthenticationRepository.AuthenticationAsync(request);
            return authetication;  
        }

        public async Task<AppTokenResponse> RefrashAsync(Guid appId, Guid userId)
        {
            var request = new RefrashRequest()
            {
                AppId = appId,
                UserId = userId
            };

            var authetication = await _userAuthenticationRepository.RefrashAsync(request);
            return authetication;  
        }

        public async Task<AppTokenResponse> RegisterAsync(RegisterRequest request)
        {
            var authetication = await _userAuthenticationRepository.RegisterAsync(request);
            return authetication;
        }
    }
}