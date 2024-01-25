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
    public class Period1Controller : EntityController<Period1, Period1, Period1>
    {
        protected IComboEntitySvc<Period1, Period1, Period1> cmbSvc;

        public Period1Controller(IComboEntitySvc<Period1, Period1, Period1> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        [HttpPost]
        public JsTable<vw_Period1List> GetList(PageDetail pageDetail)
        {           
            return (Svc as SMS.Business.Service.Period1Svc).GetList(pageDetail);
        }


    }
}