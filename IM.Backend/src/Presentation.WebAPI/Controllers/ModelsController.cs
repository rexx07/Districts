﻿using Core.Infrastructure.Persistence.Dynamic;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Requests;
using Microsoft.AspNetCore.Mvc;
using Modules.BaseApplication.Features.Models.Commands.Create;
using Modules.BaseApplication.Features.Models.Commands.Delete;
using Modules.BaseApplication.Features.Models.Commands.Update;
using Modules.BaseApplication.Features.Models.Queries.GetById;
using Modules.BaseApplication.Features.Models.Queries.GetList;
using Modules.BaseApplication.Features.Models.Queries.GetListByDynamic;

namespace Presentation.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
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