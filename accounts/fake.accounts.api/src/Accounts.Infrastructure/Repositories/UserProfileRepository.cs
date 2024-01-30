using Accounts.Core.Entities;
using Accounts.Core.Repositories;
using Accounts.Infrastructure.Data;
using Accounts.Infrastructure.Repositories.Base;

namespace Accounts.Infrastructure.Repositories
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(AccountsContext dbContext) : base(dbContext)
        {

        }

        public async Task<UserProfile> GetFirstByUserIdAndProfileId(Guid userId, int? profileId)
        {
            return await base.GetFirstAsync(w =>
                            w.ProfileId == profileId &&
                            w.UserId == userId);
        }

        public async Task<UserProfile> GetFirstByUserIdAndProfilesIds(Guid userId, IEnumerable<int> profiles)
        {
            return await base.GetFirstAsync(w =>
                    w.UserId == userId &&
                    profiles.Contains(w.ProfileId));
        }
    }
}