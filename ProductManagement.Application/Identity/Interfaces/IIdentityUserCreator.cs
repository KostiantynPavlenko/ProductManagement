using ProductManagement.Application.Identity.DTO;

namespace ProductManagement.Application.Identity.Interfaces;

public interface IIdentityUserCreator
{
    ApplicationUserDto CreateIdentityUser(string username, string email);
}