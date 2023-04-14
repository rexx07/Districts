using Microsoft.AspNetCore.Mvc;
using Modules.AirTransport.Commands;
using Modules.AirTransport.Dtos;

namespace Presentation.WebAPI.Controllers.AirControllers;

public class AircraftController: BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateAircraft([FromBody] CreateAircraftCommand request,
                                                    CancellationToken cancellationToken)
    {
        AircraftResponseDto result = await Mediator.Send(request, cancellationToken);

        return Created(uri: "", result);
    }
}