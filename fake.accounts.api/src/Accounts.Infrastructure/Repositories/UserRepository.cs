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

        public async Task<User> GetByEmail(string email)
        {
            return await _table
                .Include(i => i.UsersProfiles)
                .FirstOrDefaultAsync(w => w.Email == email);
        }

        public async Task<IEnumerable<User>> GetPaginationAsync(string search, int position, int take)
        {
            return await _table
                .Where(WherePagination(search))
                .OrderBy(o => o.Name)
                .Skip(position == 0 ? position : (position - 1) * take)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> CountPaginationAsync(string search)
        {
            return await _table
                .Where(WherePagination(search))
                .CountAsync();
        }

        private static Expression<Func<User, bool>> WherePagination(string search)
        {
            return w => 
                w.Name.ToLower().Contains(search.ToLower()) ||
                w.Email.ToLower().Contains(search.ToLower()) || 
                (search == null || search == ""); 
        }
    }
}