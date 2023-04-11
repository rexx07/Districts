using Core.Domain.Enums;
using Core.Infrastructure.Dtos;

namespace Application.Features.RentalBranches.Commands.Update;

public class UpdatedRentalBranchResponse : IDto
{
    public int Id { get; set; }
    public City City { get; set; }
}