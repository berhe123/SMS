using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class QuizResultSvc : EntityBaseSvc<QuizResult>, IComboEntitySvc<QuizResult, QuizResult, QuizResult>
    {
        readonly StudentSvc studentSvc;
        readonly QuizScheduleSvc quizscheduleSvc;
        readonly SubjectTeacherClassRoomSvc subjectteacherclassroomSvc;
        public QuizResultSvc(ILogger logger)
            : base(logger, "QuizResult")
        {
            studentSvc = new StudentSvc(logger);
            quizscheduleSvc = new QuizScheduleSvc(logger);
            subjectteacherclassroomSvc = new SubjectTeacherClassRoomSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.QuizResults
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Student.FirstName + " " + m.Student.FatherName + " " + m.Student.GrandFatherName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.QuizResults
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Student.FirstName + " " + m.Student.FatherName + " " + m.Student.GrandFatherName
                    };

            return q.AsEnumerable();
        }        

        public QuizResult GetServiceFilters()
        {
            QuizResult QuizResult = new QuizResult();
            return new QuizResult();
        }

        public IQueryable<QuizResult> Get()
        {
            return Db.QuizResults;
        }

        public QuizResult GetModel(Guid? userId)
        {
            return new QuizResult() {
                //Students = studentSvc.GetComboItems(),
                //QuizSchedules = quizscheduleSvc.GetComboItems()
            };

        }

        public QuizResult GetModelByUser(Guid? userId)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == userId.Value).Select(x => x.TableRowGuid).Single();
            return new QuizResult()
            {

                ClassRooms = subjectteacherclassroomSvc.GetClassRoomsByTeacherGuid(teacherGuid),

            };

        }

        public QuizResult GetModel(Guid id)
        {
            var QuizResult = this.Get(id);

            //QuizResult.Students = studentSvc.GetComboItems(Db);
            //QuizResult.QuizSchedules = quizscheduleSvc.GetComboItems(Db);

            return QuizResult;
        }

        public QuizResult GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }

        public QuizResult Get(Guid id)
        {
            return Db.QuizResults.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<QuizResult> Get(PageDetail pageDetail)
        {
            var query = Db.QuizResults.AsQueryable();

            List<Expression<Func<QuizResult, bool>>> filters = new List<Expression<Func<QuizResult, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.Student.FirstName + m.Student.FatherName + m.Student.GrandFatherName).Contains(pageDetail.Search));


            Func<QuizResult, QuizResult> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(QuizResult QuizResult)
        {
            if (string.IsNullOrWhiteSpace(QuizResult.QuizScheduleGuid.ToString()))
                return new FailedResponse("Please provide a valid Student Name.");

            //if (string.IsNullOrWhiteSpace(QuizResult.MonthGuid.ToString()))
            //    return new FailedResponse("Please provide a valid Month Name.");

            //if (string.IsNullOrWhiteSpace(QuizResult.QuizResultDate.ToString()))
            //    return new FailedResponse("Please provide a valid QuizResult Date.");

            //if (string.IsNullOrWhiteSpace(QuizResult.ReciptionNumber.ToString()))
            //    return new FailedResponse("Please provide a valid Reciption Number.");

            //if (QuizResult.TableRowGuid == Guid.Empty)
            //{
            //    var queryStudent = Db.QuizResults.Where(r => r.StudentGuid == QuizResult.StudentGuid);
            //    var queryMonth = Db.QuizResults.Where(r => r.MonthGuid == QuizResult.MonthGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("A QuizResult provided has already been created. Please provide a different Student or Month.");
            //}
            //else
            //{
            //    var queryStudent = Db.QuizResults.Where(r => r.StudentGuid == QuizResult.StudentGuid && r.TableRowGuid != QuizResult.TableRowGuid);
            //    var queryMonth = Db.QuizResults.Where(r => r.MonthGuid == QuizResult.MonthGuid && r.TableRowGuid != QuizResult.TableRowGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("An QuizResult provided has already been created. Please provide a different Student or Month.");
            //}

            return new SuccessResponse("");
        }

        public Response Validate(List<StudentQuizResult> quizResult)
        {

            foreach (StudentQuizResult sqr in quizResult)
            {
                decimal dbValue;
                if (string.IsNullOrWhiteSpace(sqr.StudentGuid.ToString()))
                    return new FailedResponse("Please provide a valid Student Name.");
                if (string.IsNullOrWhiteSpace(sqr.QuizScheduleGuid.ToString()))
                    return new FailedResponse("Please provide a valid Quiz.");
                if (string.IsNullOrWhiteSpace(sqr.Result))
                    return new FailedResponse("Please provide a valid result.");
                if (!decimal.TryParse(sqr.Result, out dbValue))
                    return new FailedResponse("Please provide a valid result.");
            }
            return new SuccessResponse("");
        }

        public Response Add(QuizResult QuizResult, Guid? userid)
        {
            Response response = new Response();

            response = Validate(QuizResult);
            if (!response.Success) return response;

            QuizResult.TableRowGuid = Guid.NewGuid();
            QuizResult.UserId = userid;

            return base.Add(QuizResult);
        }

        public Response SaveStudentResultByBatch(List<StudentQuizResult> quizResult)
        {
            Response response = new Response();

            response = Validate(quizResult);
            if (!response.Success) return response;

            List<QuizResult> qr = new List<QuizResult>();
            foreach (StudentQuizResult sqr in quizResult)
            {
                QuizResult x = new QuizResult();
                x.QuizScheduleGuid = sqr.QuizScheduleGuid;
                x.StudentGuid = sqr.StudentGuid;
                x.Result = Convert.ToDecimal(sqr.Result);
                x.Comment = sqr.Comment;
                x.TableRowGuid = Guid.NewGuid();

                qr.Add(x);

            }

            return base.Add(qr);
        }

        public Response Update(QuizResult QuizResult, Guid? userid)
        {
            Response response = new Response();

            response = Validate(QuizResult);
            if (!response.Success) return response;

            QuizResult.UserId = userid;

            return base.Update(QuizResult);
        }

        public Response Delete(QuizResult QuizResult, Guid? userid)
        {
            QuizResult.UserId = userid;
            return base.Delete(QuizResult);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new QuizResult() { TableRowGuid = id }, userid);
        }


    }
}
