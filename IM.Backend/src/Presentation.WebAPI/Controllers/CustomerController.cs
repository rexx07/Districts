﻿using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Requests;
using Microsoft.AspNetCore.Mvc;
using Modules.BaseApplication.Features.Customers.Commands.Create;
using Modules.BaseApplication.Features.Customers.Commands.Delete;
using Modules.BaseApplication.Features.Customers.Commands.Update;
using Modules.BaseApplication.Features.Customers.Queries.GetById;
using Modules.BaseApplication.Features.Customers.Queries.GetByUserId;
using Modules.BaseApplication.Features.Customers.Queries.GetList;

namespace Presentation.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdCustomerQuery getByIdCustomerQuery)
    {
        GetByIdCustomerResponse result = await Mediator.Send(getByIdCustomerQuery);
        return Ok(result);
    }

    [HttpGet("ByAuth")]
    public async Task<IActionResult> GetByAuth()
    {
        GetByUserIdCustomerQuery getByUserIdCustomerQuery = new() { UserId = getUserIdFromRequest() };
        GetByUserIdCustomerResponse result = await Mediator.Send(getByUserIdCustomerQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCustomerQuery getListCustomerQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListCustomerListItemDto> result = await Mediator.Send(getListCustomerQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCustomerCommand createCustomerCommand)
    {
        CreatedCustomerResponse result = await Mediator.Send(createCustomerCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand updateCustomerCommand)
    {
        UpdatedCustomerResponse result = await Mediator.Send(updateCustomerCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteCustomerCommand deleteCustomerCommand)
    {
        DeletedCustomerResponse result = await Mediator.Send(deleteCustomerCommand);
        return Ok(result);
    }
}