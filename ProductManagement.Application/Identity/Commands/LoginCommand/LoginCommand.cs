using System.Windows.Input;
using MediatR;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Identity.DTO;

namespace ProductManagement.Application.Identity.Commands.LoginCommand;

public class LoginCommand : IRequest<Result<ApplicationUserDto>>
{
     public LoginUserDto LoginUser { get; set; } 
}