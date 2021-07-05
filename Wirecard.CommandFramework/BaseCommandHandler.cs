using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Wirecard.Core.Services;

namespace Wirecard.CommandFramework
{
    public abstract class BaseCommandHandler<TCommand, TOutput> : IDisposable, ICommandHandler<TCommand, TOutput> where TOutput : class, new()
    {
        ICommandLogger commandLogger;

        [DataMember]
        public CommandResult<TOutput> CommandResult { get; private set; }

        public BaseCommandHandler(ICommandLogger commandLogger)
        {
            this.commandLogger = commandLogger;

            CommandResult = new CommandResult<TOutput>();
            CommandResult.Result = -1;
            CommandResult.ErrorCode = "";
            CommandResult.ErrorMessage = "";
            CommandResult.ReferenceId = "";
            CommandResult.OutputObject = new TOutput();


        }

        public void Dispose()
        {

        }

        public abstract void HandleInner(TCommand cmd);

        public CommandResult<TOutput> Handle(TCommand cmd)
        {
            commandLogger.LogInput(cmd.ToString());

            try
            {
                HandleInner(cmd);
            }
            catch (Exception ex)
            {
                CommandResult.Result = 1;
                CommandResult.ErrorCode = "Error Occured";
                CommandResult.ErrorMessage = ex.ToString();
                CommandResult.ReferenceId = "";
            }

            commandLogger.LogOutput(this.ToString());

            return CommandResult;
        }

        public async Task<CommandResult<TOutput>> HandleAsync(TCommand cmd)
        {
            return await Task.Run(() => { return Handle(cmd); });
        }
    }
}
