using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Core.Models.Client;
using Accounts.Login.Core.Models.Token;
using Refit;

namespace Accounts.Login.Infrastructure.Repositories.External
{
    public interface IClientAuthorizationApiRepository
    {
        [Post("/client/authorization")]
        Task<TokenResponse> AuthenticationAsync([Body]ClientRequest request);
    }
}