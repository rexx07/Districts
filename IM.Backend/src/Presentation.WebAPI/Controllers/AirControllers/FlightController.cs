using Microsoft.AspNetCore.Mvc;
using Modules.AirTransport.Commands;
using Modules.AirTransport.Dtos;
using Modules.AirTransport.Queries;

namespace Presentation.WebAPI.Controllers.AirControllers;

public class FlightController: BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetFlight([FromRoute] GetFlightByIdQuery request,
                                               CancellationToken cancellationToken)
    {
        FlightResponseDto result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAvailableFlights([FromQuery] GetAvailableFlightsQuery request,
                                                         CancellationToken cancellationToken)
    {
        IEnumerable<FlightResponseDto> result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFlight([FromBody] CreateFlightCommand request, CancellationToken cancellationToken)
    {
        FlightResponseDto result = await Mediator.Send(request, cancellationToken);
        
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFlight([FromBody] UpdateFlightCommand request,
                                                  CancellationToken cancellationToken)
    {
        FlightResponseDto result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteFlight([FromBody] DeleteFlightCommand request, CancellationToken cancellationToken)
    {
        FlightResponseDto result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}