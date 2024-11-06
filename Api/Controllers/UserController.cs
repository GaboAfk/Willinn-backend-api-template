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
    public async Task<ActionResult<User>> AddUser([FromBody] UserDTO user)
    {
        if (user.Name == null || user.Email == null || user.Password == null)
            return BadRequest();
        
        var response = await userService.AddUser (user.Name, user.Email, user.Password, user.IsActive);
        
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
    
    [HttpPut("users/{id}")]
    public async Task<ActionResult<User>> UpdateUser (User user)
    {
        try {
            var response = await userService.UpdateUser(user);
            return Ok(response);
        } catch (KeyNotFoundException ex) {
            return NotFound(ex.Message);
        } catch (Exception ex) {
            return BadRequest(ex.Message);
        }
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