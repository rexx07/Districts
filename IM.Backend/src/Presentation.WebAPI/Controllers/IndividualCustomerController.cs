﻿using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Requests;
using Microsoft.AspNetCore.Mvc;
using Modules.BaseApplication.Features.IndividualCustomers.Commands.Create;
using Modules.BaseApplication.Features.IndividualCustomers.Commands.Delete;
using Modules.BaseApplication.Features.IndividualCustomers.Commands.Update;
using Modules.BaseApplication.Features.IndividualCustomers.Queries.GetByCustomerId;
using Modules.BaseApplication.Features.IndividualCustomers.Queries.GetById;
using Modules.BaseApplication.Features.IndividualCustomers.Queries.GetList;

namespace Presentation.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IndividualCustomersController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdIndividualCustomerQuery getByIdIndividualCustomerQuery)
    {
        GetByIdIndividualCustomerResponse result = await Mediator.Send(getByIdIndividualCustomerQuery);
        return Ok(result);
    }

    [HttpGet("ByCustomerId/{CustomerId}")]
    public async Task<IActionResult> GetById(
        [FromRoute] GetByCustomerIdIndividualCustomerQuery getByCustomerIdIndividualCustomerQuery)
    {
        GetByCustomerIdIndividualCustomerResponse result = await Mediator.Send(getByCustomerIdIndividualCustomerQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListIndividualCustomerQuery getListIndividualCustomerQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListIndividualCustomerListItemDto> result =
            await Mediator.Send(getListIndividualCustomerQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateIndividualCustomerCommand createIndividualCustomerCommand)
    {
        CreatedIndividualCustomerResponse result = await Mediator.Send(createIndividualCustomerCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateIndividualCustomerCommand updateIndividualCustomerCommand)
    {
        UpdatedIndividualCustomerResponse result = await Mediator.Send(updateIndividualCustomerCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteIndividualCustomerCommand deleteIndividualCustomerCommand)
    {
        DeletedIndividualCustomerResponse result = await Mediator.Send(deleteIndividualCustomerCommand);
        return Ok(result);
    }
}