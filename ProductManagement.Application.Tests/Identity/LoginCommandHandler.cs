using Extensions.Web.Results;
using FluentAssertions;
using Moq;
using ProductManagement.Application.Identity.Commands.LoginCommand;
using ProductManagement.Application.Identity.DTO;
using ProductManagement.Application.Identity.Interfaces;
using ProductManagement.Application.Tests.Common;

namespace ProductManagement.Application.Tests.Identity;

public class LoginCommandHandlerTests
{
    private readonly Mock<ILoginService> _loginService;

    public LoginCommandHandlerTests()
    {
        _loginService = new Mock<ILoginService>();
    }

    [Fact]
    public async Task Login_WithValidUserCredentials_UserIsAuthenticated()
    {
        var loginRequest = new LoginUserDto
        {
            UserName = DataGenerator.GenerateString(12),
            Password = DataGenerator.GenerateString(12)
        };
        
        var applicationUser = new ApplicationUserDto
        {
            UserName = loginRequest.UserName,
            Email = DataGenerator.GenerateString(25),
            Token = DataGenerator.GenerateString(25)
        };
        
        var loginResult = Result<ApplicationUserDto>.Success(applicationUser);
        
        _loginService.Setup(x => x.Login(loginRequest))
            .ReturnsAsync(loginResult)
            .Verifiable();

        var loginCommand = new LoginCommand
        {
            LoginUser = loginRequest
        };

        var loginCommandHandler = new LoginCommandHandler(_loginService.Object);

        var result = await loginCommandHandler.Handle(loginCommand, CancellationToken.None);
        
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(applicationUser);
        _loginService.Verify(x => x.Login(loginRequest), Times.Exactly(1));
    }
}