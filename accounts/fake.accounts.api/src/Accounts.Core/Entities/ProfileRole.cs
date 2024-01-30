using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Core.Entities
{
    public class ProfileRole
    {
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}