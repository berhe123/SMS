using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class SMSEntities
    {
        public SMSEntities(bool proxyCreationEnabled)
            : base("name=SMSEntities")
        {
            Configuration.ProxyCreationEnabled = proxyCreationEnabled;

        }
    }
}
