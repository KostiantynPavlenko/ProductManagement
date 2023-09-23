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

public class RegisterServiceTests
{
    private readonly Mock<FakeUserManager> _userManager;
    private readonly IIdentityUserCreator _userCreator;
    private readonly Mock<ITokenService> _tokenService;
    
    public RegisterServiceTests()
    {
        _userManager = new Mock<FakeUserManager>();
        _tokenService = new Mock<ITokenService>();
        _userCreator = new IdentityUserCreator(_tokenService.Object);
    }
    
    [Fact]
    public async Task Register_WithValidCredentials_UserIsSignedUp()
    {
        var user = ApplicationUserCreator.CreateApplicationUser();
        
        var registerUser = new RegisterUserDto
        {
            FirstName = UserCredentialsConstants.FirstName,
            LastName = UserCredentialsConstants.LastName,
            Email = UserCredentialsConstants.Email,
            UserName = UserCredentialsConstants.UserName,
            Password = UserCredentialsConstants.Password
        };
        
        _userManager.Setup(x => x.FindByNameAsync(user.UserName))
            .ReturnsAsync(default(ApplicationUser))
            .Verifiable();        
        
        _userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), registerUser.Password))
            .ReturnsAsync(IdentityResult.Success)
            .Verifiable();
        
        _tokenService.Setup(x => x.GenerateToken(UserCredentialsConstants.UserName, UserCredentialsConstants.Password))
            .Returns(UserCredentialsConstants.Token)
            .Verifiable();

        var registerService = new RegisterService(_userManager.Object, _userCreator);

        var result = await registerService.Register(registerUser);
        
        _userManager.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), registerUser.Password),Times.Exactly(1));
        _userManager.Verify(x => x.FindByNameAsync(user.UserName),Times.Exactly(1));
        _tokenService.Verify(x => x.GenerateToken(UserCredentialsConstants.UserName, UserCredentialsConstants.Email),Times.Exactly(1));

        result.Should().BeAssignableTo<Result<ApplicationUserDto>>();
        result.IsSuccess.Should().BeTrue();
        result.Value.UserName.Should().BeEquivalentTo(UserCredentialsConstants.UserName);
        result.Value.Email.Should().BeEquivalentTo(UserCredentialsConstants.Email);
    }
    
    [Fact]
    public async Task Register_WithExistingUserCredentials_ReturnFailureResult()
    {
        var user = ApplicationUserCreator.CreateApplicationUser();
        
        var registerUser = new RegisterUserDto
        {
            FirstName = UserCredentialsConstants.FirstName,
            LastName = UserCredentialsConstants.LastName,
            Email = UserCredentialsConstants.Email,
            UserName = UserCredentialsConstants.UserName,
            Password = UserCredentialsConstants.Password
        };
        
        _userManager.Setup(x => x.FindByNameAsync(user.UserName))
            .ReturnsAsync(user)
            .Verifiable();        
        
        _userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), registerUser.Password))
            .Verifiable();
        
        _tokenService.Setup(x => x.GenerateToken(UserCredentialsConstants.UserName, UserCredentialsConstants.Password))
            .Verifiable();

        var registerService = new RegisterService(_userManager.Object, _userCreator);

        var result = await registerService.Register(registerUser);
        
        _userManager.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), registerUser.Password),Times.Never);
        _userManager.Verify(x => x.FindByNameAsync(user.UserName),Times.Exactly(1));
        _tokenService.Verify(x => x.GenerateToken(UserCredentialsConstants.UserName, UserCredentialsConstants.Email),Times.Never);

        result.Should().BeAssignableTo<Result<ApplicationUserDto>>();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Registration.UserNameAlreadyExists);
    }
    
    [Fact]
    public async Task Register_ThrowErrorWithValidCredentials_ReturnsFailureResult()
    {
        var user = ApplicationUserCreator.CreateApplicationUser();
        
        var registerUser = new RegisterUserDto
        {
            FirstName = UserCredentialsConstants.FirstName,
            LastName = UserCredentialsConstants.LastName,
            Email = UserCredentialsConstants.Email,
            UserName = UserCredentialsConstants.UserName,
            Password = UserCredentialsConstants.Password
        };
        
        _userManager.Setup(x => x.FindByNameAsync(user.UserName))
            .ReturnsAsync(default(ApplicationUser))
            .Verifiable();        
        
        _userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), registerUser.Password))
            .ReturnsAsync(IdentityResult.Failed())
            .Verifiable();
        
        _tokenService.Setup(x => x.GenerateToken(UserCredentialsConstants.UserName, UserCredentialsConstants.Password))
            .Verifiable();

        var registerService = new RegisterService(_userManager.Object, _userCreator);

        var result = await registerService.Register(registerUser);
        
        _userManager.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), registerUser.Password),Times.Exactly(1));
        _userManager.Verify(x => x.FindByNameAsync(user.UserName),Times.Exactly(1));
        _tokenService.Verify(x => x.GenerateToken(UserCredentialsConstants.UserName, UserCredentialsConstants.Email),Times.Never);

        result.Should().BeAssignableTo<Result<ApplicationUserDto>>();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Registration.UserCreationFailed);
    }
}