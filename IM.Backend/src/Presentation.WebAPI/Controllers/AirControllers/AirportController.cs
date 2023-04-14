using Microsoft.AspNetCore.Mvc;
using Modules.AirTransport.Commands;
using Modules.AirTransport.Dtos;

namespace Presentation.WebAPI.Controllers.AirControllers;

public class AirportController: BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateAirport([FromBody] CreateAirportCommand request,
                                                   CancellationToken cancellationToken)
    {
        AirportResponseDto result = await Mediator.Send(request, cancellationToken);

        return Created(uri: "", result);
    }
}