using Core.Infrastructure.Dtos;

namespace Application.Features.UserOperationClaims.Commands.Delete;

public class DeletedUserOperationClaimResponse : IDto
{
    public int Id { get; set; }
}