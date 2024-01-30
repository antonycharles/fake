using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Core.Entities
{
    public class ClientProfile
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}