using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Requests;
using Microsoft.AspNetCore.Mvc;
using Modules.BaseApplication.Features.Fuels.Commands.Create;
using Modules.BaseApplication.Features.Fuels.Commands.Delete;
using Modules.BaseApplication.Features.Fuels.Commands.Update;
using Modules.BaseApplication.Features.Fuels.Queries.GetById;
using Modules.BaseApplication.Features.Fuels.Queries.GetList;

namespace Presentation.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FuelsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdFuelQuery getByIdFuelQuery)
    {
        GetByIdFuelResponse result = await Mediator.Send(getByIdFuelQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListFuelQuery getListFuelQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListFuelListItemDto> result = await Mediator.Send(getListFuelQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateFuelCommand createFuelCommand)
    {
        CreatedFuelResponse result = await Mediator.Send(createFuelCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateFuelCommand updateFuelCommand)
    {
        UpdatedFuelResponse result = await Mediator.Send(updateFuelCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteFuelCommand deleteFuelCommand)
    {
        DeletedFuelResponse result = await Mediator.Send(deleteFuelCommand);
        return Ok(result);
    }
}