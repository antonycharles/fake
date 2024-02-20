using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Core.Models.Token;

namespace Accounts.Login.Core.Repositories
{
    public interface IClientAuthorizationRepository
    {
        Task<TokenResponse> GetTokenAsync();
    } 
}