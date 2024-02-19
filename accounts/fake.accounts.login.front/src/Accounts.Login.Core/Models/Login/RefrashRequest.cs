using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Login.Core.Models.Login
{
    public class RefrashRequest
    {
        public Guid AppId { get; set;}
        public Guid UserId { get; set;}
    }
}