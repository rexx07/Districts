using Core.Domain.Enums;
using Core.Infrastructure.Dtos;

namespace Application.Features.RentalBranches.Commands.Create;

public class CreatedRentalBranchResponse : IDto
{
    public int Id { get; set; }
    public City City { get; set; }
}