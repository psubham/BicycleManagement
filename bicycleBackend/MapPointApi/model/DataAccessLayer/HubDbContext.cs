using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapPointApi.model
{
    public class HubDbContext : DbContext
    {

        public HubDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Hub> hubs { get; set; }
    }
}
