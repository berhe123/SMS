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
    public class ExamScheduleController : EntityController<ExamSchedule, ExamSchedule, ExamSchedule>
    {
        protected IComboEntitySvc<ExamSchedule, ExamSchedule, ExamSchedule> cmbSvc;

        public ExamScheduleController(IComboEntitySvc<ExamSchedule, ExamSchedule, ExamSchedule> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }
        [HttpPost]
        public JsTable<vw_ExamScheduleList> GetList(PageDetail pageDetail)
        {           
            return (Svc as SMS.Business.Service.ExamScheduleSvc).GetList(pageDetail);
        }

        public IEnumerable<ComboItem> GetClassRoomsByTeacherId(Guid id)
        {
            return (Svc as SMS.Business.Service.ExamScheduleSvc).GetClassRoomsByTeacherId(id);
        }

        public IEnumerable<ComboItem> GetSubjectsByClassRoomId(Guid id)
        {
            Guid userId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.ExamScheduleSvc).GetSubjectsByClassRoomId(userId, id);
        }

        public IEnumerable<ComboItem> GetExamsByTeacherUserId(string userId)
        {
            return (Svc as SMS.Business.Service.ExamScheduleSvc).GetExamsByTeacherUserId(userId);
        }

    }
}