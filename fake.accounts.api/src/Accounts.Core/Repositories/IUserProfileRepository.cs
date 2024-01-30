using Accounts.Core.Entities;
using Accounts.Core.Repositories.Base;

namespace Accounts.Core.Repositories
{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        Task<UserProfile> GetFirstByUserIdAndProfileId(Guid userId, int? profileId);
        Task<UserProfile> GetFirstByUserIdAndProfilesIds(Guid userId, IEnumerable<int> profiles);
    }
}