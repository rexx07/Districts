﻿using Application.Features.Models.Commands.Create;
using Application.Features.Models.Commands.Delete;
using Application.Features.Models.Commands.Update;
using Application.Features.Models.Queries.GetById;
using Application.Features.Models.Queries.GetList;
using Application.Features.Models.Queries.GetListByDynamic;
using Application.Requests;
using Infrastructure.Persistence.Dynamic;
using Infrastructure.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class ModelsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdModelQuery getByIdModelQuery)
    {
        GetByIdModelResponse result = await Mediator.Send(getByIdModelQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListModelQuery getListModelQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListModelListItemDto> result = await Mediator.Send(getListModelQuery);
        return Ok(result);
    }

    [HttpPost("GetList/ByDynamic")]
    public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest,
                                                      [FromBody] DynamicQuery? dynamicQuery = null)
    {
        GetListByDynamicModelQuery getListModelByDynamicQuery =
            new() { PageRequest = pageRequest, DynamicQuery = dynamicQuery };
        GetListResponse<GetListByDynamicModelListItemDto> result = await Mediator.Send(getListModelByDynamicQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateModelCommand createModelCommand)
    {
        CreatedModelResponse result = await Mediator.Send(createModelCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateModelCommand updateModelCommand)
    {
        UpdatedModelResponse result = await Mediator.Send(updateModelCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteModelCommand deleteModelCommand)
    {
        DeletedModelResponse result = await Mediator.Send(deleteModelCommand);
        return Ok(result);
    }
}