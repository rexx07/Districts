using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Requests;
using Microsoft.AspNetCore.Mvc;
using Modules.BaseApplication.Features.FindeksCreditRates.Commands.Create;
using Modules.BaseApplication.Features.FindeksCreditRates.Commands.Delete;
using Modules.BaseApplication.Features.FindeksCreditRates.Commands.Update;
using Modules.BaseApplication.Features.FindeksCreditRates.Commands.UpdateByUserIdFromService;
using Modules.BaseApplication.Features.FindeksCreditRates.Commands.UpdateFromService;
using Modules.BaseApplication.Features.FindeksCreditRates.Queries.GetByCustomerIdFindeksCreditRate;
using Modules.BaseApplication.Features.FindeksCreditRates.Queries.GetByIdFindeksCreditRate;
using Modules.BaseApplication.Features.FindeksCreditRates.Queries.GetListFindeksCreditRate;
using Presentation.WebAPI.Controllers.Dtos;

namespace Presentation.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FindeksCreditRatesController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdFindeksCreditRateQuery getByIdFindeksCreditRateQuery)
    {
        GetByIdFindeksCreditRateResponse result = await Mediator.Send(getByIdFindeksCreditRateQuery);
        return Ok(result);
    }

    [HttpGet("ByCustomerId/{CustomerId}")]
    public async Task<IActionResult> GetById(
        [FromRoute] GetByCustomerIdFindeksCreditRateQuery getByCustomerIdFindeksCreditRateQuery)
    {
        GetByCustomerIdFindeksCreditRateResponse result = await Mediator.Send(getByCustomerIdFindeksCreditRateQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListFindeksCreditRateQuery getListFindeksCreditRateQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListFindeksCreditRateListItemDto>
            result = await Mediator.Send(getListFindeksCreditRateQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateFindeksCreditRateCommand createFindeksCreditRateCommand)
    {
        CreatedFindeksCreditRateResponse result = await Mediator.Send(createFindeksCreditRateCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateFindeksCreditRateCommand updateFindeksCreditRateCommand)
    {
        UpdatedFindeksCreditRateResponse result = await Mediator.Send(updateFindeksCreditRateCommand);
        return Ok(result);
    }

    [HttpPut("FromService")]
    public async Task<IActionResult> UpdateFromService(
        [FromBody] UpdateFindeksCreditRateFromServiceCommand findeksCreditRateFromServiceCommand
    )
    {
        UpdateFindeksCreditRateFromServiceResponse result = await Mediator.Send(findeksCreditRateFromServiceCommand);
        return Ok(result);
    }

    [HttpPut("ByAuth/FromService")]
    public async Task<IActionResult> UpdateByAuthFromService(
        [FromBody] UpdateByAuthFromServiceRequestDto updateByAuthFromServiceRequestDto)
    {
        UpdateByUserIdFindeksCreditRateFromServiceCommand updateByUserIdFindeksCreditRateFromServiceCommand =
            new()
            {
                UserId = getUserIdFromRequest(), IdentityNumber = updateByAuthFromServiceRequestDto.IdentityNumber
            };

        UpdateByUserIdFindeksCreditRateFromServiceResponse result =
            await Mediator.Send(updateByUserIdFindeksCreditRateFromServiceCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteFindeksCreditRateCommand deleteFindeksCreditRateCommand)
    {
        DeletedFindeksCreditRateResponse result = await Mediator.Send(deleteFindeksCreditRateCommand);
        return Ok(result);
    }
}