using Extensions.Web.Results;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Identity.DTO;
using ProductManagement.Application.Identity.Interfaces;
using ProductManagement.Infrastructure.Identity.Helpers;
using ProductManagement.Infrastructure.Identity.Models;
using ProductManagement.Infrastructure.Identity.Services;
using ProductManagement.Infrastructure.Tests.Identity.Common;
using ProductManagement.Infrastructure.Tests.Identity.Constants;
using ProductManagement.Infrastructure.Tests.Identity.Mocks;

namespace ProductManagement.Infrastructure.Tests.Identity.Services;

public class LoginServiceTests
{
    private readonly Mock<FakeUserManager> _userManager;
    private readonly Mock<FakeSignInManager> _signInManager;
    private readonly IIdentityUserCreator _userCreator;
    private readonly Mock<ITokenService> _tokenService;
    
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
        var user = ApplicationUserCreator.CreateApplicationUser();
        var userDto = new LoginUserDto
        {
            UserName = UserCredentialsConstants.UserName,
            Password = UserCredentialsConstants.Password
        };
        
        _userManager.Setup(x => x.FindByNameAsync(user.UserName))
            .ReturnsAsync(user)
            .Verifiable();
        
        _signInManager.Setup(x => x.CheckPasswordSignInAsync(user, UserCredentialsConstants.Password, false))
            .ReturnsAsync(SignInResult.Success)
            .Verifiable();
        
        _tokenService.Setup(x => x.GenerateToken(UserCredentialsConstants.UserName, UserCredentialsConstants.Password))
            .Returns(UserCredentialsConstants.Token)
            .Verifiable();

        var loginService = new LoginService(_userManager.Object, _signInManager.Object, _userCreator);

        var result = await loginService.Login(userDto);


        _userManager.Verify(x => x.FindByNameAsync(user.UserName),Times.Exactly(1));

        _signInManager.Verify(x => x.CheckPasswordSignInAsync(user, UserCredentialsConstants.Password, false),Times.Exactly(1));

        _tokenService.Verify(x => x.GenerateToken(UserCredentialsConstants.UserName, UserCredentialsConstants.Email),Times.Exactly(1));

        result.Should().BeAssignableTo<Result<ApplicationUserDto>>();
        result.IsSuccess.Should().BeTrue();
        result.Value.UserName.Should().BeEquivalentTo(UserCredentialsConstants.UserName);
        result.Value.Email.Should().BeEquivalentTo(UserCredentialsConstants.Email);
    }
    
    [Fact]
    public async Task Login_WithNotExistingCredentials_ReturnsNotAuthorized()
    {
        var user = ApplicationUserCreator.CreateApplicationUser();
        var userDto = new LoginUserDto
        {
            UserName = UserCredentialsConstants.UserName,
            Password = UserCredentialsConstants.Password
        };
        
        _userManager.Setup(x => x.FindByNameAsync(user.UserName))
            .ReturnsAsync(default(ApplicationUser))
            .Verifiable();
        
        _signInManager.Setup(x => x.CheckPasswordSignInAsync(user, UserCredentialsConstants.Password, false))
            .Verifiable();
        
        _tokenService.Setup(x => x.GenerateToken(UserCredentialsConstants.UserName, UserCredentialsConstants.Email))
            .Verifiable();

        var loginService = new LoginService(_userManager.Object, _signInManager.Object, _userCreator);

        var result = await loginService.Login(userDto);


        _userManager.Verify(x => x.FindByNameAsync(user.UserName),Times.Exactly(1));

        _signInManager.Verify(x => x.CheckPasswordSignInAsync(user, UserCredentialsConstants.Password, false),Times.Never);

        _tokenService.Verify(x => x.GenerateToken(UserCredentialsConstants.UserName, UserCredentialsConstants.Email),Times.Never);

        result.Should().BeAssignableTo<Result<ApplicationUserDto>>();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Login.UserNamesNotRegistered);
    }
    
    
    [Fact]
    public async Task Login_WithInvalidPassword_ReturnsNotAuthorized()
    {
        var user = ApplicationUserCreator.CreateApplicationUser();
        var userDto = new LoginUserDto
        {
            UserName = UserCredentialsConstants.UserName,
            Password = UserCredentialsConstants.Password
        };
        
        _userManager.Setup(x => x.FindByNameAsync(user.UserName))
            .ReturnsAsync(user)
            .Verifiable();
        
        _signInManager.Setup(x => x.CheckPasswordSignInAsync(user, UserCredentialsConstants.Password, false))
            .ReturnsAsync(SignInResult.Failed)
            .Verifiable();
        
        _tokenService.Setup(x => x.GenerateToken(UserCredentialsConstants.UserName, UserCredentialsConstants.Email))
            .Verifiable();

        var loginService = new LoginService(_userManager.Object, _signInManager.Object, _userCreator);

        var result = await loginService.Login(userDto);


        _userManager.Verify(x => x.FindByNameAsync(user.UserName),Times.Exactly(1));

        _signInManager.Verify(x => x.CheckPasswordSignInAsync(user, UserCredentialsConstants.Password, false),Times.Exactly(1));

        _tokenService.Verify(x => x.GenerateToken(UserCredentialsConstants.UserName, UserCredentialsConstants.Email),Times.Never);

        result.Should().BeAssignableTo<Result<ApplicationUserDto>>();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Login.InvalidCredentialsProvided);
    }
}