using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Core.Configurations
{
    public class AccountsSettings
    {
        [Required]
        public string DatabaseConnection { get; set; }
        [Required]
        public string RedisConnection { get; set;}
    }
}