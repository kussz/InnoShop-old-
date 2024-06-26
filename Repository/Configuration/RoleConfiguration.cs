using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using UMS.Entities.Models;

namespace Repository.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData
            (
            new Role
            {
                Id = new Guid("bba84abf-a571-463f-b4e0-fcec5729c454"),
                Name = "Admin",
                ManipulationAccess = true,
                PostAccess = true
            },
            new Role
            {
                Id = new Guid("2dd259f0-e447-4bf5-82a1-4646f89712e5"),
                Name = "Regular",
                ManipulationAccess = false,
                PostAccess = true
            },
            new Role
            {
                Id = new Guid("78dbc134-6233-4605-962a-17263cec1c95"),
                Name = "Guest",
                ManipulationAccess = false,
                PostAccess = false
            }
            );
        }
    }

}
