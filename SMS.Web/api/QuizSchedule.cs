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
    public class QuizScheduleController : EntityController<QuizSchedule, QuizSchedule, QuizSchedule>
    {
        protected IComboEntitySvc<QuizSchedule, QuizSchedule, QuizSchedule> cmbSvc;

        public QuizScheduleController(IComboEntitySvc<QuizSchedule, QuizSchedule, QuizSchedule> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        [HttpPost]
        public JsTable<vw_QuizScheduleList> GetList(PageDetail pageDetail)
        {
            Guid userId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            pageDetail.UserId = userId;
            return (Svc as SMS.Business.Service.QuizScheduleSvc).GetList(pageDetail);
        }

        public IEnumerable<ComboItem> GetClassRoomsByTeacherId(Guid id)
        {
            return (Svc as SMS.Business.Service.QuizScheduleSvc).GetClassRoomsByTeacherId(id);
        }

        public IEnumerable<ComboItem> GetSubjectsByClassRoomId(Guid id)
        {
            return (Svc as SMS.Business.Service.QuizScheduleSvc).GetSubjectsByClassRoomId(id);
        }

        public IEnumerable<ComboItem> GetQuizsByClassRoomId(Guid id)
        {
            Guid userId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.QuizScheduleSvc).GetQuizsByClassRoomId(userId, id);
        }

        public IEnumerable<ComboItem> GetQuizsByTeacherUserId(string userId)
        {
            return (Svc as SMS.Business.Service.QuizScheduleSvc).GetQuizsByTeacherUserId(userId);
        }

    }
}