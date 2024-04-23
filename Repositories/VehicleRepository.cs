using team_stranger_strings_backend.Migrations;
using team_stranger_strings_backend.Models;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace team_stranger_strings_backend.Repositories;

public class VehicleRepository : IVehicleRepository 
{
    private readonly VehicleDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userService;
    public VehicleRepository(VehicleDbContext context, IHttpContextAccessor httpContextAccessor, IUserRepository service)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _userService = service;
    }


    public Vehicle CreateVehicle(Vehicle newVehicle)
    {
        
        var email = _httpContextAccessor.HttpContext.User.Identity.Name;
        var user = _userService.GetUserByEmail(email);
        if (user != null)
    {
        newVehicle.UserId = user.UserId;
        newVehicle.UserEmail = user.Email;
        newVehicle.User = user;

        _context.Vehicles.Add(newVehicle);
        _context.SaveChanges();
        return newVehicle;
    }

    throw new InvalidOperationException("User not found");
        
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
            originalVehicle.UserEmail = newVehicle.UserEmail;
            _context.SaveChanges();
        }
        return originalVehicle;
    }

    public IEnumerable<Vehicle> GetUsersVehicles(string Email)
    {
        return _context.Vehicles.Where(Vehicle => Vehicle.User.Email == Email).ToList();
    }
}