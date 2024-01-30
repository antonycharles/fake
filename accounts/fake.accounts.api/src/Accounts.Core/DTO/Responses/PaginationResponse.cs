using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Core.DTO.Responses
{
    public class PaginationResponse
    {

        public string Search { get; set; }
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int? NextPage { get; set; }
        public int? PrevPage { get; set; }
    }
}