using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPMSWebApplication.Model.Authentication
{
    /// <summary>
    /// To be used to store data in the Database.
    /// </summary>
    public class AuthenticationContext : DbContext
    {
        public AuthenticationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<String> Username { get; set; }
        public DbSet<String> Password { get; set; }
        public DbSet<String> Name { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<String>().ToTable("dbo.Users");
        }
    }
}
