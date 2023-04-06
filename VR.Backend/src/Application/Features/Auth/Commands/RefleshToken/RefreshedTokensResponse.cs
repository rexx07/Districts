using Application.Dtos;
using Domain.Entities;
using Infrastructure.Security.JWT;

namespace Application.Features.Auth.Commands.RefleshToken;

public class RefreshedTokensResponse : IDto
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
}