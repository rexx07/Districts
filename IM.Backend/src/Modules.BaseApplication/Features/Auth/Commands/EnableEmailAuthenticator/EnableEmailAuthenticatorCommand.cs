﻿using System.Web;
using Core.Domain.Entities.Security;
using Core.Domain.Enums;
using Core.Infrastructure.Mailing;
using MediatR;
using MimeKit;
using Modules.BaseApplication.Features.Auth.Rules;
using Modules.BaseApplication.Services.AuthenticatorService;
using Modules.BaseApplication.Services.UserService;

namespace Modules.BaseApplication.Features.Auth.Commands.EnableEmailAuthenticator;

public class EnableEmailAuthenticatorCommand : IRequest
{
    public int UserId { get; set; }
    public string VerifyEmailUrlPrefix { get; set; }

    public class EnableEmailAuthenticatorCommandHandler : IRequestHandler<EnableEmailAuthenticatorCommand>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
        private readonly IMailService _mailService;
        private readonly IUserService _userService;

        public EnableEmailAuthenticatorCommandHandler(
            IUserService userService,
            IEmailAuthenticatorRepository emailAuthenticatorRepository,
            IMailService mailService,
            AuthBusinessRules authBusinessRules,
            IAuthenticatorService authenticatorService
        )
        {
            _userService = userService;
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
            _mailService = mailService;
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
        }

        public async Task Handle(EnableEmailAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetById(request.UserId);
            await _authBusinessRules.UserShouldBeExists(user);
            await _authBusinessRules.UserShouldNotBeHaveAuthenticator(user);

            user.AuthenticatorType = AuthenticatorType.Email;
            await _userService.Update(user);

            EmailAuthenticator emailAuthenticator = await _authenticatorService.CreateEmailAuthenticator(user);
            EmailAuthenticator addedEmailAuthenticator =
                await _emailAuthenticatorRepository.AddAsync(emailAuthenticator);

            var toEmailList = new List<MailboxAddress> { new(name: $"{user.FirstName} {user.LastName}", user.Email) };

            _mailService.SendMail(
                new Mail
                {
                    ToList = toEmailList,
                    Subject = "Verify Your Email - RentACar",
                    TextBody =
                        $"Click on the link to verify your email: {request.VerifyEmailUrlPrefix}?ActivationKey={HttpUtility.UrlEncode(addedEmailAuthenticator.ActivationKey)}"
                }
            );
        }
    }
}