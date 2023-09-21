using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using ProductManagement.Infrastructure.Identity.Models;

namespace ProductManagement.Infrastructure.Tests.Identity.Mocks;

public class FakeSignInManager : SignInManager<ApplicationUser>
{            
    public FakeSignInManager()
        : base(
            new Mock<FakeUserManager>().Object,
            new HttpContextAccessor(),
            new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
            new Mock<Microsoft.Extensions.Options.IOptions<IdentityOptions>>().Object,
            new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
            new Mock<Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider>().Object,
            new Mock<Microsoft.AspNetCore.Identity.IUserConfirmation<ApplicationUser>>().Object
        )
    { }
}