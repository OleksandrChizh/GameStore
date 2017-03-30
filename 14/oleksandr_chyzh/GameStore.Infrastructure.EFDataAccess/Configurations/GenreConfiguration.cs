using System.Data.Entity.ModelConfiguration;
using GameStore.Domain.Core.Models;

namespace GameStore.Infrastructure.EFDataAccess.Configurations
{
    public class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            Property(g => g.Name).IsRequired().HasMaxLength(200);
            HasOptional(g => g.ParentGenre).WithMany();
        }
    }
}
