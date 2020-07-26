using DragonLibrary_.Models;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DragonLibrary_.Services
{
    public class JWTService : IJWTService
    {
        private readonly ILogger _logger;

        public JWTService(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public string GetHeroNameFromToken(string token)
        {
            var claims = GetClaims(token);

            _logger.Debug("JWTService.GetHeroNameFromToken: token:{@token}, name:{@name}.",
                token,
                claims.Identity.Name);

            return claims.Identity.Name;
        }

        public string GetToken(string heroName)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, heroName)
                };

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    notBefore: now,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(
                        AuthOptions.GetSymmetricSecurityKey(),
                        SecurityAlgorithms.HmacSha256));
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            _logger.Debug("JWTService.GetToken: Token for hero {@hero} created.", token);

            return token;
        }

        private ClaimsPrincipal GetClaims(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);

            return claims;
        }
    }
}
