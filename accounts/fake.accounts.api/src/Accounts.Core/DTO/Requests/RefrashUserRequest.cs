using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Core.DTO.Requests
{
    public class RefrashUserRequest
    {
        [Required]
        public Guid? AppId { get; set;}
        [Required]
        public Guid? UserId { get; set;}
    }
}