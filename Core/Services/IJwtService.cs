namespace Core.Services;

public interface IJwtService
{
    string EncrypterSha256(string input);
    string GeneratorJWT(User user);
}