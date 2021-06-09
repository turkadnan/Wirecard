using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wirecard.Core.Configuration;
using Wirecard.Core.Dtos;
using Wirecard.Core.Models;

namespace Wirecard.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(UserApp useApp);

        ClientTokenDto CreateTokenByClient(Client client);
    }
}
