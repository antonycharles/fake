using System.Linq.Expressions;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Accounts.Core.Repositories;
using Accounts.Infrastructure.Data;
using Accounts.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AccountsContext dbContext) : base(dbContext)
        {

        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var result = await _table
                .AsNoTracking()
                .Include(i => i.UsersProfiles)
                .ThenInclude(i => i.Profile.App)
                .Include(i => i.UsersProfiles)
                .ThenInclude(i => i.Profile.ProfilesRoles)
                .ThenInclude(i => i.Role)
                .FirstOrDefaultAsync(w => w.Email == email);

            return result;
        }

        public async Task<IEnumerable<User>> GetPaginationAsync(string search, int position, int take)
        {
            IQueryable<User> query = _dbContext.Set<User>();

            query = WherePagination(search, query);

            return await query
                .OrderBy(o => o.Name)
                .Skip(position == 0 ? position : (position - 1) * take)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> CountPaginationAsync(string search)
        {
            IQueryable<User> query = _dbContext.Set<User>();
            
            query = WherePagination(search, query);

            return await query.CountAsync();
        }

        private static IQueryable<User> WherePagination(string search, IQueryable<User> query)
        {
            if (search != null && search != "")
            {
                query = query.Where(
                    w => w.Name.ToLower().Contains(search.ToLower()) ||
                    w.Email.ToLower().Contains(search.ToLower()));
            }

            return query;
        }
    }
}