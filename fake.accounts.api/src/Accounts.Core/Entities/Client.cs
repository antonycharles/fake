using System.ComponentModel.DataAnnotations;
using Accounts.Core.Enums;

namespace Accounts.Core.Entities
{
    public class Client : BaseEntity<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string SecretHash { get; set; }
        [Required]
        public string Salt { get; set; }
        public StatusEnum Status { get; set; }
        public ICollection<ClientProfile> ClientsProfiles { get; set; }
    }
}