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
    public class GroupWorkScheduleController : EntityController<GroupWorkSchedule, GroupWorkSchedule, GroupWorkSchedule>
    {
        protected IComboEntitySvc<GroupWorkSchedule, GroupWorkSchedule, GroupWorkSchedule> cmbSvc;

        public GroupWorkScheduleController(IComboEntitySvc<GroupWorkSchedule, GroupWorkSchedule, GroupWorkSchedule> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        [HttpPost]
        public JsTable<vw_GroupWorkScheduleList> GetList(PageDetail pageDetail)
        {
            return (Svc as SMS.Business.Service.GroupWorkScheduleSvc).GetList(pageDetail);
        }

        //public JsTable<GroupNames> GetGroupNamesByLessonClassId(PageDetail pageDetail, Guid id)
        //{
        //    return (Svc as SMS.Business.Service.GroupWorkScheduleSvc).GetGroupNamesByLessonClassId(pageDetail, id);
        //}
    }
}