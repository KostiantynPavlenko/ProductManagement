using FluentAssertions;
using Moq;
using ProductManagement.Application.Identity.Interfaces;
using ProductManagement.Infrastructure.Identity.Helpers;

namespace ProductManagement.Infrastructure.Tests.Identity.Helpers;

public class IdentityUserCreatorTests
{
    private readonly Mock<ITokenService> _tokenService;
    private const string UserName = "UserName";
    private const string Email = "user@gmail.com";
    private const string Token = "We123rtfgfdsfsdgdrgh4wrfsdfsd";

    public IdentityUserCreatorTests()
    {
        _tokenService = new Mock<ITokenService>();
    }

    [Fact]
    public void CreateIdentityUser_WithValidCredentialsAfterAuth_UserDtoIsReturned()
    {
        _tokenService.Setup(x => x.GenerateToken(UserName, Email))
            .Returns(Token)
            .Verifiable();

        var identityCreator = new IdentityUserCreator(_tokenService.Object);

        var identity = identityCreator.CreateIdentityUser(UserName, Email);

        identity.Email.Should().Be(Email);
        identity.UserName.Should().Be(UserName);
        _tokenService.Verify(x => x.GenerateToken(UserName, Email), Times.Exactly(1));
    }
}