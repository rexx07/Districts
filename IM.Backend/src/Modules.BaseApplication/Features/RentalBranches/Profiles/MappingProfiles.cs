using AutoMapper;
using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Paging;
using Modules.BaseApplication.Features.RentalBranches.Commands.Create;
using Modules.BaseApplication.Features.RentalBranches.Commands.Delete;
using Modules.BaseApplication.Features.RentalBranches.Commands.Update;
using Modules.BaseApplication.Features.RentalBranches.Queries.GetById;
using Modules.BaseApplication.Features.RentalBranches.Queries.GetList;

namespace Modules.BaseApplication.Features.RentalBranches.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RentalBranch, CreateRentalBranchCommand>().ReverseMap();
        CreateMap<RentalBranch, CreatedRentalBranchResponse>().ReverseMap();
        CreateMap<RentalBranch, UpdateRentalBranchCommand>().ReverseMap();
        CreateMap<RentalBranch, UpdatedRentalBranchResponse>().ReverseMap();
        CreateMap<RentalBranch, DeleteRentalBranchCommand>().ReverseMap();
        CreateMap<RentalBranch, DeletedRentalBranchResponse>().ReverseMap();
        CreateMap<RentalBranch, GetByIdRentalBranchResponse>().ReverseMap();
        CreateMap<RentalBranch, GetListRentalBranchListItemDto>().ReverseMap();
        CreateMap<IPaginate<RentalBranch>, GetListResponse<GetListRentalBranchListItemDto>>().ReverseMap();
    }
}