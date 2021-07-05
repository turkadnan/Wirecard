using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wirecard.Business.Commands;
using Wirecard.Business.Services;
using Wirecard.CommandFramework;
using Wirecard.Core.Dtos;
using Wirecard.Core.Services;

namespace Wirecard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        private readonly ICommandLogger _commandLogger;

        private readonly ICompositionRoot _compositionRoot;

        public UserController(IUserService userService, ICommandLogger commandLogger, ICompositionRoot compositionRoot)
        {
            _userService = userService;

            _commandLogger = commandLogger;

            _compositionRoot = compositionRoot;
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


       
        [HttpGet]
        [Route("GetUserByName")]
        public async Task<IActionResult> GetUserByName(string userName)
        {

            GetUserCommand cmd = new GetUserCommand();
            cmd.UserName = userName;


            using (var scope = _compositionRoot.BeginLifetimeScope())
            {

                using (var handler = _compositionRoot.Resolve<ICommandHandler<GetUserCommand, GetUserResult>>(scope))
                {
                    var result =  await handler.HandleAsync(cmd);

                    return Ok(result);
                }
            }

          

            //GetUserCommandHandler handler = new GetUserCommandHandler(_commandLogger, _userService);
            //var result = await handler.HandleAsync(cmd);

            //return Ok(result) ;
        }
    }
}
