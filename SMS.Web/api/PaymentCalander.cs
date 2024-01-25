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
    public class PaymentCalanderController : EntityController<PaymentCalander, PaymentCalander, PaymentCalander>
    {
        protected IComboEntitySvc<PaymentCalander, PaymentCalander, PaymentCalander> cmbSvc;

        public PaymentCalanderController(IComboEntitySvc<PaymentCalander, PaymentCalander, PaymentCalander> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        [HttpPost]
        public JsTable<vw_PaymentCalanderList> GetList(PageDetail pageDetail)
        {
            return (Svc as SMS.Business.Service.PaymentCalanderSvc).GetList(pageDetail);
        }


    }
}