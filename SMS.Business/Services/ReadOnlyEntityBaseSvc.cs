using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Core;
using SMS.Entities;
using NLog;
using System.Linq.Expressions;

namespace SMS.Business.Service
{

    public class ReadOnlyEntityBaseSvc<T> where T : class
    {

        protected readonly SMSEntities Db = new SMSEntities();
        protected readonly ILogger Logger;
        protected readonly string TypeName;

        public ReadOnlyEntityBaseSvc(ILogger logger)
        {
            this.Logger = logger;
            this.TypeName = typeof(T).Name;
        }

        public ReadOnlyEntityBaseSvc(ILogger logger, SMSEntities db)
        {
            this.Logger = logger;
            this.TypeName = typeof(T).Name;
            this.Db = db;
        }
        
    }
}
