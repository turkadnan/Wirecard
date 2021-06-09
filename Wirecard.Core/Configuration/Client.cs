using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wirecard.Core.Configuration
{
    public class Client
    {
        public string Id { get; set; }
        public string Screet { get; set; }

        /// <summary>
        /// Erişebileceği API listesi tutulacak. örn wwww.api1.com, wwww.api2.com
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public List<string> Audiences { get; set; }
    }
}
