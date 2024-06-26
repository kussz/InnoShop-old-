using Microsoft.EntityFrameworkCore;
using UMS.Entities.Models;
using Repository.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UMS.Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options)
    : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }

    public DbSet<User>? Users { get; set; }
    public DbSet<Role>? Roles { get; set; }


}
