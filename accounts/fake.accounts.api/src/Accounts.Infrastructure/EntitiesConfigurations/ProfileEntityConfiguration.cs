using Accounts.Core.Entities;
using Accounts.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.Infrastructure.EntitiesConfigurations
{
    public class ProfileEntityConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();
                
            builder
                .Property(s => s.Status)
                .HasDefaultValue(StatusEnum.Active);

            builder
                .Property(s => s.IsDefault)
                .HasDefaultValue(false);

            builder
                .Property(s => s.IsSystem)
                .HasDefaultValue(false);
        }
    }
}