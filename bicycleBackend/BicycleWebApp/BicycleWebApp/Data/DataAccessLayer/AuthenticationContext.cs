using BicycleWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleWebApp.DataAccessLayer
{
    public class AuthenticationContext: IdentityDbContext<ApplicatinUser>
    {
        public AuthenticationContext()
        {
        }

        public AuthenticationContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ApplicatinUser> ApplicatinUsers { get; set; }


    }
}
