using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using GameStore.Domain.Core.Models;

namespace GameStore.Infrastructure.EFDataAccess.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).IsRequired();
            Property(u => u.UserName).IsRequired().HasMaxLength(50);
            Property(u => u.PasswordHash).IsRequired();
            Property(u => u.Salt).IsRequired();
            HasMany(u => u.Roles).WithMany();
        }
    }
}
