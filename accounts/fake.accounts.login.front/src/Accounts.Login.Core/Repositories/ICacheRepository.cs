using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Login.Core.Repositories
{
    public interface ICacheRepository
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, DateTime? absoluteExpiration);
        Task RemoveAsync(string key);
    }
}