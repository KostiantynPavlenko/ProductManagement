using ProductManagement.Application.Identity.DTO;
using ProductManagement.Application.Identity.Interfaces;

namespace ProductManagement.Infrastructure.Identity.Helpers;

public class IdentityUserCreator : IIdentityUserCreator
{
    private readonly ITokenService _tokenService;

    public IdentityUserCreator(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
    public ApplicationUserDto CreateIdentityUser(string username, string email)
    {
        return new ApplicationUserDto
        {
            UserName = username,
            Email = email,
            Token = _tokenService.GenerateToken(username, email)
        };
    }
}