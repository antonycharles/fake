using Accounts.Core.Entities;
using Accounts.Infrastructure.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Data
{
    public class AccountsContext : DbContext
    {
        public AccountsContext(DbContextOptions<AccountsContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                                .SelectMany(t => t.GetForeignKeys())
                                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

                
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityConfiguration).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<App> Apps { get; set; }
        public DbSet<ClientProfile> ClientsProfiles { get; set; }
        public DbSet<ProfileRole> ProfilesRoles { get; set; }
        public DbSet<UserProfile> UsersProfiles { get; set; }
    }
}