using Microsoft.IdentityModel.Tokens;
using System;

namespace SimpleCrm.WebApi.Auth
{
    public class JwtIssuerOptions
    {
        string Issue { get; set; }
        string Subject { get; set; }
        string Audience { get; set; }
        DateTime Expiration { get; set; }
        DateTime NotBefore { get; set; }
        DateTime IssuedAt { get; set; }
        SigningCredentials SigningCredentials { get; set; }
    }
}
