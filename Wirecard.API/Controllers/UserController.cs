using Microsoft.AspNetCore.Authorization;
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
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            var retVal = await _userService.CreateUserAsync(createUserDto);
            return CommonActionResult(retVal);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var retVal = await _userService.GetUserByNameAync(HttpContext.User.Identity.Name);
            return CommonActionResult(retVal);
        }
    }
}
