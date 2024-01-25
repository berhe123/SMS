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
    public class TestScheduleController : EntityController<TestSchedule, TestSchedule, TestSchedule>
    {
        protected IComboEntitySvc<TestSchedule, TestSchedule, TestSchedule> cmbSvc;

        public TestScheduleController(IComboEntitySvc<TestSchedule, TestSchedule, TestSchedule> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        [HttpPost]
        public JsTable<vw_TestScheduleList> GetList(PageDetail pageDetail)
        {
            Guid userId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            pageDetail.UserId = userId;
            return (Svc as SMS.Business.Service.TestScheduleSvc).GetList(pageDetail);
        }

         public IEnumerable<ComboItem> GetClassRoomsByTeacherId(Guid id)
        {
            return (Svc as SMS.Business.Service.TestScheduleSvc).GetClassRoomsByTeacherId(id);
        }

        public IEnumerable<ComboItem> GetSubjectsByClassRoomId(Guid id)
        {
            return (Svc as SMS.Business.Service.TestScheduleSvc).GetSubjectsByClassRoomId(id);
        }

        public IEnumerable<ComboItem> GetTestsByClassRoomId(Guid id)
        {
            Guid userId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.TestScheduleSvc).GetTestsByClassRoomId(userId, id);
        }

        public IEnumerable<ComboItem> GetTestsByTeacherUserId(string userId)
        {
            return (Svc as SMS.Business.Service.TestScheduleSvc).GetTestsByTeacherUserId(userId);
        }

    }
}