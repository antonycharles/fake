using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Enums;

namespace Accounts.Core.Entities
{
    public class Profile : BaseEntity<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public StatusEnum Status { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        [Required]
        public Guid AppId { get; set; }
        public virtual App App { get; set; }
        public ICollection<ProfileRole> ProfilesRoles { get; set; }
        public ICollection<UserProfile> ProfilesUsers { get; set;}
        public ICollection<ClientProfile> ProfilesClients { get; set;}
    }
}