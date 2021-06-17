using SharedLibrary.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Wirecard.Core.Configuration;
using Wirecard.Core.Dtos;
using Wirecard.Core.Models;

namespace Wirecard.Core.Providers
{
    public interface ITokenProvider
    {
        TokenDto GetToken(UserApp useApp, string refreshToken, CustomTokenOption tokenOption);
        ClientTokenDto GetTokenByClient(Client client, CustomTokenOption tokenOption);

    }

}
