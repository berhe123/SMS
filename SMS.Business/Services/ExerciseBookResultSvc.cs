using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class ExerciseBookResultSvc : EntityBaseSvc<ExerciseBookResult>, IComboEntitySvc<ExerciseBookResult, ExerciseBookResult, ExerciseBookResult>
    {
        readonly StudentSvc studentSvc;
        readonly LessonClassSvc lessonscheduleSvc;
        readonly SubjectTeacherClassRoomSvc subjectteacherclassroomSvc;
        public ExerciseBookResultSvc(ILogger logger)
            : base(logger, "ExerciseBookResult")
        {
            studentSvc = new StudentSvc(logger);
            lessonscheduleSvc = new LessonClassSvc(logger);
            subjectteacherclassroomSvc = new SubjectTeacherClassRoomSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.ExerciseBookResults
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Student.FirstName + " " + m.Student.FatherName + " " + m.Student.GrandFatherName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.ExerciseBookResults
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Student.FirstName + " " + m.Student.FatherName + " " + m.Student.GrandFatherName
                    };

            return q.AsEnumerable();
        }

        public ExerciseBookResult GetServiceFilters()
        {
            ExerciseBookResult ExerciseBookResult = new ExerciseBookResult();
            return new ExerciseBookResult();
        }

        public IQueryable<ExerciseBookResult> Get()
        {
            return Db.ExerciseBookResults;
        }

        public ExerciseBookResult GetModel(Guid? userId)
        {
            return new ExerciseBookResult() {
                //Students = studentSvc.GetComboItems(),
                //LessonSchedules = lessonscheduleSvc.GetComboItems()
            };

        }
        public ExerciseBookResult GetModelByUser(Guid? userId)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == userId.Value).Select(x => x.TableRowGuid).Single();
            return new ExerciseBookResult
            {
                ClassRooms = subjectteacherclassroomSvc.GetClassRoomsByTeacherGuid(teacherGuid)
            };

        }

        public ExerciseBookResult GetModel(Guid id)
        {
            var ExerciseBookResult = this.Get(id);

            //ExerciseBookResult.Students = studentSvc.GetComboItems(Db);
            //ExerciseBookResult.LessonSchedules = lessonscheduleSvc.GetComboItems(Db);

            return ExerciseBookResult;
        }

        public ExerciseBookResult GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }

        public ExerciseBookResult Get(Guid id)
        {
            return Db.ExerciseBookResults.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<ExerciseBookResult> Get(PageDetail pageDetail)
        {
            var query = Db.ExerciseBookResults.AsQueryable();

            List<Expression<Func<ExerciseBookResult, bool>>> filters = new List<Expression<Func<ExerciseBookResult, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.Student.FirstName + m.Student.FatherName + m.Student.GrandFatherName).Contains(pageDetail.Search));


            Func<ExerciseBookResult, ExerciseBookResult> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }       

        public Response Validate(ExerciseBookResult ExerciseBookResult)
        {
            //if (string.IsNullOrWhiteSpace(ExerciseBookResult..ToString()))
            //    return new FailedResponse("Please provide a valid Student Name.");

            //if (string.IsNullOrWhiteSpace(ExerciseBookResult.MonthGuid.ToString()))
            //    return new FailedResponse("Please provide a valid Month Name.");

            //if (string.IsNullOrWhiteSpace(ExerciseBookResult.ExerciseBookResultDate.ToString()))
            //    return new FailedResponse("Please provide a valid ExerciseBookResult Date.");

            //if (string.IsNullOrWhiteSpace(ExerciseBookResult.ReciptionNumber.ToString()))
            //    return new FailedResponse("Please provide a valid Reciption Number.");

            //if (ExerciseBookResult.TableRowGuid == Guid.Empty)
            //{
            //    var queryStudent = Db.ExerciseBookResults.Where(r => r.StudentGuid == ExerciseBookResult.StudentGuid);
            //    var queryMonth = Db.ExerciseBookResults.Where(r => r.MonthGuid == ExerciseBookResult.MonthGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("A ExerciseBookResult provided has already been created. Please provide a different Student or Month.");
            //}
            //else
            //{
            //    var queryStudent = Db.ExerciseBookResults.Where(r => r.StudentGuid == ExerciseBookResult.StudentGuid && r.TableRowGuid != ExerciseBookResult.TableRowGuid);
            //    var queryMonth = Db.ExerciseBookResults.Where(r => r.MonthGuid == ExerciseBookResult.MonthGuid && r.TableRowGuid != ExerciseBookResult.TableRowGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("An ExerciseBookResult provided has already been created. Please provide a different Student or Month.");
            //}

            return new SuccessResponse("");
        }

        public Response Validate(List<StudentExerciseBookResult> exercisebookResult)
        {

            foreach (StudentExerciseBookResult sebr in exercisebookResult)
            {
                decimal dbValue;
                Int32 intValue;
                if (string.IsNullOrWhiteSpace(sebr.StudentGuid.ToString()))
                    return new FailedResponse("Please provide a valid Student Name.");
                if (string.IsNullOrWhiteSpace(sebr.SubjectTeacherClassRoomGuid.ToString()))
                    return new FailedResponse("Please provide a valid Subject.");
                if (string.IsNullOrWhiteSpace(sebr.Result))
                    return new FailedResponse("Please provide a valid result.");
                if (!decimal.TryParse(sebr.Result, out dbValue))
                    return new FailedResponse("Please provide a valid result.");
                if (string.IsNullOrWhiteSpace(sebr.OutOf))
                    return new FailedResponse("Please provide a valid out of.");
                if (!Int32.TryParse(sebr.OutOf, out intValue))
                    return new FailedResponse("Please provide a valid out of.");
            }
            return new SuccessResponse("");
        }

        public Response Add(ExerciseBookResult ExerciseBookResult, Guid? userid)
        {
            Response response = new Response();

            response = Validate(ExerciseBookResult);
            if (!response.Success) return response;

            ExerciseBookResult.TableRowGuid = Guid.NewGuid();
            ExerciseBookResult.UserId = userid;

            return base.Add(ExerciseBookResult);
        }

        public Response SaveStudentResultByBatch(List<StudentExerciseBookResult> exercisebookResult)
        {
            Response response = new Response();

            response = Validate(exercisebookResult);
            if (!response.Success) return response;

            List<ExerciseBookResult> ebr = new List<ExerciseBookResult>();
            foreach (StudentExerciseBookResult sebr in exercisebookResult)
            {
                ExerciseBookResult x = new ExerciseBookResult();
                x.SubjectTeacherClassRoomGuid = sebr.SubjectTeacherClassRoomGuid;
                x.StudentGuid = sebr.StudentGuid;
                x.Result = Convert.ToDecimal(sebr.Result);
                x.Comment = sebr.Comment;
                x.OutOf = Convert.ToInt32(sebr.OutOf);
                x.TableRowGuid = Guid.NewGuid();

                ebr.Add(x);

            }

            return base.Add(ebr);
        }

        public Response Update(ExerciseBookResult ExerciseBookResult, Guid? userid)
        {
            Response response = new Response();

            response = Validate(ExerciseBookResult);
            if (!response.Success) return response;

            ExerciseBookResult.UserId = userid;

            return base.Update(ExerciseBookResult);
        }

        public Response Delete(ExerciseBookResult ExerciseBookResult, Guid? userid)
        {
            ExerciseBookResult.UserId = userid;
            return base.Delete(ExerciseBookResult);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new ExerciseBookResult() { TableRowGuid = id }, userid);
        }


    }
}
