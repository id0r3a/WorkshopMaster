using Microsoft.EntityFrameworkCore;
using WorkshopMaster.Domain.Entities;

namespace WorkshopMaster.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();
        public DbSet<ServiceType> ServiceTypes => Set<ServiceType>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<BookingServiceType> BookingServiceTypes => Set<BookingServiceType>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(x => x.FullName).IsRequired().HasMaxLength(200);
                entity.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(50);
                entity.Property(x => x.Email).IsRequired().HasMaxLength(200);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(x => x.RegistrationNumber).IsRequired().HasMaxLength(20);
                entity.Property(x => x.Brand).IsRequired().HasMaxLength(100);
                entity.Property(x => x.Model).IsRequired().HasMaxLength(100);

                entity.HasOne(x => x.Customer)
                    .WithMany(c => c.Vehicles)
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ServiceType>(entity =>
            {
                entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
                entity.Property(x => x.BasePrice).HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(x => x.Status).IsRequired().HasMaxLength(50);

                entity.HasOne(x => x.Vehicle)
                    .WithMany(v => v.Bookings)
                    .HasForeignKey(x => x.VehicleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<BookingServiceType>(entity =>
            {
                entity.HasKey(x => new { x.BookingId, x.ServiceTypeId });

                entity.HasOne(x => x.Booking)
                    .WithMany(b => b.BookingServiceTypes)
                    .HasForeignKey(x => x.BookingId);

                entity.HasOne(x => x.ServiceType)
                    .WithMany(s => s.BookingServiceTypes)
                    .HasForeignKey(x => x.ServiceTypeId);
            });
        }
    }
}
