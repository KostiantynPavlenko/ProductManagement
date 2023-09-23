using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moq;
using ProductManagement.Infrastructure.Identity.Services;
using ProductManagement.Infrastructure.Tests.Identity.Constants;

namespace ProductManagement.Infrastructure.Tests.Identity.Services;

public class UserAccessorTests
{
    private Mock<IHttpContextAccessor> _httpContextAccessorMock;

    public UserAccessorTests()
    {
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
    }

    [Fact]
    public void GetCurrentUserName_ReturnsUserName()
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, UserCredentialsConstants.UserName),
            new Claim(ClaimTypes.Email, UserCredentialsConstants.Email)
        };

        var identity = new ClaimsIdentity(claims, "TestAuthentication");
        
        var context = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(identity)
        };

        _httpContextAccessorMock.Setup(x => x.HttpContext)
            .Returns(context);

        var userAccessor = new UserAccessor(_httpContextAccessorMock.Object);

        var result = userAccessor.GetCurrentUserName();

        Assert.Equal(UserCredentialsConstants.UserName, result);
    }
}