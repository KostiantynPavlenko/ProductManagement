using Extensions.Web.Results;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using ProductManagement.Application.Identity.DTO;
using ProductManagement.Application.Identity.Interfaces;
using ProductManagement.Infrastructure.Identity.Helpers;
using ProductManagement.Infrastructure.Identity.Models;
using ProductManagement.Infrastructure.Identity.Services;
using ProductManagement.Infrastructure.Tests.Identity.Mocks;

namespace ProductManagement.Infrastructure.Tests.Identity.Services;

public class LoginServiceTests
{
    private readonly Mock<FakeUserManager> _userManager;
    private readonly Mock<FakeSignInManager> _signInManager;
    private readonly IIdentityUserCreator _userCreator;
    private readonly Mock<ITokenService> _tokenService;

    private const string Email = "Test@gmail.com";
    private const string UserName = "UserName";
    private const string Password = "Password123";
    private const string Token = "We123rtfgfdsfsdgdrgh4wrfsdfsd";

    
    
    public LoginServiceTests()
    {
        _userManager = new Mock<FakeUserManager>();
        _signInManager = new Mock<FakeSignInManager>();
        
         _tokenService = new Mock<ITokenService>();
        _userCreator = new IdentityUserCreator(_tokenService.Object);
    }

    [Fact]
    public async Task Login_WithValidCredentials_UserIsLoggedIn()
    {
        var user = CreateApplicationUser();
        var userDto = new LoginUserDto
        {
            UserName = UserName,
            Password = Password
        };
        
        _userManager.Setup(x => x.FindByNameAsync(user.UserName))
            .ReturnsAsync(user)
            .Verifiable();
        
        _signInManager.Setup(x => x.CheckPasswordSignInAsync(user, Password, false))
            .ReturnsAsync(SignInResult.Success)
            .Verifiable();
        
        _tokenService.Setup(x => x.GenerateToken(UserName, Email))
            .Returns(Token)
            .Verifiable();

        var loginService = new LoginService(_userManager.Object, _signInManager.Object, _userCreator);

        var result = await loginService.Login(userDto);


        _userManager.Verify(x => x.FindByNameAsync(user.UserName),Times.Exactly(1));

        _signInManager.Verify(x => x.CheckPasswordSignInAsync(user, Password, false),Times.Exactly(1));

        _tokenService.Verify(x => x.GenerateToken(UserName, Email),Times.Exactly(1));

        result.Should().BeAssignableTo<Result<ApplicationUserDto>>();
        result.IsSuccess.Should().BeTrue();
        result.Value.UserName.Should().BeEquivalentTo(UserName);
        result.Value.Email.Should().BeEquivalentTo(Email);
    }

    private ApplicationUser CreateApplicationUser()
    {
        return new ApplicationUser
        {
            Email = Email,
            UserName = UserName
        };
    }
}