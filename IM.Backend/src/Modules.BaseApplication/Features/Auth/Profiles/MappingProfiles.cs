using AutoMapper;
using Core.Domain.Entities.Security;
using Modules.BaseApplication.Features.Auth.Commands.RevokeToken;

namespace Modules.BaseApplication.Features.Auth.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RefreshToken, RevokedTokenResponse>().ReverseMap();
    }
}