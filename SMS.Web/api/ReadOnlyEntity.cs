using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMS.Core;
using SMS.Entities;
using System.Web;

namespace SMS.Web.api
{
    public class ReadOnlyEntityController<T, TD, TR> : ApiController where T : class
    {
        protected IReadOnlyEntitySvc<T, TD, TR> Svc;
        protected readonly ILogger Logger;
        protected string EntityName { get; set; }

        public ReadOnlyEntityController(IReadOnlyEntitySvc<T, TD, TR> Svc, ILogger logger)
        {
            this.Svc = Svc;
            this.Logger = logger;
        }

        public IQueryable<TR> Get()
        {
            return Svc.Get();
        }

        public T Get(Guid id)
        {
            return Svc.Get(id);
        }

        [HttpPost]
        public JsTable<TD> Get(PageDetail pageDetail)
        {
            return Svc.Get(pageDetail);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return Svc.GetComboItems();
        }
    }
}