using Microsoft.EntityFrameworkCore;
using Provider.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Infrastructure.Persistence
{
    public class ServicesDbContext : DbContext
    {
        public ServicesDbContext(DbContextOptions<ServicesDbContext> options) : base(options)
        {
        }
        public DbSet<ServiceProviderDetails> ServiceProviderDetail { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
