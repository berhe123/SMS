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
    public class PeriodController : EntityController<Period, Period, Period>
    {
        protected IComboEntitySvc<Period, Period, Period> cmbSvc;

        public PeriodController(IComboEntitySvc<Period, Period, Period> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        [HttpPost]
        public JsTable<vw_PeriodList> GetList(PageDetail pageDetail)
        {
            return (Svc as SMS.Business.Service.PeriodSvc).GetList(pageDetail);
        }


    }
}