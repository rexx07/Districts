﻿using Core.Infrastructure.Security.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator? Mediator => 
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    protected string? getIpAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
    }

    protected int getUserIdFromRequest() //todo authentication behavior?
    {
        int userId = HttpContext.User.GetUserId();
        return userId;
    }
}