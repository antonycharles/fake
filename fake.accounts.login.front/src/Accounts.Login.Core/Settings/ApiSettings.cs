using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Login.Core.Settings
{
    public class ApiSettings
    {
        [Required]
        public string AccountsClientId { get; set; }
        [Required]
        public string AccountsClientSecret { get; set; }
        [Required]
        public string FakeAccountsApiURL { get; set; }
        public string RedisURL { get; set; }
    }
}