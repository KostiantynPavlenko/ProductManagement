namespace ProductManagement.Application.Identity.Interfaces;

public interface ITokenService
{
    string GenerateToken(string username, string email);
}