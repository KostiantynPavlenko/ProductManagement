using MediatR;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Identity.DTO;

namespace ProductManagement.Application.Identity.Commands.RegisterCommand;

public class RegisterCommand : IRequest<Result<ApplicationUserDto>>
{
    public RegisterUserDto RegisterUser { get; set; }
}