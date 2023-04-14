using Core.Domain.Entities.Security;
using Core.Infrastructure.Security.JWT;
using MediatR;
using Modules.BaseApplication.Features.Auth.Rules;
using Modules.BaseApplication.Services.AuthService;
using Modules.BaseApplication.Services.UserService;

namespace Modules.BaseApplication.Features.Auth.Commands.RefleshToken;

public class RefreshTokenCommand : IRequest<RefreshedTokensResponse>
{
    public string? RefleshToken { get; set; }
    public string? IPAddress { get; set; }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshedTokensResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public RefreshTokenCommandHandler(IAuthService authService, IUserService userService,
                                          AuthBusinessRules authBusinessRules)
        {
            _authService = authService;
            _userService = userService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<RefreshedTokensResponse> Handle(RefreshTokenCommand request,
                                                          CancellationToken cancellationToken)
        {
            RefreshToken? refreshToken = await _authService.GetRefreshTokenByToken(request.RefleshToken);
            await _authBusinessRules.RefreshTokenShouldBeExists(refreshToken);

            if (refreshToken.Revoked != null)
                await _authService.RevokeDescendantRefreshTokens(
                    refreshToken,
                    request.IPAddress,
                    reason: $"Attempted reuse of revoked ancestor token: {refreshToken.Token}"
                );
            await _authBusinessRules.RefreshTokenShouldBeActive(refreshToken);

            User user = await _userService.GetById(refreshToken.UserId);

            RefreshToken newRefreshToken = await _authService.RotateRefreshToken(user, refreshToken, request.IPAddress);
            RefreshToken addedRefreshToken = await _authService.AddRefreshToken(newRefreshToken);

            await _authService.DeleteOldRefreshTokens(refreshToken.UserId);

            AccessToken createdAccessToken = await _authService.CreateAccessToken(user);

            RefreshedTokensResponse refreshedTokensResponse = new()
                { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
            return refreshedTokensResponse;
        }
    }
}