using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class ExamResultSvc : EntityBaseSvc<ExamResult>, IComboEntitySvc<ExamResult, ExamResult, ExamResult>
    {
        readonly StudentSvc studentSvc;
        //readonly ExamScheduleSvc examscheduleSvc;
        readonly SubjectTeacherClassRoomSvc subjectteacherclassroomSvc;
        public ExamResultSvc(ILogger logger)
            : base(logger, "ExamResult")
        {
            studentSvc = new StudentSvc(logger);
            //examscheduleSvc = new ExamScheduleSvc(logger);
            subjectteacherclassroomSvc = new SubjectTeacherClassRoomSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.ExamResults
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Student.FirstName + " " + m.Student.FatherName + " " + m.Student.GrandFatherName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.ExamResults
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Student.FirstName + " " + m.Student.FatherName + " " + m.Student.GrandFatherName
                    };

            return q.AsEnumerable();
        }

        public ExamResult GetServiceFilters()
        {
            ExamResult ExamResult = new ExamResult();
            return new ExamResult();
        }

        public IQueryable<ExamResult> Get()
        {
            return Db.ExamResults;
        }

        public ExamResult GetModel(Guid? userId)
        {
            return new ExamResult() {
                //Students = studentSvc.GetComboItems(),
                //ExamSchedules = examscheduleSvc.GetComboItems()
            };

        }
        public ExamResult GetModelByUser(Guid? userId)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == userId.Value).Select(x => x.TableRowGuid).Single();
            return new ExamResult()
            {
                ClassRooms = subjectteacherclassroomSvc.GetClassRoomsByTeacherGuid(teacherGuid)
            };

        }
        public ExamResult GetModel(Guid id)
        {
            var ExamResult = this.Get(id);

            //ExamResult.Students = studentSvc.GetComboItems(Db);
            //ExamResult.ExamSchedules = examscheduleSvc.GetComboItems(Db);

            return ExamResult;
        }
        public ExamResult GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }

        public ExamResult Get(Guid id)
        {
            return Db.ExamResults.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<ExamResult> Get(PageDetail pageDetail)
        {
            var query = Db.ExamResults.AsQueryable();

            List<Expression<Func<ExamResult, bool>>> filters = new List<Expression<Func<ExamResult, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.Student.FirstName + m.Student.FatherName + m.Student.GrandFatherName).Contains(pageDetail.Search));


            Func<ExamResult, ExamResult> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(ExamResult ExamResult)
        {
            if (string.IsNullOrWhiteSpace(ExamResult.ExamScheduleGuid.ToString()))
                return new FailedResponse("Please provide a valid Student Name.");

            //if (string.IsNullOrWhiteSpace(ExamResult.MonthGuid.ToString()))
            //    return new FailedResponse("Please provide a valid Month Name.");

            //if (string.IsNullOrWhiteSpace(ExamResult.ExamResultDate.ToString()))
            //    return new FailedResponse("Please provide a valid ExamResult Date.");

            //if (string.IsNullOrWhiteSpace(ExamResult.ReciptionNumber.ToString()))
            //    return new FailedResponse("Please provide a valid Reciption Number.");

            //if (ExamResult.TableRowGuid == Guid.Empty)
            //{
            //    var queryStudent = Db.ExamResults.Where(r => r.StudentGuid == ExamResult.StudentGuid);
            //    var queryMonth = Db.ExamResults.Where(r => r.MonthGuid == ExamResult.MonthGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("A ExamResult provided has already been created. Please provide a different Student or Month.");
            //}
            //else
            //{
            //    var queryStudent = Db.ExamResults.Where(r => r.StudentGuid == ExamResult.StudentGuid && r.TableRowGuid != ExamResult.TableRowGuid);
            //    var queryMonth = Db.ExamResults.Where(r => r.MonthGuid == ExamResult.MonthGuid && r.TableRowGuid != ExamResult.TableRowGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("An ExamResult provided has already been created. Please provide a different Student or Month.");
            //}

            return new SuccessResponse("");
        }

        public Response Validate(List<StudentExamResult> studentExamResult)
        {

            foreach (StudentExamResult ser in studentExamResult)
            {
                decimal dbValue;
                if (string.IsNullOrWhiteSpace(ser.StudentGuid.ToString()))
                    return new FailedResponse("Please provide a valid Student Name.");
                if (string.IsNullOrWhiteSpace(ser.ExamScheduleGuid.ToString()))
                    return new FailedResponse("Please provide a valid Subject.");
                if (string.IsNullOrWhiteSpace(ser.Result))
                    return new FailedResponse("Please provide a valid result.");
                if (!decimal.TryParse(ser.Result, out dbValue))
                    return new FailedResponse("Please provide a valid result.");
            }
            return new SuccessResponse("");
        }

        public Response Add(ExamResult ExamResult, Guid? userid)
        {
            Response response = new Response();

            response = Validate(ExamResult);
            if (!response.Success) return response;

            ExamResult.TableRowGuid = Guid.NewGuid();
            ExamResult.UserId = userid;

            return base.Add(ExamResult);
        }

        public Response SaveStudentResultByBatch(List<StudentExamResult> studentExamResult)
        {
            Response response = new Response();

            response = Validate(studentExamResult);
            if (!response.Success) return response;

            List<ExamResult> er = new List<ExamResult>();
            foreach (StudentExamResult ser in studentExamResult)
            {
                ExamResult x = new ExamResult();
                x.ExamScheduleGuid = ser.ExamScheduleGuid;
                x.StudentGuid = ser.StudentGuid;
                x.Result = Convert.ToDecimal(ser.Result);
                x.Comment = ser.Comment;
                x.TableRowGuid = Guid.NewGuid();

                er.Add(x);

            }

            return base.Add(er);
        }

        public Response Update(ExamResult ExamResult, Guid? userid)
        {
            Response response = new Response();

            response = Validate(ExamResult);
            if (!response.Success) return response;

            ExamResult.UserId = userid;

            return base.Update(ExamResult);
        }

        public Response Delete(ExamResult ExamResult, Guid? userid)
        {
            ExamResult.UserId = userid;
            return base.Delete(ExamResult);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new ExamResult() { TableRowGuid = id }, userid);
        }


    }
}
