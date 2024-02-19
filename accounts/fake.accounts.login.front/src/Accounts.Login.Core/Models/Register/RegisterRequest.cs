using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Login.Core.Models.Register
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name{ get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        [Required(ErrorMessage="AppId not informed")]
        public Guid? AppId { get; set;}
    }
}