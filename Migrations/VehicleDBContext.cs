using team_stranger_strings_backend.Models;
using Microsoft.EntityFrameworkCore;


namespace team_stranger_strings_backend.Migrations;

public class VehicleDbContext : DbContext
{
    public DbSet<Vehicle> Vehicles { get; set; }

    public DbSet<User> Users { get; set; }

    public VehicleDbContext(DbContextOptions<VehicleDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Vehicle)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId);
            entity.Property(e => e.Make).IsRequired();
            entity.Property(e => e.Model).IsRequired();
            entity.Property(e => e.Year).IsRequired();
            entity.Property(e => e.Colour).IsRequired();
            entity.Property(e => e.Photo).IsRequired();
            entity.Property(e => e.Price).IsRequired();
            entity.Property(e => e.UserId).IsRequired(false);
            entity.Property(e => e.UserEmail).IsRequired(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Email).IsRequired();
            entity.HasIndex(x => x.Email).IsUnique();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
            entity.Property(e => e.Bio).IsRequired();
        });

        modelBuilder.Entity<Vehicle>().HasData(
            new Vehicle { 
                VehicleId = 1,
                Make = "Audi",
                Model = "Q8",
                Year = 2023,
                Colour = "blue",
                Photo = "https://www.motortrend.com/uploads/2022/09/2023-Audi-RS-Q8-PVOTY22-24.jpg",
                Price = 79100,
                UserId = 1,
                UserEmail = "ivanasocegovorko@gmail.com",
            }
        );

        modelBuilder.Entity<User>().HasData(
            new User { 
                UserId = 1,
                FirstName = "Jude",
                LastName = "Klassen",
                Email = "jude@klassen.com",
                Password = "strangerstrings",
                Bio = "Hi I'm Jude Klassen.",
                Location = "Redding, California",
            }
        );
    }
}