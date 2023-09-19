﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Identity.Commands.LoginCommand;
using ProductManagement.Application.Identity.Commands.RegisterCommand;
using ProductManagement.Application.Identity.DTO;
using ProductManagement.Application.Identity.Interfaces;
using ProductManagement.Infrastructure.Identity.Models;

namespace ProductManagement.API.Controllers;

[AllowAnonymous]
public class AccountController : BaseApiController
{
    [HttpPost]
    [Route("/register")]
    public async Task<ActionResult<ApplicationUserDto>> Register([FromBody] RegisterUserDto request)
    {
       return HandleResult(await Mediator.Send(new RegisterCommand{RegisterUser = request}));
    }
    
    [HttpPost]
    [Route("/login")]
    public async Task<ActionResult<ApplicationUserDto>> Login([FromBody] LoginUserDto loginUser)
    {
        return HandleResult(await Mediator.Send(new LoginCommand{LoginUser = loginUser}));
    }
}