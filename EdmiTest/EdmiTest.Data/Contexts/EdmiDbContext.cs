using EdmiTest.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdmiTest.Data.Contexts
{
    public partial class EdmiDbContext : DbContext
    {
        public EdmiDbContext(DbContextOptions<EdmiDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<ElectricMeter> ElectricMeters { get; set; }
        public virtual DbSet<WaterMeter> WaterMeters { get; set; }
        public virtual DbSet<Gateway> Gateways { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Device>()
            //.HasAlternateKey(c => c.SerialNumber);

            modelBuilder.Entity<ElectricMeter>()
            .HasAlternateKey(c => c.SerialNumber);

            modelBuilder.Entity<WaterMeter>()
            .HasAlternateKey(c => c.SerialNumber);

            modelBuilder.Entity<Gateway>()
            .HasAlternateKey(c => c.SerialNumber);
        }

    }


}
