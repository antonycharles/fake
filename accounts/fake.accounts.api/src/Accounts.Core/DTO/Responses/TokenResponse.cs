using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Core.DTO.Responses
{
    public class TokenResponse
    {
        public DateTime? ExpiresIn { get; set; }
        public string Token { get; set; }
    }
}