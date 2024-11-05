using Core;
using Core.Services;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Api.Controllers;

/*[Route("api/[controller]")]*/
[AllowAnonymous]
[ApiController]
public class AccessController(AppDbContext context, IJwtService jwtService, IUserService userService) : ControllerBase
{
    [HttpPost("registerUser")]
    public async Task<IActionResult> Register(UserDTO userDto)
    {
        var newUser = new User();
        var userDB = await userService.GetUserByEmail(userDto.Email);
        
        if (userDB == null)
            newUser = await userService.AddUser(userDto.Name, userDto.Email, userDto.Password);
        
        return Ok(new { isSuccess = newUser.ID != 0 });
    }

    [HttpPost ("login")]
    public async Task<IActionResult> Login(LoginDTO loginDto)
    {
        var userFound = await context.Users
            .Where(u => u.Email == loginDto.Email && u.Password == jwtService.EncrypterSha256(loginDto.Password))
            .FirstOrDefaultAsync();

        /*return userFound == null ? StatusCode(StatusCodes.Status200OK, new {isSuccess = false}) : StatusCode(StatusCodes.Status200OK, new {isSuccess = true, token = jwtService.GeneratorJWT(userFound)});*/
        return userFound == null ? Ok(new { isSuccess = false }) : Ok(new { isSuccess = true, token = jwtService.GeneratorJWT(userFound) });
    }
}