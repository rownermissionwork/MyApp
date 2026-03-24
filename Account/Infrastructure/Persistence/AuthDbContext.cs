using Account.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Infrastructure.Persistence
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        // Define your DbSets here, for example:
        // public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogin { get; set; }
        public DbSet<Users> Users { get; set; }

    }
}
