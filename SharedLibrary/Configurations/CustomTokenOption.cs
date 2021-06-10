using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Configurations
{
    public class CustomTokenOption
    {
        public List<string> Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpration { get; set; }
        public int RefresfTokenExpration { get; set; }
        public string SecurityKey { get; set; }
    }
}
