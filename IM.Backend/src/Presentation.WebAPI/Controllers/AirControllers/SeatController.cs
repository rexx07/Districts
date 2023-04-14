using Microsoft.AspNetCore.Mvc;
using Modules.AirTransport.Commands;
using Modules.AirTransport.Dtos;
using Modules.AirTransport.Queries;

namespace Presentation.WebAPI.Controllers.AirControllers;

public class SeatController: BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAvailableSeats([FromRoute] GetAvailableSeatsQuery request,
                                                       CancellationToken cancellationToken)
    {
        IEnumerable<SeatResponseDto> result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSeat([FromBody] CreateSeatCommand request,
                                               CancellationToken cancellationToken)
    {
        SeatResponseDto result = await Mediator.Send(request, cancellationToken);

        return Created(uri: "", result);
    }

    [HttpPost]
    public async Task<IActionResult> ReserveSeat([FromBody] ReserveSeatCommand request,
                                                 CancellationToken cancellationToken)
    {
        SeatResponseDto result = await Mediator.Send(request, cancellationToken);

        return Created(uri: "", result);
    }
}