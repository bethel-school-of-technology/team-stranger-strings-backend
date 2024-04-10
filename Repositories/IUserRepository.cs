using team_stranger_strings_backend.Models;

namespace team_stranger_strings_backend.Repositories;

public interface IUserRepository 
{
    User CreateUser(User user);
    string SignIn(string Email, string Password);
    public User? GetUserByEmail(string Email);
    User? UpdateUser(User newUser);
}