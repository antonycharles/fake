using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Application.Cache;
using Accounts.Login.Core.Handlers;
using Accounts.Login.Core.Models.Token;
using Accounts.Login.Core.Repositories;

namespace Accounts.Login.Application.Handlers
{
    public class ClientAuthorizationHandler : IClientAuthorizationHandler
    {
        private readonly ICacheRepository _cacheRepository;
        private readonly IClientAuthorizationRepository _clientAuthorizationRepository;
        private readonly ICacheKeyProvider _cacheKeyProvider;

        public ClientAuthorizationHandler(
            ICacheRepository cacheRepository,
            IClientAuthorizationRepository clientAuthorizationRepository,
            ICacheKeyProvider cacheKeyProvider)
        {
            _cacheRepository = cacheRepository ?? throw new ArgumentNullException(nameof(cacheRepository));
            _clientAuthorizationRepository = clientAuthorizationRepository ?? throw new ArgumentNullException(nameof(clientAuthorizationRepository));
            _cacheKeyProvider = cacheKeyProvider ?? throw new ArgumentNullException(nameof(cacheKeyProvider));
        }

        public async Task<string> GetTokenAsync()
        {
            var token = await _cacheRepository.GetAsync<TokenResponse>(
                _cacheKeyProvider.KeyAccounstsApiToken()
            );

            if(token != null)
                return token.Token;
            
            var tokenResponse = await _clientAuthorizationRepository.GetTokenAsync();

            await _cacheRepository.SetAsync<TokenResponse>(
                _cacheKeyProvider.KeyAccounstsApiToken(), 
                tokenResponse, 
                tokenResponse.ExpiresIn);

            return tokenResponse.Token;
        }
    }
}