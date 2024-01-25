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
    public class PaymentCalandersToClassRoomController : EntityController<PaymentCalandersToClassRoom, PaymentCalandersToClassRoom, PaymentCalandersToClassRoom>
    {
        protected IComboEntitySvc<PaymentCalandersToClassRoom, PaymentCalandersToClassRoom, PaymentCalandersToClassRoom> cmbSvc;

        public PaymentCalandersToClassRoomController(IComboEntitySvc<PaymentCalandersToClassRoom, PaymentCalandersToClassRoom, PaymentCalandersToClassRoom> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        [HttpPost]
        public JsTable<vw_PaymentCalanderToClassRoomList> GetList(PageDetail pageDetail)
        {
            return (Svc as SMS.Business.Service.PaymentCalandersToClassRoomSvc).GetList(pageDetail);
        }


    }
}