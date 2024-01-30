using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Accounts.Core.Repositories.Base;

namespace Accounts.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email);

        Task<IEnumerable<User>> GetPaginationAsync(string search,int position, int take);
        Task<int> CountPaginationAsync(string search);
    }
}