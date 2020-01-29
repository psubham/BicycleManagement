using BicycleApp.Data.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleApp.Data.DataAccessLayer
{
    public class BicycleDbContext:DbContext
    {
        public BicycleDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<BicycleType> BicycleType { get; set; }
        public DbSet<Bicycles> Bicycles { get; set; }
        public DbSet<Booking> Bookings { get; set; }

    }
}
