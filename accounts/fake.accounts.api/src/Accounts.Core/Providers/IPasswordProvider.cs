using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Core.Providers
{
    public interface IPasswordProvider
    {
        string GenerateSalt();
        string HashPassword(string password, string salt);
    }
}