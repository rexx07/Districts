using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Requests;
using Microsoft.AspNetCore.Mvc;
using Modules.BaseApplication.Features.Colors.Commands.Create;
using Modules.BaseApplication.Features.Colors.Commands.Delete;
using Modules.BaseApplication.Features.Colors.Commands.Update;
using Modules.BaseApplication.Features.Colors.Queries.GetById;
using Modules.BaseApplication.Features.Colors.Queries.GetList;

namespace Presentation.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ColorsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdColorQuery getByIdColorQuery)
    {
        GetByIdColorResponse result = await Mediator.Send(getByIdColorQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListColorQuery getListColorQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListColorListItemDto> result = await Mediator.Send(getListColorQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateColorCommand createColorCommand)
    {
        CreatedColorResponse result = await Mediator.Send(createColorCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateColorCommand updateColorCommand)
    {
        UpdatedColorResponse result = await Mediator.Send(updateColorCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteColorCommand deleteColorCommand)
    {
        DeletedColorResponse result = await Mediator.Send(deleteColorCommand);
        return Ok(result);
    }
}