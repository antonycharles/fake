using System.Net;
using System.Text.Json;
using Accounts.Login.Core.Exceptions;
using Accounts.Login.Core.Models.Client;
using Accounts.Login.Core.Models.Token;
using Accounts.Login.Core.Repositories;
using Accounts.Login.Core.Settings;
using Accounts.Login.Infrastructure.Repositories.External;
using Microsoft.Extensions.Options;
using Refit;

namespace Accounts.Login.Infrastructure.Repositories
{
    public class ClientAuthorizationRepository : IClientAuthorizationRepository
    {
        private readonly IClientAuthorizationApiRepository _clientAuthorizationApiRepository;
        
        private readonly LoginSettings _apiSettings;

        public ClientAuthorizationRepository(
            IClientAuthorizationApiRepository clientAuthorizationApiRepository,
            ICacheRepository cacheRepository,
            IOptions<LoginSettings> apiSettings) 
        {
            _clientAuthorizationApiRepository = clientAuthorizationApiRepository ?? throw new ArgumentNullException(nameof(clientAuthorizationApiRepository));
            _apiSettings = apiSettings.Value;
        }

        public async Task<TokenResponse> GetTokenAsync()
        {
            try
            {
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