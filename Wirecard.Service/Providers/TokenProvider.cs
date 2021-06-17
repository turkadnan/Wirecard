using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Configurations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Wirecard.Core.Configuration;
using Wirecard.Core.Dtos;
using Wirecard.Core.Models;
using Wirecard.Core.Providers;
using Wirecard.Business.Services;

namespace Wirecard.Business.Providers
{

    public class JWTTokenProvider : ITokenProvider
    {

        private IEnumerable<Claim> GetClaims(UserApp userApp, List<string> audiences)
        {
            var claimList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,userApp.Id),
                //new Claim("Email",userApp.Email) // Buda bir kullanım
                new Claim(JwtRegisteredClaimNames.Email,userApp.Email),
                new Claim(ClaimTypes.Name,userApp.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            //audiences ları JwtRegisteredClaimNames.Aud ile merge ediliyor
            claimList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            return claimList;
        }
        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>();
            claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            //Token Kimliği
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString()));

            return claims;
        }

        public TokenDto GetToken(UserApp useApp,string refreshToken, CustomTokenOption tokenOption)
        {

            var accessTokenExpration = DateTime.Now.AddMinutes(tokenOption.AccessTokenExpration);
            var refreshTokenExpration = DateTime.Now.AddMinutes(tokenOption.RefresfTokenExpration);

            var securityKey = SignService.GetSymmetricSecurityKey(tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken =
                new JwtSecurityToken(
                    issuer: tokenOption.Issuer,
                    expires: accessTokenExpration,
                    notBefore: DateTime.Now,
                    claims: GetClaims(useApp, tokenOption.Audience),
                    signingCredentials: signingCredentials
                    );


            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);


            var tokenDto = new TokenDto
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                AccessTokenExpiration = accessTokenExpration,
                RefreshTokenExpiration = refreshTokenExpration
            };

            return tokenDto;
        }

        public ClientTokenDto GetTokenByClient(Client client, CustomTokenOption tokenOption)
        {
            var accessTokenExpration = DateTime.Now.AddMinutes(tokenOption.AccessTokenExpration);

            var securityKey = SignService.GetSymmetricSecurityKey(tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken =
                new JwtSecurityToken(
                    issuer: tokenOption.Issuer,
                    expires: accessTokenExpration,
                    notBefore: DateTime.Now,
                    claims: GetClaimsByClient(client),
                    signingCredentials: signingCredentials
                    );


            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);

            var clientTokenDto = new ClientTokenDto
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpration
            };

            return clientTokenDto;
        }
    }

    public class NULLTokenProvider : ITokenProvider
    {
        public TokenDto GetToken(UserApp useApp, string refreshToken, CustomTokenOption tokenOption)
        {
            return new TokenDto();
        }

        public ClientTokenDto GetTokenByClient(Client client, CustomTokenOption tokenOption)
        {
            return new ClientTokenDto();
        }
    }
}
