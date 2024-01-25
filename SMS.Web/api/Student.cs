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
    public class StudentController : EntityController<Student, Student, Student>
    {
        protected IComboEntitySvc<Student, Student, Student> cmbSvc;

        public StudentController(IComboEntitySvc<Student, Student, Student> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }
        public IEnumerable<ComboItem> GetStudentsByClassRoomId(Guid id)
        {
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsByClassRoomId(id);
        }

        [HttpPost]
        public JsTable<vw_StudentList> GetList(PageDetail pageDetail)
        {           
            return (Svc as SMS.Business.Service.StudentSvc).GetList(pageDetail);
        }

        [HttpPost]
        public JsTable<vw_StudentListByClassRoomId> GetStudentsByClassRoomGuid(PageDetail pagedetail)
        {
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsByClassRoomId(pagedetail);
        }

        [HttpPost]
        public JsTable<vw_StudentsForQuizResultDataEntry> GetStudentsForQuizResultDataEntry(PageDetail pagedetail)
        {
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsForQuizResultDataEntry(pagedetail);
        }
        [HttpPost]
        public JsTable<vw_StudentsForTestResultDataEntry> GetStudentsForTestResultDataEntry(PageDetail pagedetail)
        {
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsForTestResultDataEntry(pagedetail);
        }
        [HttpPost]
        public JsTable<vw_StudentsForExerciseBookResultDataEntry> GetStudentsForExerciseBookResultDataEntry(PageDetail pagedetail)
        {
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsForExerciseBookResultDataEntry(pagedetail);
        }
        [HttpPost]
        public JsTable<vw_StudentsForExamResultDataEntry> GetStudentsForExamResultDataEntry(PageDetail pagedetail)
        {
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsForExamResultDataEntry(pagedetail);
        }
        [HttpPost]
        public JsTable<vw_StudentsForLessonClassDataView> GetStudentsForLessonClassDataView(PageDetail pagedetail)
        {
            pagedetail.UserId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsForLessonClassDataView(pagedetail);
        }
        [HttpPost]
        public JsTable<vw_StudentsForIndividualWorkScheduleDataView> GetStudentsForIndividualWorkScheduleDataView(PageDetail pagedetail)
        {
            pagedetail.UserId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsForIndividualWorkScheduleDataView(pagedetail);
        }
        [HttpPost]
        public JsTable<vw_StudentsForQuizScheduleDataView> GetStudentsForQuizScheduleDataView(PageDetail pagedetail)
        {
            pagedetail.UserId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsForQuizScheduleDataView(pagedetail);
        }
        [HttpPost]
        public JsTable<vw_StudentInfromationDataView> GetStudentsForInfromationDataView(PageDetail pagedetail)
        {
            pagedetail.UserId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsForInfromationDataView(pagedetail);
        }
        [HttpPost]
        public JsTable<vw_StudentPaymentDataView> GetStudentsForPaymentDataView(PageDetail pagedetail)
        {
            pagedetail.UserId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsForPaymentDataView(pagedetail);
        }
        [HttpPost]
        public JsTable<vw_StudentUnPaymentDataView> GetStudentsForUnPaymentDataView(PageDetail pagedetail)
        {
            pagedetail.UserId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsForUnPaymentDataView(pagedetail);
        }
        [HttpPost]
        public JsTable<vw_StudentsForIndividualWorkResultDataView> GetStudentsForIndividualWorkResultDataView(PageDetail pagedetail)
        {
            pagedetail.UserId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsForIndividualWorkResultDataView(pagedetail);
        }
        [HttpPost]
        public JsTable<vw_StudentsForQuizResultDataView> GetStudentsForQuizResultDataView(PageDetail pagedetail)
        {
            pagedetail.UserId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsForQuizResultDataView(pagedetail);
        }
        [HttpPost]
        public JsTable<vw_StudentsForTestResultDataView> GetStudentsForTestResultDataView(PageDetail pagedetail)
        {
            pagedetail.UserId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsForTestResultDataView(pagedetail);
        }
        [HttpPost]
        public JsTable<vw_StudentsForExerciseBookResultDataView> GetStudentsForExerciseBookResultDataView(PageDetail pagedetail)
        {
            pagedetail.UserId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsForExerciseBookResultDataView(pagedetail);
        }
        [HttpPost]
        public JsTable<vw_StudentsForExamResultDataView> GetStudentsForExamResultDataView(PageDetail pagedetail)
        {
            pagedetail.UserId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsForExamResultDataView(pagedetail);
        }
        [HttpPost]
        public JsTable<vw_StudentsForTestScheduleDataView> GetStudentsForTestScheduleDataView(PageDetail pagedetail)
        {
            pagedetail.UserId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsForTestScheduleDataView(pagedetail);
        }
        [HttpPost]
        public JsTable<vw_StudentsForExamScheduleDataView> GetStudentsForExamScheduleDataView(PageDetail pagedetail)
        {
            pagedetail.UserId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsForExamScheduleDataView(pagedetail);
        }     
        [HttpPost]
        public JsTable<vw_StudentsForIndividualWorkResultDataEntry> GetStudentsForIndividualWorkResultDataEntry(PageDetail pagedetail)
        {
            return (Svc as SMS.Business.Service.StudentSvc).GetStudentsForIndividualWorkResultDataEntry(pagedetail);
        }   
    }
}