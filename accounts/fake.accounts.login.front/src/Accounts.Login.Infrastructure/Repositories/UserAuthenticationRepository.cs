using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Accounts.Login.Core.Exceptions;
using Accounts.Login.Core.Models.Login;
using Accounts.Login.Core.Models.Register;
using Accounts.Login.Core.Models.Token;
using Accounts.Login.Core.Repositories;
using Accounts.Login.Infrastructure.Repositories.External;
using Refit;

namespace Accounts.Login.Infrastructure.Repositories
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        private readonly IClientAuthorizationRepository _clientAuthorizationRepository;
        private readonly IUserAuthenticationApiRepository _userAuthenticationApiRepository;

        public UserAuthenticationRepository(
            IClientAuthorizationRepository clientAuthorizationRepository, 
            IUserAuthenticationApiRepository userAuthenticationApiRepository)
        {
            _clientAuthorizationRepository = clientAuthorizationRepository;
            _userAuthenticationApiRepository = userAuthenticationApiRepository;
        }

        public async Task<AppTokenResponse> AuthenticationAsync(LoginRequest request)
        {
            var token = await _clientAuthorizationRepository.GetToken();

            try{
                return await _userAuthenticationApiRepository.AuthenticationAsync($"Bearer {token}", request);
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

        public async Task<AppTokenResponse> RegisterAsync(RegisterRequest request)
        {
            var token = await _clientAuthorizationRepository.GetToken();

            try{
                return await _userAuthenticationApiRepository.RegisterAsync($"Bearer {token}", request);
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