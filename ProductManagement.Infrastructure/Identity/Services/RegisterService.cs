using Extensions.Web.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Identity.DTO;
using ProductManagement.Application.Identity.Interfaces;
using ProductManagement.Infrastructure.Identity.Helpers;
using ProductManagement.Infrastructure.Identity.Models;

namespace ProductManagement.Infrastructure.Identity.Services;

public class RegisterService : IRegisterService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IIdentityUserCreator _userCreator;

    public RegisterService(UserManager<ApplicationUser> userManager, IIdentityUserCreator userCreator)
    {
        _userManager = userManager;
        _userCreator = userCreator;
    }
    
    public async Task<Result<ApplicationUserDto>> Register(RegisterUserDto registerUser)
    {
        var userExists = await _userManager.FindByNameAsync(registerUser.UserName) is not null;

        if (userExists)
        {
            return Result<ApplicationUserDto>.Failure(DomainErrors.Registration.UserNameAlreadyExists);
        }

        var user = new ApplicationUser
        {
            FirstName = registerUser.FirstName,
            LastName = registerUser.LastName,
            UserName = registerUser.UserName,
            Email = registerUser.Email
        };

        var result = await _userManager.CreateAsync(user, registerUser.Password);

        if (!result.Succeeded)
        {
            return Result<ApplicationUserDto>.Failure(DomainErrors.Registration.UserCreationFailed);
        }

        var createdUser = _userCreator.CreateIdentityUser(user.UserName, user.Email);
        return Result<ApplicationUserDto>.Success(createdUser);
    }
}