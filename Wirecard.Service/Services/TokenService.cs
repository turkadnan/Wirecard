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
        private readonly ITokenProvider _tokenProvider;

        public TokenService(UserManager<UserApp> userManager, IOptions<CustomTokenOption> options, ITokenProvider tokenProvider)
        {
            _userManager = userManager;
            _tokenOption = options.Value;
            _tokenProvider = tokenProvider;
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
            return _tokenProvider.GetToken(useApp, CreateRefreshToken(), _tokenOption);
        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            return _tokenProvider.GetTokenByClient(client, _tokenOption);
        }
    }
}
