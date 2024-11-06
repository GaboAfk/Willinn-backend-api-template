namespace Core;

public class UserDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; } = true;

}

public class RecoveryUserDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
}