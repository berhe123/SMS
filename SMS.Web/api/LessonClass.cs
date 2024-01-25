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
    public class LessonClassController : EntityController<LessonClass, LessonClass, LessonClass>
    {
        protected IComboEntitySvc<LessonClass, LessonClass, LessonClass> cmbSvc;

        public LessonClassController(IComboEntitySvc<LessonClass, LessonClass, LessonClass> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        [HttpPost]
        public JsTable<vw_LessonClassList> GetList(PageDetail pageDetail)
        {
            return (Svc as SMS.Business.Service.LessonClassSvc).GetList(pageDetail);
        }

        [HttpPost]
        public JsTable<vw_LessonClassListForTeacher> GetListByTeacher(PageDetail pageDetail, string userId)
        {
            return (Svc as SMS.Business.Service.LessonClassSvc).GetListByTeacher(pageDetail, userId);
        }

        public IEnumerable<ComboItem> GetPeriodsBySubjectGuid(Guid id)
        {
            Guid userId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.LessonClassSvc).GetPeriodsBySubjectGuid(userId,id);
        }

        public IEnumerable<ComboItem> GetClassRoomsByTeacherUserId(Guid id)
        {
            return (Svc as SMS.Business.Service.LessonClassSvc).GetClassRoomsByTeacherUserId(id);
        }

        public IEnumerable<ComboItem> GetTeacherByLessonScheduleId(Guid id)
        {
            return (Svc as SMS.Business.Service.LessonClassSvc).GetTeacherByLessonClassId(id);
        }
        //public JsTable<TeacherClassRoom> GetClassRoomsByTeacherUserId(PageDetail pageDetail, string userId)
        //{
        //    //return (Svc as SMS.Business.Service.LessonClassSvc).GetClassRoomsByTeacherUserId(pageDetail,nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name));
        //    return (Svc as SMS.Business.Service.LessonClassSvc).GetClassRoomsByTeacherUserId(pageDetail, userId);
        //}



        public IEnumerable<ComboItem> GetSubjectsByTeacherUserIdandClassRoomId(Guid Id)
        {
            Guid userId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.LessonClassSvc).GetSubjectsByTeacherUserIdandClassRoomId(userId, Id);
        }

        public IEnumerable<ComboItem> GetLessonClassesByTeacherUserIdandClassRoomId(string userId, Guid classroomId)
        {
            return (Svc as SMS.Business.Service.LessonClassSvc).GetLessonClassesByTeacherUserIdandClassRoomId(userId, classroomId);
        }
    }
}