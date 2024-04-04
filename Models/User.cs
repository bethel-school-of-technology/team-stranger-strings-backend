using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using team_stranger_strings_backend.Models;

namespace backend.Models;

public class User 
{
    [JsonIgnore]
    public int UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Bio { get; set; }
    public string? Location { get; set; }
    [JsonIgnore]
    public IEnumerable<Vehicle>? Vehicle { get; set;}
}