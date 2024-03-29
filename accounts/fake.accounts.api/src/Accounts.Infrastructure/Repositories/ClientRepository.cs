using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Accounts.Core.Repositories;
using Accounts.Infrastructure.Data;
using Accounts.Infrastructure.Repositories.Base;

namespace Accounts.Infrastructure.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(AccountsContext dbContext) : base(dbContext)
        {

        }
    }
}