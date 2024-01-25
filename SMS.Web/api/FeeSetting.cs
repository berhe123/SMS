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
    public class FeeSettingController : EntityController<FeeSetting, FeeSetting, FeeSetting>
    {
        protected IComboEntitySvc<FeeSetting, FeeSetting, FeeSetting> cmbSvc;

        public FeeSettingController(IComboEntitySvc<FeeSetting, FeeSetting, FeeSetting> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        [HttpPost]
        public JsTable<vw_FeeSettingList> GetList(PageDetail pageDetail)
        {
            return (Svc as SMS.Business.Service.FeeSettingSvc).GetList(pageDetail);
        }

        public IEnumerable<ComboItem> GetFeeSettingByPaymentCalanderId(Guid id)
        {
            return (Svc as SMS.Business.Service.FeeSettingSvc).GetFeeSettingByPaymentCalanderId(id);
        }

    }
}