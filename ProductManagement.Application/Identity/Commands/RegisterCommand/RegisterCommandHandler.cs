using Extensions.Web.Results;
using MediatR;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Identity.DTO;
using ProductManagement.Application.Identity.Interfaces;

namespace ProductManagement.Application.Identity.Commands.RegisterCommand;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<ApplicationUserDto>>
{
    private readonly IRegisterService _registerService;

    public RegisterCommandHandler(IRegisterService registerService)
    {
        _registerService = registerService;
    }
    
    public async Task<Result<ApplicationUserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _registerService.Register(request.RegisterUser);
    }
}