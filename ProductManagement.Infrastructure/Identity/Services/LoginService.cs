using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Identity.DTO;
using ProductManagement.Application.Identity.Interfaces;
using ProductManagement.Infrastructure.Identity.Models;

namespace ProductManagement.Infrastructure.Identity.Services;

public class LoginService : ILoginService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IIdentityUserCreator _userCreator;

    public LoginService(UserManager<ApplicationUser> userManager,
                        SignInManager<ApplicationUser> signInManager,
                        IIdentityUserCreator userCreator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userCreator = userCreator;
    }
    
    public async Task<Result<ApplicationUserDto>> Login(LoginUserDto loginUser)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginUser.UserName);

        if (user is null)
        {
           return Result<ApplicationUserDto>.Unauthorized(DomainErrors.Login.UserNameVerification);
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginUser.Password, false);

        if (!result.Succeeded)
        {
            return Result<ApplicationUserDto>.Unauthorized(DomainErrors.Login.CredentialsVerification);
        }

        var identityUser = _userCreator.CreateIdentityUser(user?.UserName, user?.Email);
        return Result<ApplicationUserDto>.Success(identityUser);
    }
}