using Accounts.Core.DTO.Requests;
using Accounts.Core.DTO.Responses;
using Accounts.Core.DTO.Responses.User;

namespace Accounts.Core.Handlers
{
    public interface IUserHandler
    {
        Task<UserPaginationResponse> GetPaginationAsync(PaginationRequest paginationRequest);
        Task<UserResponse> CreateAsync(UserRequest authorizationRequest);
        Task<UserResponse> GetOrCreateByEmailAsync(UserRequest authorizationRequest);
    }
}