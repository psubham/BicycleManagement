using DeliverPage.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverPage.Data
{
    public class DeliveryDbContext:DbContext
    {
        public DeliveryDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Delivery> delivery { get; set; }
    }
}
