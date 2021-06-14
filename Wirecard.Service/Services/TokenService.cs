using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Configurations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Wirecard.Core.Configuration;
using Wirecard.Core.Dtos;
using Wirecard.Core.Models;
using Wirecard.Core.Providers;
using Wirecard.Core.Services;
using Wirecard.Business.Providers;

namespace Wirecard.Business.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOption _tokenOption;
        private ITokenProvider _tokenProvider;

        public TokenService(UserManager<UserApp> userManager, IOptions<CustomTokenOption> options)
        {
            _userManager = userManager;
            _tokenOption = options.Value;
        }

        private string CreateRefreshToken()
        {
            var byteNumber = new byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(byteNumber);
            return Convert.ToBase64String(byteNumber);
        }

        public TokenDto CreateToken(UserApp useApp)
        {
            _tokenProvider = new NULLTokenProvider(_tokenOption);            
            return _tokenProvider.GetToken(useApp, CreateRefreshToken());
        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            _tokenProvider = new JWTTokenProvider(_tokenOption);
            return _tokenProvider.GetTokenByClient(client);
        }
    }
}
