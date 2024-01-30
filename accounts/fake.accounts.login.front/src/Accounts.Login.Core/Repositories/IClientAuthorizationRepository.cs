using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Login.Core.Repositories
{
    public interface IClientAuthorizationRepository
    {
        Task<string> GetToken();
    } 
}