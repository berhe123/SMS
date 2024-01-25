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
    public class HolidayController : EntityController<Holiday, Holiday, Holiday>
    {
        protected IComboEntitySvc<Holiday, Holiday, Holiday> cmbSvc;

        public HolidayController(IComboEntitySvc<Holiday, Holiday, Holiday> Svc, ILogger logger)
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