using iPractice.Domain.Entities;
using iPractice.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace iPractice.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Psychologist> Psychologists { get; set; }
        public DbSet<Client> Clients { get; set; } 

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Psychologist>().HasKey(psychologist => psychologist.Id);
            modelBuilder.Entity<Psychologist>().HasMany(p => p.Clients).WithMany(b => b.Psychologists);
            modelBuilder.Entity<Psychologist>().HasMany(p => p.Bookings).WithOne(b => b.Psychologist);
            modelBuilder.Entity<Psychologist>().HasMany(p => p.Availabilities);

            modelBuilder.Entity<Client>().HasKey(client => client.Id);
            modelBuilder.Entity<Client>().HasMany(p => p.Psychologists).WithMany(b => b.Clients);
            modelBuilder.Entity<Client>().HasMany(p => p.Bookings).WithOne(b => b.Client);

            modelBuilder.Entity<AvailabilityEntity>().HasKey(availability => availability.Id);
            modelBuilder.Entity<AvailabilityEntity>().Property(availability => availability.Id).ValueGeneratedOnAdd();            
            
            modelBuilder.Entity<Booking>().HasKey(booking => booking.Id);
            modelBuilder.Entity<Booking>().HasOne(b => b.Client).WithMany(c => c.Bookings);
            modelBuilder.Entity<Booking>().HasOne(b => b.Psychologist).WithMany(p => p.Bookings);
        }
    }
}
