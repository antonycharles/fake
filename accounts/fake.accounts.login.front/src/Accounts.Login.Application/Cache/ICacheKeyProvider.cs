using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Login.Application.Cache
{
    public interface ICacheKeyProvider
    {
        string KeyAccounstsApiToken();
        string KeyLoginTokenAppUser(string hash);
    }
}