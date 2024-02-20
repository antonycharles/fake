using System.Security.Authentication;
using Accounts.Login.Application.Cache;
using Accounts.Login.Application.Extentions;
using Accounts.Login.Core.Handlers;
using Accounts.Login.Core.Models.Token;
using Accounts.Login.Core.Repositories;

namespace Accounts.Login.Application.Handlers
{
    public class LoginTokenHandler : ILoginTokenHandler
    {
        private readonly ICacheRepository _cacheRepository;
        private readonly ICacheKeyProvider _cacheKeyProvider;

        public LoginTokenHandler(ICacheRepository cacheRepository, ICacheKeyProvider cacheKeyProvider)
        {
            _cacheRepository = cacheRepository ?? throw new ArgumentNullException(nameof(cacheRepository));
            _cacheKeyProvider = cacheKeyProvider ?? throw new ArgumentNullException(nameof(cacheKeyProvider));
        }


        public async Task<TokenResponse> GetTokenAsync(string accessToken)
        {
            var key = _cacheKeyProvider.KeyLoginTokenAppUser(accessToken);
            var token = await _cacheRepository.GetAsync<TokenResponse>(key);

            if(token is null)
                throw new AuthenticationException("User not authorized");

            await _cacheRepository.RemoveAsync(key);

            return token;
        }

        public async Task<string> SaveTokenAsync(AppTokenResponse appToken)
        {
            var keyHash = $"{appToken.AppId}-{appToken.User.Id}".EncodeToBase64();
            var key = _cacheKeyProvider.KeyLoginTokenAppUser(keyHash);
            await _cacheRepository.SetAsync<TokenResponse>(key,appToken.Token,DateTime.Now.AddMinutes(2));

            return keyHash;
        }
    }
}