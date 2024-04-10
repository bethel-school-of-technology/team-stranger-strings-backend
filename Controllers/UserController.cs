using team_stranger_strings_backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using team_stranger_strings_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace team_stranger_strings_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase 
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _authService;

    public UserController(ILogger<UserController> logger, IUserRepository service)
    {
        _logger = logger;
        _authService = service;
    }

    [HttpPost]
    [Route("register")]
    public ActionResult CreateUser(User user) 
    {
        if (user == null || !ModelState.IsValid) {
            return BadRequest();
        }
        _authService.CreateUser(user);
        return NoContent();
    }

    [HttpGet]
    [Route("login")]
    public ActionResult<string> SignIn(string Email, string Password) 
    {
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            return BadRequest();
        }

        var token = _authService.SignIn(Email, Password);

        if (string.IsNullOrWhiteSpace(token)) {
            return Unauthorized();
        }

        return Ok(token);
    }

    [HttpGet]
    [Route("current")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<User> GetCurrentUser() 
    {
        var Email = User.Identity.Name;

        var user = _authService.GetUserByEmail(Email);

        if (user == null)
        {
            return NotFound("User not found");
        }

        return Ok(user);
    }

    [HttpGet]
    [Route("Email")]
    public ActionResult<User> GetUserByEmail(string Email) 
    {
        var user = _authService.GetUserByEmail(Email);

    if (user == null)
    {
        return NotFound("User not found");
    }

    return Ok(user);
    }

    [HttpPut]
    [Route("update")]
    public ActionResult<User> UpdateUser(User updatedUser)
    {
        if (updatedUser == null || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var existingUser = _authService.GetUserByEmail(updatedUser.Email);

        if (existingUser == null)
        {
            return NotFound("User not found");
        }

        return Ok(_authService.UpdateUser(updatedUser));
    }
}