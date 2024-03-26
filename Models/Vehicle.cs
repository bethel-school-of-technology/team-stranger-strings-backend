using System.ComponentModel.DataAnnotations;

namespace team_stranger_strings_backend.Models;

public class Vehicle 
{
    public int VehicleId { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public int Year { get; set; }
    public string? Colour { get; set; }
    public string? Photo { get; set; }

}