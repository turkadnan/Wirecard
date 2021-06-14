
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wirecard.Core.Dtos;
using Wirecard.Core.Services;

namespace Wirecard.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var retVal = await _authenticationService.CreateTokenAsync(loginDto);
            return CommonActionResult(retVal);
        }

        [HttpPost]
        public IActionResult CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var retVal =  _authenticationService.CreateTokenByClient(clientLoginDto);
            return CommonActionResult(retVal);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var retVal = await _authenticationService.RevokeRefreshTokenAsync(refreshTokenDto.RefreshToken);
            return CommonActionResult(retVal);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var retVal = await _authenticationService.CreateTokenByRefreshTokenAsync(refreshTokenDto.RefreshToken);
            return CommonActionResult(retVal);
        }
    }
}
