using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Accounts.Login.Core.Exceptions;
using Accounts.Login.Core.Models.Client;
using Accounts.Login.Core.Models.Token;
using Accounts.Login.Core.Repositories;
using Accounts.Login.Core.Settings;
using Accounts.Login.Infrastructure.Repositories.External;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Refit;

namespace Accounts.Login.Infrastructure.Repositories
{
    public class ClientAuthorizationRepository : IClientAuthorizationRepository
    {
        private readonly IClientAuthorizationApiRepository _clientAuthorizationApiRepository;
        private readonly IDistributedCache _cache;  
        
        private readonly LoginSettings _apiSettings;
        private readonly string _keyCache = "accountslogin:accountsapi:token";

        public ClientAuthorizationRepository(
            IClientAuthorizationApiRepository clientAuthorizationApiRepository,
            IDistributedCache cache,
            IOptions<LoginSettings> apiSettings) 
        {
            _clientAuthorizationApiRepository = clientAuthorizationApiRepository;
            _cache = cache;
            _apiSettings = apiSettings.Value;
        }

        public async Task<string> GetToken()
        {
            return await GetTokenCacheAsync();
        }

        private async Task<string> GetTokenCacheAsync()
        {
            var token = await _cache.GetStringAsync(_keyCache);

            if(token != null)
                return token;
            
            var tokenResponse = await GetTokenApiAsync();

            var expiretion = tokenResponse.ExpiresIn.Value.AddMinutes(-10) - DateTime.Now;

            await _cache.SetStringAsync(_keyCache, tokenResponse.Token, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiretion
            });

            return tokenResponse.Token;
        }

        private async Task<TokenResponse> GetTokenApiAsync()
        {
            try{
                var token = await _clientAuthorizationApiRepository.AuthenticationAsync(new ClientRequest{
                    ClientId = _apiSettings.AccountsClientId,
                    ClientSecret = _apiSettings.AccountsClientSecret
                });

                return token;
            }
            catch(ApiException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
            {
                ProblemDetails? problemDetails  = JsonSerializer.Deserialize<ProblemDetails>(ex.Content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                throw new ExternalApiException(problemDetails?.Detail);
            }
            catch(Exception ex)
            {
                throw new ExternalApiException(ex.Message);
            }
        }
    }
}