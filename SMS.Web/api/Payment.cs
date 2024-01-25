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
    public class PaymentController : EntityController<Payment, Payment, Payment>
    {
        protected IComboEntitySvc<Payment, Payment, Payment> cmbSvc;

        public PaymentController(IComboEntitySvc<Payment, Payment, Payment> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        [HttpPost]
        public JsTable<vw_PaymentList> GetList(PageDetail pageDetail)
        {           
            return (Svc as SMS.Business.Service.PaymentSvc).GetList(pageDetail);
        }


    }
}