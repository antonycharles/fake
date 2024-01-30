using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Core.Models.Login;
using Accounts.Login.Core.Models.Register;
using Accounts.Login.Core.Models.Token;
using Refit;

namespace Accounts.Login.Infrastructure.Repositories.External
{
    public interface IUserAuthenticationApiRepository
    {
        [Post("/user-authentication/login")]
        Task<AppTokenResponse> AuthenticationAsync([Header("Authorization")] string bearerToken, LoginRequest request);
        [Post("/user-authentication/signup")]
        Task<AppTokenResponse> RegisterAsync([Header("Authorization")] string bearerToken, RegisterRequest request);
    }
}