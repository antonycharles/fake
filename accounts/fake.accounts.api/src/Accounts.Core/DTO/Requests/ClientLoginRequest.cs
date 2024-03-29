using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Core.DTO.Requests
{
    public class ClientLoginRequest
    {
        [Required]
        public int ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }   
    }
}