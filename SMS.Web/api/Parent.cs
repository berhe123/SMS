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
    public class ParentController : EntityController<Parent, Parent, Parent>
    {
        protected IComboEntitySvc<Parent, Parent, Parent> cmbSvc;

        public ParentController(IComboEntitySvc<Parent, Parent, Parent> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

    }
}