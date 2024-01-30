using System.ComponentModel.DataAnnotations;
using Accounts.Core.Enums;

namespace Accounts.Core.Entities
{
    public class App : BaseEntity<Guid>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set;}
        public string Description { get; set; }
        public string CallbackUrl { get; set; }
        public StatusEnum Status { get; set; }
    }
}