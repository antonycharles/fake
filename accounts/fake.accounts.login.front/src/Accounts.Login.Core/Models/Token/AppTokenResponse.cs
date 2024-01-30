using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Core.Models.User;

namespace Accounts.Login.Core.Models.Token
{
    public class AppTokenResponse
    {
        public Guid? AppId { get; set; }
        public UserResponse User { get; set; }
        public string CallbackUrl { get; set; }
        public TokenResponse Token { get; set; }
    }
}