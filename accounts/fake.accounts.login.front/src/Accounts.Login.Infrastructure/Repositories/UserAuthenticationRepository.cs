using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Accounts.Login.Core.Exceptions;
using Accounts.Login.Core.Models.Errors;
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
        private readonly IUserAuthenticationApiRepository _userAuthenticationApiRepository;

        public UserAuthenticationRepository(
            IUserAuthenticationApiRepository userAuthenticationApiRepository)
        {
            _userAuthenticationApiRepository = userAuthenticationApiRepository ?? throw new ArgumentNullException(nameof(userAuthenticationApiRepository));
        }

        public async Task<AppTokenResponse> AuthenticationAsync(LoginRequest request)
        {
            try
            {
                return await _userAuthenticationApiRepository.AuthenticationAsync(request);
            }
            catch(ApiException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
            {
                CustomProblemDetails? problemDetails  = JsonSerializer.Deserialize<CustomProblemDetails>(ex.Content);
                throw new ExternalApiException(problemDetails?.Detail);
            }
            catch(Exception ex)
            {
                throw new ExternalApiException(ex.Message);
            }
        }

        public async Task<AppTokenResponse> RefrashAsync(RefrashRequest request)
        {
            try
            {
                return await _userAuthenticationApiRepository.RefrashAsync(request);
            }
            catch(ApiException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
            {
                CustomProblemDetails? problemDetails  = JsonSerializer.Deserialize<CustomProblemDetails>(ex.Content);
                throw new ExternalApiException(problemDetails?.Detail);
            }
            catch(Exception ex)
            {
                throw new ExternalApiException(ex.Message);
            }
        }

        public async Task<AppTokenResponse> RegisterAsync(RegisterRequest request)
        {
            try
            {
                return await _userAuthenticationApiRepository.RegisterAsync(request);
            }
            catch(ApiException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
            {
                CustomProblemDetails? problemDetails  = JsonSerializer.Deserialize<CustomProblemDetails>(ex.Content);
                throw new ExternalApiException(problemDetails?.Detail);
            }
            catch(Exception ex)
            {
                throw new ExternalApiException(ex.Message);
            }
        }

    }
}