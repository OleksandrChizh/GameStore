using System.Data.Entity.ModelConfiguration;
using GameStore.Domain.Core.Models;

namespace GameStore.Infrastructure.EFDataAccess.Configurations
{
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            Property(c => c.Name).IsRequired();
            Property(c => c.Body).IsRequired();
            HasOptional(c => c.ParentComment).WithMany();
        }
    }
}
