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
    public class IndividualWorkScheduleController : EntityController<IndividualWorkSchedule, IndividualWorkSchedule, IndividualWorkSchedule>
    {
        protected IComboEntitySvc<IndividualWorkSchedule, IndividualWorkSchedule, IndividualWorkSchedule> cmbSvc;

        public IndividualWorkScheduleController(IComboEntitySvc<IndividualWorkSchedule, IndividualWorkSchedule, IndividualWorkSchedule> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;           
        }
        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }
        [HttpPost]
        public JsTable<vw_IndividualWorkScheduleList> GetList(PageDetail pageDetail)
        {
            pageDetail.UserId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.IndividualWorkScheduleSvc).GetList(pageDetail);
        }
        [HttpPost]
        public JsTable<vw_IndividualWorkScheduleList> GetAllIndividualWorksForDataView(PageDetail pagedetail)
        {
            return (Svc as SMS.Business.Service.IndividualWorkScheduleSvc).GetAllIndividualWorksForDataView(pagedetail);
        }

        public IEnumerable<ComboItem> GetClassRoomsByTeacherId(Guid id)
        {
            return (Svc as SMS.Business.Service.IndividualWorkScheduleSvc).GetClassRoomsByTeacherId(id);
        }
        public IEnumerable<ComboItem> GetSubjectsByClassRoomId(Guid id)
        {
            return (Svc as SMS.Business.Service.IndividualWorkScheduleSvc).GetSubjectsByClassRoomId(id);
        }
        public IEnumerable<ComboItem> GetIndividualWorkSchedulesBySubjectId(Guid id)
        {
            Guid userId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.IndividualWorkScheduleSvc).GetIndividualWorkSchedulesBySubjectId(id, userId);
        }
        public IEnumerable<ComboItem> GetIndividualWorksByClassRoomId(Guid classroomId, string userId)
        {
            return (Svc as SMS.Business.Service.IndividualWorkScheduleSvc).GetSubjectsByTeacherUserIdandClassRoomId(classroomId, userId);
        }
        public IEnumerable<ComboItem> GetIndividualWorksByTeacherGuid()
        {
            Guid userId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.IndividualWorkScheduleSvc).GetIndividualWorksByTeacherGuid(userId);
        }
    }
}