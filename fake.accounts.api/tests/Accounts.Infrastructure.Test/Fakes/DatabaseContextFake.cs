using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Test.Fakes
{
    public class DatabaseContextFake
    {
        public static AccountsContext Create()
        {
            var options = new DbContextOptionsBuilder<AccountsContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new AccountsContext(options);
            databaseContext.Database.EnsureCreated();

            return databaseContext;
        }
    }
}