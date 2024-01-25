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
    public class TeacherController : EntityController<Teacher, Teacher, Teacher>
    {
        protected IComboEntitySvc<Teacher, Teacher, Teacher> cmbSvc;

        public TeacherController(IComboEntitySvc<Teacher, Teacher, Teacher> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        public IEnumerable<ComboItem> GetExaminer(Guid id)
        {
            return (Svc as SMS.Business.Service.TeacherSvc).GetComboItems(id);
        }

        [HttpPost]
        public JsTable<vw_TeacherList> GetList(PageDetail pageDetail)
        {
            return (Svc as SMS.Business.Service.TeacherSvc).GetList(pageDetail);
        }

        public Guid[] GetClassRooms(Guid id)
        {
            return (Svc as SMS.Business.Service.TeacherSvc).GetClassRooms(id);
        }

        [HttpPost]
        public JsTable<vw_TeachersForLessonClassDataView> GetTeachersForLessonClassDataView(PageDetail pagedetail)
        {
            pagedetail.UserId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.TeacherSvc).GetTeachersForLessonClassDataView(pagedetail);
        }      
    }
}