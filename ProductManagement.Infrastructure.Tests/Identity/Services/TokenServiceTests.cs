using FluentAssertions;
using Moq;
using ProductManagement.Domain.Interfaces;
using ProductManagement.Infrastructure.Identity.Services;
using ProductManagement.Infrastructure.Tests.Identity.Constants;

namespace ProductManagement.Infrastructure.Tests.Identity.Services;

public class TokenServiceTests
{
    private readonly Mock<IDateTime> _dateTimeService;
    
    public TokenServiceTests()
    {
        _dateTimeService = new Mock<IDateTime>();
    }

    [Fact]
    public void GenerateToken_WithValidClaims_ReturnsToke()
    {
        _dateTimeService.Setup(x =>x.Now)
            .Returns(DateTime.Now)
            .Verifiable();

        var tokenService = new TokenService(_dateTimeService.Object);

        var token = tokenService.GenerateToken(UserCredentialsConstants.UserName, UserCredentialsConstants.Email);

        token.Should().NotBe(null)
            .And
            .NotBe(String.Empty);
    }
}