using AutoMapper;
using Core.Domain.Entities.Security;
using Core.Infrastructure.Persistence.Paging;
using Modules.BaseApplication.Features.OperationClaims.Commands.Create;
using Modules.BaseApplication.Features.OperationClaims.Commands.Delete;
using Modules.BaseApplication.Features.OperationClaims.Commands.Update;
using Modules.BaseApplication.Features.OperationClaims.Queries.GetById;
using Modules.BaseApplication.Features.OperationClaims.Queries.GetList;

namespace Modules.BaseApplication.Features.OperationClaims.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<OperationClaim, CreateOperationClaimCommand>().ReverseMap();
        CreateMap<OperationClaim, CreatedOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim, UpdateOperationClaimCommand>().ReverseMap();
        CreateMap<OperationClaim, UpdatedOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim, DeleteOperationClaimCommand>().ReverseMap();
        CreateMap<OperationClaim, DeletedOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim, GetByIdOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim, GetListOperationClaimListItemDto>().ReverseMap();
        CreateMap<IPaginate<OperationClaim>, GetListResponse<GetListOperationClaimListItemDto>>().ReverseMap();
    }
}