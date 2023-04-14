using Microsoft.AspNetCore.Mvc;
using Modules.PassengerAndCargo.Commands;
using Modules.PassengerAndCargo.Dtos;
using Modules.PassengerAndCargo.Queries;

namespace Presentation.WebAPI.Controllers.PassengerAndCargoControllers;

public class PassengerController: BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetPassengerById([FromRoute] GetPassengerQueryById query, CancellationToken cancellationToken)
    {
        PassengerResponseDto result = await Mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CompleteRegisterPassenger([FromBody] CompleteRegisterPassengerCommand command,
                                                               CancellationToken cancellationToken)
    {
        PassengerResponseDto result = await Mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}