using Account.Domain.Entities;
using Account.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Infrastructure.Persistence
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }
        // Define your DbSets here, for example:
        // public DbSet<User> Users { get; set; }
     //   public DbSet<UserLogin> UserLogin { get; set; }
        public DbSet<Entities.Users> User { get; set; }
        public DbSet<Entities.UserProfile> UserProfiles { get; set; }
        public DbSet<Entities.UserRole> UserRoles { get; set; }

    }
}
