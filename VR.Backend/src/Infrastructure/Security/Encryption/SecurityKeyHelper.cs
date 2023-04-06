﻿using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security.Encryption;

public class SecurityKeyHelper
{
    public static SecurityKey CreateSecurityKey(string securityKey) =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
}