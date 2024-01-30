using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.DTO.Requests;
using Accounts.Core.DTO.Responses.User;
using Accounts.Core.Entities;

namespace Accounts.Core.Builders
{
    public interface IUserPaginationResponseBuilder
    {
        UserPaginationResponse Build(PaginationRequest paginationRequest, IEnumerable<User> users, int totalRecords);
    }
}