using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wirecard.Core.Services
{
    public interface ICommandLogger
    {
        void LogInput(string cmd);
        void LogOutput(string output);
    }
}
