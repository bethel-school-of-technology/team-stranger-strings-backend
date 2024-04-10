using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using team_stranger_strings_backend.Migrations;
using team_stranger_strings_backend.Models;
using Microsoft.IdentityModel.Tokens;
using bcrypt = BCrypt.Net.BCrypt;

namespace team_stranger_strings_backend.Repositories;

public class UserRepository : IUserRepository
{
    private static VehicleDbContext _context;
    private static IConfiguration _config;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserRepository(VehicleDbContext context, IConfiguration config, IHttpContextAccessor httpContextAccessor) {
        _context = context;
        _config = config;
        _httpContextAccessor = httpContextAccessor;
    }

    public User CreateUser(User user)
    {
        var passwordHash = bcrypt.HashPassword(user.Password);
        user.Password = passwordHash;
        
        _context.Add(user);
        _context.SaveChanges();
        return user;
    }

    public string SignIn(string Email, string Password)
    {
        var user = _context.Users.SingleOrDefault(x => x.Email == Email);
        var verified = false;

        if (user != null) {
            verified = bcrypt.Verify(Password, user.Password);
        }

        if (user == null || !verified)
        {
            return String.Empty;
        }
        
        return BuildToken(user);
    }

    private string BuildToken(User user) {
        var secret = _config.GetValue<String>("TokenSecret");
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim("User_userid", user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email ?? ""),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ?? ""),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ?? "")
        };

        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: signingCredentials);

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }

    public User? GetUserByEmail(string Email){
        return _context.Users.FirstOrDefault(u => u.Email == Email);
    }
    public User? UpdateUser(User newUser)
    {
        var originalUser = _context.Users.FirstOrDefault(u => u.Email == newUser.Email);
        if (originalUser != null)
        {
            originalUser.Email = newUser.Email;
            originalUser.FirstName = newUser.FirstName;
            originalUser.LastName = newUser.LastName;
            originalUser.Bio = newUser.Bio;
            originalUser.Location = newUser.Location;
            _context.SaveChanges();
        }
        return originalUser;
    }
}