using team_stranger_strings_backend.Migrations;
using team_stranger_strings_backend.Models;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace team_stranger_strings_backend.Repositories;

public class VehicleRepository : IVehicleRepository 
{
    private readonly VehicleDbContext _context;

    public VehicleRepository(VehicleDbContext context)
    {
        _context = context;
    }


    public Vehicle CreateVehicle(Vehicle newVehicle)
    {
        _context.Vehicles.Add(newVehicle);
        _context.SaveChanges();
        return newVehicle;
    }

    public void DeleteVehicleById(int VehicleId)
    {
        var Vehicle = _context.Vehicles.Find(VehicleId);
        if (Vehicle != null) {
            _context.Vehicles.Remove(Vehicle); 
            _context.SaveChanges(); 
        }
    }

    public IEnumerable<Vehicle> GetAllVehicles()
    {
        return _context.Vehicles.ToList();
    }

    public Vehicle? GetVehicleById(int VehicleId)
    {
        return _context.Vehicles.SingleOrDefault(c => c.VehicleId == VehicleId);
    }

    public Vehicle? UpdateVehicle(Vehicle newVehicle)
    {
        var originalVehicle = _context.Vehicles.Find(newVehicle.VehicleId);
        if (originalVehicle != null) {
            originalVehicle.Make = newVehicle.Make;
            originalVehicle.Year = newVehicle.Year;
            originalVehicle.Colour = newVehicle.Colour;
            originalVehicle.Photo = newVehicle.Photo;
            originalVehicle.Price = newVehicle.Price;
            originalVehicle.UserId = newVehicle.UserId;
            _context.SaveChanges();
        }
        return originalVehicle;
    }
}