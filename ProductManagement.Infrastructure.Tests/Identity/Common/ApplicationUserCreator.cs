using ProductManagement.Infrastructure.Identity.Models;
using ProductManagement.Infrastructure.Tests.Identity.Constants;

namespace ProductManagement.Infrastructure.Tests.Identity.Common;

public static class ApplicationUserCreator
{
    public static ApplicationUser CreateApplicationUser()
    {
        return new ApplicationUser
        {
            FirstName = UserCredentialsConstants.FirstName,
            LastName = UserCredentialsConstants.LastName,
            Email = UserCredentialsConstants.Email,
            UserName = UserCredentialsConstants.UserName,
        };
    }
}