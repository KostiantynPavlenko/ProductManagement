using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ProductManagement.Infrastructure.Identity.Interfaces;

namespace ProductManagement.Infrastructure.Identity.Services;

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _context;

    public UserAccessor(IHttpContextAccessor context)
    {
        _context = context;
    }
    
    public string GetCurrentUserName()
    {
        return _context.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
    }
}