﻿namespace Core.Infrastructure.Security.EmailAuthenticator;

public interface IEmailAuthenticatorHelper
{
    public Task<string> CreateEmailActivationKey();
    public Task<string> CreateEmailActivationCode();
}