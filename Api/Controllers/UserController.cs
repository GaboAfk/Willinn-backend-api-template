using Core;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Authorize]
/*[Route("api/[controller]")]*/
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost ("users")]
    public async Task<ActionResult<User>> AddUser(string name, string email, string password)
    {
        if (name == null || email == null || password == null)
            return BadRequest();
        
        var response = await userService.AddUser (name, email, password);
        
        if (response != null)
            return Ok(response);
        
        return BadRequest();
    }
    
    [HttpGet ("users")]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        var response = await userService.GetAllUsers();
        
        if (response != null)
            return Ok(response);
        
        return BadRequest();
    }
    
    [HttpGet ("users/{id:int}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var response = await userService.GetUserById(id);
        
        if (response != null)
            return Ok(response);
        
        return BadRequest();
    }
    
    [HttpGet ("users/{email}")]
    public async Task<ActionResult<User>> GetUserByEmail(string email)
    {
        var response = await userService.GetUserByEmail(email);
        
        if (response != null)
            return Ok(response);
        
        return BadRequest();
    }
    
    [HttpPut ("users/{id:int}")]
    public async Task<ActionResult<User>> UpdateUser(User user)
    {
        var response = await userService.UpdateUser(user);
        
        if (response != null)
            return Ok(response);
        
        return BadRequest();
    }

    [HttpDelete ("users/{id:int}")]
    public async Task<ActionResult<User>> DeleteUser(int id)
    {
        var response = await userService.DeleteUser(id);
        
        if (response != null)
            return Ok(response);
        
        return BadRequest();
    }
}