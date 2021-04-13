using Application.Interfaces;
using AutoMapper.Configuration;
using Domain.Identity;
using Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Security
{
    public class JwtTokensData
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string RefreshTokenSerial { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }

    public class TokenFactoryService : ITokenFactoryService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly ISecurityService _securityService;
        private BearerTokensOptions _bearerTokensOptions;
        public TokenFactoryService(IConfiguration config, IOptions<SiteSettings> siteSettings, ISecurityService securityService)
        {
            _bearerTokensOptions = siteSettings.Value.BearerTokensOptions;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_bearerTokensOptions.Key));
            _securityService = securityService;
        }


        public string GetRefreshTokenSerial(string refreshTokenValue)
        {
            if (string.IsNullOrWhiteSpace(refreshTokenValue))
            {
                return null;
            }

            ClaimsPrincipal decodedRefreshTokenPrincipal = null;
            try
            {
                decodedRefreshTokenPrincipal = new JwtSecurityTokenHandler().ValidateToken(
                    refreshTokenValue,
                    new TokenValidationParameters
                    {
                        RequireExpirationTime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_bearerTokensOptions.Key)),
                        ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                        ValidateLifetime = true, // validate the expiration
                        ClockSkew = TimeSpan.Zero // tolerance for the expiration date
                    },
                    out _
                );
            }
            catch (Exception)
            {

            }

            return decodedRefreshTokenPrincipal?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
        }
        public JwtTokensData CreateJwtTokens(AppUser user)
        {
            var (accessToken, claims) = CreateAccessToken(user);
            var (refreshTokenValue, refreshTokenSerial) = CreateRefreshToken();
            return new JwtTokensData
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue,
                RefreshTokenSerial = refreshTokenSerial,
                Claims = claims
            };
        }


        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            // generate signing credentials
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        private (string AccessToken, IEnumerable<Claim> Claims) CreateAccessToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                // Unique Id for all Jwt tokes
                new Claim(JwtRegisteredClaimNames.Jti, _securityService.CreateCryptographicallySecureGuid().ToString(), ClaimValueTypes.String, _bearerTokensOptions.Issuer),
                // Issuer
                new Claim(JwtRegisteredClaimNames.Iss, _bearerTokensOptions.Issuer, ClaimValueTypes.String, _bearerTokensOptions.Issuer),
                // Issued at
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _bearerTokensOptions.Issuer),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String, _bearerTokensOptions.Issuer),
                new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String, _bearerTokensOptions.Issuer),
                new Claim("DisplayName", user.DisplayName, ClaimValueTypes.String, _bearerTokensOptions.Issuer),
                // to invalidate the cookie
                new Claim(ClaimTypes.SerialNumber, user.SerialNumber, ClaimValueTypes.String, _bearerTokensOptions.Issuer),
                // custom data
                new Claim(ClaimTypes.UserData, user.Id.ToString(), ClaimValueTypes.String, _bearerTokensOptions.Issuer)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_bearerTokensOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _bearerTokensOptions.Issuer,
                audience: _bearerTokensOptions.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_bearerTokensOptions.AccessTokenExpirationMinutes),
                signingCredentials: creds);
            return (new JwtSecurityTokenHandler().WriteToken(token), claims);
        }


        private (string RefreshTokenValue, string RefreshTokenSerial) CreateRefreshToken()
        {
            var refreshTokenSerial = _securityService.CreateCryptographicallySecureGuid().ToString().Replace("-", "");

            var claims = new List<Claim>
            {
                // Unique Id for all Jwt tokes
                new Claim(JwtRegisteredClaimNames.Jti, _securityService.CreateCryptographicallySecureGuid().ToString(), ClaimValueTypes.String, _bearerTokensOptions.Issuer),
                // Issuer
                new Claim(JwtRegisteredClaimNames.Iss, _bearerTokensOptions.Issuer, ClaimValueTypes.String, _bearerTokensOptions.Issuer),
                // Issued at
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _bearerTokensOptions.Issuer),
                // for invalidation
                new Claim(ClaimTypes.SerialNumber, refreshTokenSerial, ClaimValueTypes.String, _bearerTokensOptions.Issuer)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_bearerTokensOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _bearerTokensOptions.Issuer,
                audience: _bearerTokensOptions.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_bearerTokensOptions.RefreshTokenExpirationMinutes),
                signingCredentials: creds);
            var refreshTokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return (refreshTokenValue, refreshTokenSerial);
        }

    }
}
