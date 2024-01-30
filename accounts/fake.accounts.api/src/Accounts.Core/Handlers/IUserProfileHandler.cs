using Accounts.Core.DTO.Requests;
using Accounts.Core.DTO.Responses;

namespace Accounts.Core.Handlers
{
    public interface IUserProfileHandler
    {
        Task<UserProfileResponse> GetOrCreateAsync(UserProfileRequest userProfileRequest);
    }
}