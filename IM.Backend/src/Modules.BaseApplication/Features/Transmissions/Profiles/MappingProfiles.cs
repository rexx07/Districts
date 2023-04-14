using AutoMapper;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Paging;
using Modules.BaseApplication.Features.Transmissions.Commands.Create;
using Modules.BaseApplication.Features.Transmissions.Commands.Delete;
using Modules.BaseApplication.Features.Transmissions.Commands.Update;
using Modules.BaseApplication.Features.Transmissions.Queries.GetById;
using Modules.BaseApplication.Features.Transmissions.Queries.GetList;

namespace Modules.BaseApplication.Features.Transmissions.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Transmission, CreateTransmissionCommand>().ReverseMap();
        CreateMap<Transmission, CreatedTransmissionResponse>().ReverseMap();
        CreateMap<Transmission, UpdateTransmissionCommand>().ReverseMap();
        CreateMap<Transmission, UpdatedTransmissionResponse>().ReverseMap();
        CreateMap<Transmission, DeleteTransmissionCommand>().ReverseMap();
        CreateMap<Transmission, DeletedTransmissionResponse>().ReverseMap();
        CreateMap<Transmission, GetByIdTransmissionResponse>().ReverseMap();
        CreateMap<Transmission, GetListTransmissionListItemDto>().ReverseMap();
        CreateMap<IPaginate<Transmission>, GetListResponse<GetListTransmissionListItemDto>>().ReverseMap();
    }
}