using Microsoft.EntityFrameworkCore;
using UMS.Entities.Models;
using Repository.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PMS.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Repository;

public class RepositoryContext : IdentityDbContext<User,Role,Guid>
{
    public RepositoryContext(DbContextOptions options)
    : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Role>().HasData(
        new Role { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN" },
        new Role { Id = Guid.NewGuid(), Name = "Regular", NormalizedName = "REGULAR" },
        new Role { Id = Guid.NewGuid(), Name = "Guest", NormalizedName = "GUEST" });
    }

    public DbSet<User>? Users { get; set; }
    public DbSet<Product>? Products { get; set; }


}
