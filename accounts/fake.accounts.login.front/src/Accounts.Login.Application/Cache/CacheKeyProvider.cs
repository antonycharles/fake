using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Login.Application.Cache
{
    public class CacheKeyProvider : ICacheKeyProvider
    {
        private string PREFIX => $"accountslogin";

        public string KeyAccounstsApiToken()
            => $"{PREFIX}:accountsapi:token";

        public string KeyLoginTokenAppUser(string hash)
            => $"{PREFIX}:logintoken:{hash}";
    }
}