using Microsoft.Extensions.Options;
using Security.Transversal.Auth.Entity;
using Security.Transversal.Auth.Entity.jwt;
using Security.Transversal.Auth.Enum;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Security.Transversal.Auth.Jwt
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtIssuerOptions;
        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtIssuerOptions = jwtOptions.Value;
        }
        public UserToken GetJwt(User appUser)
        {
            var claimsIdentity = GenerateClaims(appUser);
            return GenerateJwt(claimsIdentity);
        }

        private ClaimsIdentity GenerateClaims(User appUser)
        {
            return new ClaimsIdentity(new GenericIdentity("tokenName", "Token"), new[]
            {
                new Claim(ClaimType.Id, appUser.Id.ToString()),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, appUser.Id.ToString()),
                new Claim(ClaimType.Name, appUser.Name),
                new Claim(ClaimType.Username, appUser.UserName),
                new Claim(ClaimType.RolId, appUser.RolId.ToString()),
                new Claim(ClaimType.Platform, appUser.Platform),
                new Claim(ClaimType.CompanyId, appUser.CompanyId.ToString()),
                new Claim(ClaimType.AppId, appUser.AppId.ToString())
            });
        }

        private UserToken GenerateJwt(ClaimsIdentity identity)
        {
            var response = new UserToken
            {
                Token = GenerateEncodedToken(identity),
                Expiration = _jwtIssuerOptions.Expiration,
                ExpireIn = _jwtIssuerOptions.ValidFor.TotalMinutes
            };

            return response;
        }

        private string GenerateEncodedToken(ClaimsIdentity claimsIdentity)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtIssuerOptions.IssuedAt).ToString(),
                    ClaimValueTypes.Integer64)
            };
            //agregación de claims publicos
            foreach (var claim in claimsIdentity.Claims)
            {
                Array.Resize(ref claims, claims.Length + 1);
                claims[^1] = claim;
            }

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtIssuerOptions.Issuer,
                audience: _jwtIssuerOptions.Audience,
                claims: claims,
                notBefore: _jwtIssuerOptions.NotBefore,
                expires: DateTime.Now.AddMinutes(30),//_jwtIssuerOptions.Expiration,
                signingCredentials: _jwtIssuerOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
        }
    }
}
