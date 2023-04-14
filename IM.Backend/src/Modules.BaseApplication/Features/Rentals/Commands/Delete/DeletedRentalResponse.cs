using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Rentals.Commands.Delete;

public class DeletedRentalResponse : IDto
{
    public int Id { get; set; }
}