using Microsoft.AspNetCore.Identity;

namespace ProductManagement.Infrastructure.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

