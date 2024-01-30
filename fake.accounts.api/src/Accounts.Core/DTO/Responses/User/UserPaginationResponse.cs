using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Core.DTO.Responses.User
{
    public class UserPaginationResponse
    {
        public IList<UserResponse> Data { get; set; }
        public PaginationResponse Pagination { get; set; }
    }
}