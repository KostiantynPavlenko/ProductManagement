using Extensions.Web.Results;
using FluentAssertions;
using Moq;
using ProductManagement.Application.Identity.Commands.RegisterCommand;
using ProductManagement.Application.Identity.DTO;
using ProductManagement.Application.Identity.Interfaces;
using ProductManagement.Application.Tests.Common;

namespace ProductManagement.Application.Tests.Identity;

public class RegisterCommandHandlerTests
{
    private readonly Mock<IRegisterService> _registerService;

    public RegisterCommandHandlerTests()
    {
        _registerService = new Mock<IRegisterService>();
    }

    [Fact]
    public async Task Register_WithValidUserData_UserIsRegistered()
    {
        var registerUser = new RegisterUserDto
        {
            FirstName = "FirstName",
            LastName = "LastName",
            Email = "User@gmail.com",
            UserName = "UserName",
            Password = "Password"
        };

        var applicationUser = new ApplicationUserDto
        {
            UserName = registerUser.UserName,
            Email = registerUser.Email,
            Token = DataGenerator.GenerateString(25)
        };

        var resultOfRegistering = Result<ApplicationUserDto>.Success(applicationUser);

        _registerService.Setup(x => x.Register(registerUser))
            .ReturnsAsync(resultOfRegistering)
            .Verifiable();

        var registerCommand = new RegisterCommand
        {
            RegisterUser = registerUser
        };

        var registerCommandHandler = new RegisterCommandHandler(_registerService.Object);

        var result = await registerCommandHandler.Handle(registerCommand, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(applicationUser);
        _registerService.Verify(x => x.Register(registerUser), Times.Exactly(1));
    }
}