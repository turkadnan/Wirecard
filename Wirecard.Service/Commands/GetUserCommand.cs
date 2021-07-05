using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wirecard.CommandFramework;
using Wirecard.Core.Services;

namespace Wirecard.Business.Commands
{
    public class GetUserCommand
    {
        public string UserName { get; set; }
    }

    public class GetUserResult
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
    }

    public class GetUserCommandHandler : BaseCommandHandler<GetUserCommand, GetUserResult>
    {
        IUserService userService;
 
        public GetUserCommandHandler(ICommandLogger commandLogger, IUserService userService) : base(commandLogger)
        {
            this.userService = userService;
        }

        public override void HandleInner(GetUserCommand cmd)
        {


            var user = userService.GetUserByNameAync(cmd.UserName).Result;

            base.CommandResult.Result = 0;
            base.CommandResult.OutputObject.UserId = user.Data.Id;
            base.CommandResult.OutputObject.UserName = user.Data.UserName;


                 
        }
    }
}
