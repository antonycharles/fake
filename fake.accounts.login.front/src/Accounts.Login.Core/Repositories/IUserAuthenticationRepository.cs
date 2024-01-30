using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Core.Models.Login;
using Accounts.Login.Core.Models.Register;
using Accounts.Login.Core.Models.Token;

namespace Accounts.Login.Core.Repositories
{
    public interface IUserAuthenticationRepository
    {
        Task<AppTokenResponse> AuthenticationAsync(LoginRequest request);
        Task<AppTokenResponse> RegisterAsync(RegisterRequest request);
    }
}