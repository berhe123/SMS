using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class StudentAttendanceSvc : EntityBaseSvc<StudentAttendance>, IComboEntitySvc<StudentAttendance, StudentAttendance, StudentAttendance>
    {
        readonly ClassRoomSvc classroomSvc;
        readonly LessonClassSvc lessonscheduleSvc;
        readonly StudentSvc studentSvc;
        public StudentAttendanceSvc(ILogger logger)
            : base(logger, "StudentAttendance")
        {
            classroomSvc = new ClassRoomSvc(logger);
            lessonscheduleSvc = new LessonClassSvc(logger);
            studentSvc = new StudentSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.StudentAttendances
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Student.FirstName+" "+m.Student.FatherName+" "+m.Student.GrandFatherName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.StudentAttendances
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Student.FirstName + " " + m.Student.FatherName + " " + m.Student.GrandFatherName
                    };

            return q.AsEnumerable();
        }

        public StudentAttendance GetServiceFilters()
        {
            StudentAttendance StudentAttendance = new StudentAttendance();
            return new StudentAttendance();
        }

        public IQueryable<StudentAttendance> Get()
        {
            return Db.StudentAttendances;
        }

        public StudentAttendance GetModel(Guid? userId)
        {
            return new StudentAttendance() {

            };

        }

        public StudentAttendance GetModel(Guid id)
        {
            var StudentAttendance = this.Get(id);

            return StudentAttendance;
        }

        public StudentAttendance GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModel(id) : this.GetModel(userId);
        }

        public StudentAttendance Get(Guid id)
        {
            return Db.StudentAttendances.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<StudentAttendance> Get(PageDetail pageDetail)
        {
            var query = Db.StudentAttendances.AsQueryable();

            List<Expression<Func<StudentAttendance, bool>>> filters = new List<Expression<Func<StudentAttendance, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.Student.FirstName+m.Student.FatherName+m.Student.GrandFatherName).Contains(pageDetail.Search));


            Func<StudentAttendance, StudentAttendance> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public JsTable<vw_StudentAttendanceList> GetList(PageDetail pageDetail, Guid lessonclassId)
        {
            var query = (from m in Db.StudentAttendances
                         join l in Db.LessonClasses on m.LessonClassGuid equals l.TableRowGuid
                         where l.TableRowGuid == lessonclassId
                         select new vw_StudentAttendanceList
                         {

                         }
                             ).AsQueryable();

            List<Expression<Func<vw_StudentAttendanceList, bool>>> filters = new List<Expression<Func<vw_StudentAttendanceList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.StudentAttendance.Contains(pageDetail.Search));


            Func<vw_StudentAttendanceList, vw_StudentAttendanceList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(StudentAttendance StudentAttendance)
        {
            if (string.IsNullOrWhiteSpace(StudentAttendance.LessonClassGuid.ToString()))
                return new FailedResponse("Please provide a valid Student Name.");

            //if (string.IsNullOrWhiteSpace(StudentAttendance.MonthGuid.ToString()))
            //    return new FailedResponse("Please provide a valid Month Name.");

            //if (string.IsNullOrWhiteSpace(StudentAttendance.StudentAttendanceDate.ToString()))
            //    return new FailedResponse("Please provide a valid StudentAttendance Date.");

            //if (string.IsNullOrWhiteSpace(StudentAttendance.ReciptionNumber.ToString()))
            //    return new FailedResponse("Please provide a valid Reciption Number.");

            //if (StudentAttendance.TableRowGuid == Guid.Empty)
            //{
            //    var queryStudent = Db.StudentAttendances.Where(r => r.StudentGuid == StudentAttendance.StudentGuid);
            //    var queryMonth = Db.StudentAttendances.Where(r => r.MonthGuid == StudentAttendance.MonthGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("A StudentAttendance provided has already been created. Please provide a different Student or Month.");
            //}
            //else
            //{
            //    var queryStudent = Db.StudentAttendances.Where(r => r.StudentGuid == StudentAttendance.StudentGuid && r.TableRowGuid != StudentAttendance.TableRowGuid);
            //    var queryMonth = Db.StudentAttendances.Where(r => r.MonthGuid == StudentAttendance.MonthGuid && r.TableRowGuid != StudentAttendance.TableRowGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("An StudentAttendance provided has already been created. Please provide a different Student or Month.");
            //}

            return new SuccessResponse("");
        }

        public Response Add(StudentAttendance StudentAttendance, Guid? userid)
        {
            Response response = new Response();

            response = Validate(StudentAttendance);
            if (!response.Success) return response;

            StudentAttendance.TableRowGuid = Guid.NewGuid();
            StudentAttendance.UserId = userid;

            return base.Add(StudentAttendance);
        }

        public Response Update(StudentAttendance StudentAttendance, Guid? userid)
        {
            Response response = new Response();

            response = Validate(StudentAttendance);
            if (!response.Success) return response;

            StudentAttendance.UserId = userid;

            return base.Update(StudentAttendance);
        }

        public Response Delete(StudentAttendance StudentAttendance, Guid? userid)
        {
            StudentAttendance.UserId = userid;
            return base.Delete(StudentAttendance);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new StudentAttendance() { TableRowGuid = id }, userid);
        }


    }
}
