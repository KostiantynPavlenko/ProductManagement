using Extensions.Web.Results;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Identity.DTO;

namespace ProductManagement.Application.Identity.Interfaces;

public interface ILoginService
{
    Task<Result<ApplicationUserDto>> Login(LoginUserDto loginUser);
}