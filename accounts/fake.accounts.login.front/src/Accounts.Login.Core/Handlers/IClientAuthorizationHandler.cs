using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Login.Core.Handlers
{
    public interface IClientAuthorizationHandler
    {
        Task<string> GetTokenAsync();
    }
}