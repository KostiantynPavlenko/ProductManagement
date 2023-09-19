using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Identity.DTO;

namespace ProductManagement.Application.Identity.Interfaces;

public interface IRegisterService
{
    Task<Result<ApplicationUserDto>> Register(RegisterUserDto registerUser);
}