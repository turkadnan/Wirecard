using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wirecard.CommandFramework
{
    public interface ICommandHandler<TCommand, TOutput> : IDisposable where TOutput : class //where TCommand : BaseQueryMessage
    {
        CommandResult<TOutput> Handle(TCommand cmd);

        Task<CommandResult<TOutput>> HandleAsync(TCommand cmd);

    }
}
