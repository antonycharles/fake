using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Login.Core.Models.Client
{
    public class ClientRequest
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}