using Microsoft.EntityFrameworkCore;
using Service.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Infrastructure.Persistence
{
    public class ServicesDbContext : DbContext
    {
        public ServicesDbContext(DbContextOptions<ServicesDbContext> options) : base(options)
        {
        }
        // Define your DbSets here, for example:
        // public DbSet<Service> Services { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Services> Services { get; set; }
    }
}
