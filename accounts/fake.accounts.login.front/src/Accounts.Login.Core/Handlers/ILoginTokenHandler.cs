using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Core.Models.Token;

namespace Accounts.Login.Core.Handlers
{
    public interface ILoginTokenHandler
    {
        Task<string> SaveTokenAsync(AppTokenResponse appToken);
        Task<TokenResponse> GetTokenAsync(string accessToken);
    }
}