using AutoMapper;
using Core.Domain.Entities.Security;
using Core.Infrastructure.Persistence.Paging;
using Modules.BaseApplication.Features.UserOperationClaims.Commands.Create;
using Modules.BaseApplication.Features.UserOperationClaims.Commands.Delete;
using Modules.BaseApplication.Features.UserOperationClaims.Commands.Update;
using Modules.BaseApplication.Features.UserOperationClaims.Queries.GetById;
using Modules.BaseApplication.Features.UserOperationClaims.Queries.GetList;

namespace Modules.BaseApplication.Features.UserOperationClaims.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UserOperationClaim, CreateUserOperationClaimCommand>().ReverseMap();
        CreateMap<UserOperationClaim, CreatedUserOperationClaimResponse>().ReverseMap();
        CreateMap<UserOperationClaim, UpdateUserOperationClaimCommand>().ReverseMap();
        CreateMap<UserOperationClaim, UpdatedUserOperationClaimResponse>().ReverseMap();
        CreateMap<UserOperationClaim, DeleteUserOperationClaimCommand>().ReverseMap();
        CreateMap<UserOperationClaim, DeletedUserOperationClaimResponse>().ReverseMap();
        CreateMap<UserOperationClaim, GetByIdUserOperationClaimResponse>().ReverseMap();
        CreateMap<UserOperationClaim, GetListUserOperationClaimListItemDto>().ReverseMap();
        CreateMap<IPaginate<UserOperationClaim>, GetListResponse<GetListUserOperationClaimListItemDto>>().ReverseMap();
    }
}