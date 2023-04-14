using AutoMapper;
using Core.Domain.Entities.Security;
using Core.Infrastructure.Persistence.Paging;
using Modules.BaseApplication.Features.Users.Commands.Create;
using Modules.BaseApplication.Features.Users.Commands.Delete;
using Modules.BaseApplication.Features.Users.Commands.Update;
using Modules.BaseApplication.Features.Users.Commands.UpdateFromAuth;
using Modules.BaseApplication.Features.Users.Queries.GetById;
using Modules.BaseApplication.Features.Users.Queries.GetList;

namespace Modules.BaseApplication.Features.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, CreateUserCommand>().ReverseMap();
        CreateMap<User, CreatedUserResponse>().ReverseMap();
        CreateMap<User, UpdateUserCommand>().ReverseMap();
        CreateMap<User, UpdatedUserResponse>().ReverseMap();
        CreateMap<User, UpdateUserFromAuthCommand>().ReverseMap();
        CreateMap<User, UpdatedUserFromAuthResponse>().ReverseMap();
        CreateMap<User, DeleteUserCommand>().ReverseMap();
        CreateMap<User, DeletedUserResponse>().ReverseMap();
        CreateMap<User, GetByIdUserResponse>().ReverseMap();
        CreateMap<User, GetListUserListItemDto>().ReverseMap();
        CreateMap<IPaginate<User>, GetListResponse<GetListUserListItemDto>>().ReverseMap();
    }
}