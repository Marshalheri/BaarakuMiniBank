using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaarakuMiniBankAPIs.Middleware.Core
{
    public class SystemSettings
    {
        public bool UseSwagger { get; set; }
        public bool UseFake { get; set; }
        public string CustomerIdPrefix { get; set; }
        public string AccountNumberPrefix { get; set; }
        public string BankCode { get; set; }
    }
}
