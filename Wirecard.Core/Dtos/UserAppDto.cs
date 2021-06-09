using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wirecard.Core.Dtos
{
    public class UserAppDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EMail { get; set; }
        public string IdentityNumber { get; set; }
    }
}
