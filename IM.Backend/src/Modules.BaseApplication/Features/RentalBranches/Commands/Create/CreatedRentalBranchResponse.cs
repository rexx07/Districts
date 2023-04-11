using Application.Dtos;
using Core.Domain.Enums;

namespace Application.Features.RentalBranches.Commands.Create;

public class CreatedRentalBranchResponse : IDto
{
    public int Id { get; set; }
    public City City { get; set; }
}