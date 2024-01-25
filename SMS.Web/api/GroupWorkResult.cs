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
    public class GroupWorkResultController : EntityController<GroupWorkResult, GroupWorkResult, GroupWorkResult>
    {
        protected IComboEntitySvc<GroupWorkResult, GroupWorkResult, GroupWorkResult> cmbSvc;

        public GroupWorkResultController(IComboEntitySvc<GroupWorkResult, GroupWorkResult, GroupWorkResult> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        [HttpPost]
        public JsTable<vw_GroupWorkResultList> GetList(PageDetail pageDetail, Guid lessonclassId)
        {
            return (Svc as SMS.Business.Service.GroupWorkResultSvc).GetList(pageDetail, lessonclassId);
        }


    }
}