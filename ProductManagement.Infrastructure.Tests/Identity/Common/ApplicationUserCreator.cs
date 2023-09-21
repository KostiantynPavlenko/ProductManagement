using ProductManagement.Infrastructure.Identity.Models;

namespace ProductManagement.Infrastructure.Tests.Identity.Common;

public static class ApplicationUserCreator
{
    public static ApplicationUser CreateApplicationUser(string username, string email)
    {
        return new ApplicationUser
        {
            Email = email,
            UserName = username
        };
    }
}