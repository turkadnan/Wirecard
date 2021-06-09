using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wirecard.Core.Dtos
{
    public class CreateUserDto
    {
        /// <summary>
        /// Kullanılmak istenmez ise email in @ den önceki kısmı alınıp bir kaçtane karakter atılarak kaydedilebilir. adnan_turk123456 gibi
        /// </summary>
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}