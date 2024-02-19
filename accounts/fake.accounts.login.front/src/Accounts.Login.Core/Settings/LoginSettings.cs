using System.ComponentModel.DataAnnotations;

namespace Accounts.Login.Core.Settings
{
    public class LoginSettings
    {
        [Required]
        public string AccountsClientId { get; set; }
        [Required]
        public string AccountsClientSecret { get; set; }
        [Required]
        public string FakeAccountsApiURL { get; set; }
        [Required]
        public string RedisURL { get; set; }
        [Required]
        public string RedisInstanceName { get; set; }
    }
}