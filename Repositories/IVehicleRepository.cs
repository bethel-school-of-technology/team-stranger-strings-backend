using team_stranger_strings_backend.Models;

namespace team_stranger_strings_backend.Repositories;

public interface IVehicleRepository
{
    IEnumerable<Vehicle> GetAllVehicles();
    Vehicle? GetVehicleById(int VehicleId);
    Vehicle CreateVehicle(Vehicle newVehicle);
    Vehicle? UpdateVehicle(Vehicle newVehicle);
    void DeleteVehicleById(int VehicleId);

}
