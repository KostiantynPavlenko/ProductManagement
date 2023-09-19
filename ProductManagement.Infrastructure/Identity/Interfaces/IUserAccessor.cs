namespace ProductManagement.Infrastructure.Identity.Interfaces;

public interface IUserAccessor
{
    string GetCurrentUserName();
}