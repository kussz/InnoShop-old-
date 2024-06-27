using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using UMS.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Repository.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData
            (
            new Role
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new Role
            {
                Name = "Regular",
                NormalizedName = "REGULAR"
            },
            new Role
            {
                Name = "Guest",
                NormalizedName = "GUEST"
            }
            );
        }
    }

}
