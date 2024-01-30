using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Core.DTO.Requests
{
    public class PaginationRequest
    {
        public int CurrentPage { get; set; } = 1;
        public string Search { get; set; } = "";
        public int Take { get; set; } = 10;
    }
}