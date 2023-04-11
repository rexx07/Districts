using Core.Infrastructure.Dtos;

namespace Application.Features.OperationClaims.Commands.Delete;

public class DeletedOperationClaimResponse : IDto
{
    public int Id { get; set; }
}