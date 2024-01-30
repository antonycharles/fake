using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Core.DTO.Responses
{
    public class AppTokenResponse
    {
        public Guid? AppId { get; set; }
        public UserResponse User { get; set; }
        public string CallbackUrl { get; set; }
        public TokenResponse Token { get; set; }
    }
}