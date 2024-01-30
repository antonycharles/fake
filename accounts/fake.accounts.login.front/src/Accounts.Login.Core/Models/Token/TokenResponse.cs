using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Login.Core.Models.Token
{
    public class TokenResponse
    {
        public DateTime? ExpiresIn { get; set; }
        public string Token { get; set; }
    }
}