using MediatR;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Identity.DTO;
using ProductManagement.Application.Identity.Interfaces;

namespace ProductManagement.Application.Identity.Commands.LoginCommand;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<ApplicationUserDto>>
{
    private readonly ILoginService _loginService;

    public LoginCommandHandler(ILoginService loginService)
    {
        _loginService = loginService;
    }
    
    public async Task<Result<ApplicationUserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _loginService.Login(request.LoginUser);
    }
}