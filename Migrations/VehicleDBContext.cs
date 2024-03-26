using team_stranger_strings_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace team_stranger_strings_backend.Migrations;

public class VehicleDbContext : DbContext
{
    public DbSet<Vehicle> Vehicles { get; set; }


    public VehicleDbContext(DbContextOptions<VehicleDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Vehicle>().HasData(
            new Vehicle { 
                VehicleId = 1,
                Make = "Audi",
                Model = "Q8",
                Year = 2023,
                Colour = "blue",
                Photo = "https://www.motortrend.com/uploads/2022/09/2023-Audi-RS-Q8-PVOTY22-24.jpg",
                Price = 79100
            }
        );

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId);
            entity.Property(e => e.Make).IsRequired();
            entity.Property(e => e.Model).IsRequired();
            entity.Property(e => e.Year).IsRequired();
            entity.Property(e => e.Colour).IsRequired();
            entity.Property(e => e.Photo).IsRequired();
            entity.Property(e => e.Price).IsRequired();

        });
    }
}