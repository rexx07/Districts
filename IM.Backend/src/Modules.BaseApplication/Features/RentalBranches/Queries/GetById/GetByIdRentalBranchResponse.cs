using Core.Domain.Enums;
using Core.Infrastructure.Dtos;

namespace Application.Features.RentalBranches.Queries.GetById;

public class GetByIdRentalBranchResponse : IDto
{
    public int Id { get; set; }
    public City City { get; set; }
}