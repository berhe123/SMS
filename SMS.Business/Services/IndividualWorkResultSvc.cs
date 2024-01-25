using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class IndividualWorkResultSvc : EntityBaseSvc<IndividualWorkResult>, IComboEntitySvc<IndividualWorkResult, IndividualWorkResult, IndividualWorkResult>
    {
        readonly StudentSvc studentSvc;
        readonly IndividualWorkScheduleSvc assignmentscheduleSvc;
        readonly SubjectTeacherClassRoomSvc subjectteacherclassroomSvc;

        public IndividualWorkResultSvc(ILogger logger)
            : base(logger, "IndividualWorkResult")
        {
            studentSvc = new StudentSvc(logger);
            assignmentscheduleSvc = new IndividualWorkScheduleSvc(logger);
            subjectteacherclassroomSvc = new SubjectTeacherClassRoomSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.IndividualWorkResults
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Student.FirstName + " " + m.Student.FatherName + " " + m.Student.GrandFatherName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.IndividualWorkResults
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Student.FirstName + " " + m.Student.FatherName + " " + m.Student.GrandFatherName 
                    };

            return q.AsEnumerable();
        }        

        public IndividualWorkResult GetServiceFilters()
        {
            IndividualWorkResult IndividualWorkResult = new IndividualWorkResult();
            return new IndividualWorkResult();
        }
        public IQueryable<IndividualWorkResult> Get()
        {
            return Db.IndividualWorkResults;
        }
        public IndividualWorkResult GetModel(Guid? userId)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == userId.Value).Select(x => x.TableRowGuid).Single();
            return new IndividualWorkResult()
            {

                ClassRooms = subjectteacherclassroomSvc.GetClassRoomsByTeacherGuid(teacherGuid)

            };
        }
        public IndividualWorkResult GetModelByUserId(Guid? userId)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == userId.Value).Select(x => x.TableRowGuid).Single();

            return new IndividualWorkResult() {
                ClassRooms = subjectteacherclassroomSvc.GetSubjectClassRoomsByTeacherGuid(teacherGuid)              
            };

        }
        public IndividualWorkResult GetModel(Guid id)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == id).Select(x => x.TableRowGuid).Single();
            var IndividualWorkResult = this.Get(id);

            IndividualWorkResult.IndividualWorkSchedules = assignmentscheduleSvc.GetIndividualWorksByteacherId(teacherGuid);
            //IndividualWorkResult.AssignmentSchedules = assignmentscheduleSvc.GetComboItems(Db);

            return IndividualWorkResult;
        }
        public IndividualWorkResult GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUserId(userId) : this.GetModel(id);
        }
        public IndividualWorkResult Get(Guid id)
        {
            return Db.IndividualWorkResults.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<IndividualWorkResult> Get(PageDetail pageDetail)
        {
            var query = Db.IndividualWorkResults.AsQueryable();

            List<Expression<Func<IndividualWorkResult, bool>>> filters = new List<Expression<Func<IndividualWorkResult, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.Student.FirstName + m.Student.FatherName + m.Student.GrandFatherName).Contains(pageDetail.Search));


            Func<IndividualWorkResult, IndividualWorkResult> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(IndividualWorkResult IndividualWorkResult)
        {
            if (string.IsNullOrWhiteSpace(IndividualWorkResult.StudentGuid.ToString()))
                return new FailedResponse("Please provide a valid Student Name.");

            //if (string.IsNullOrWhiteSpace(IndividualWorkResult.MonthGuid.ToString()))
            //    return new FailedResponse("Please provide a valid Month Name.");

            //if (string.IsNullOrWhiteSpace(IndividualWorkResult.IndividualWorkResultDate.ToString()))
            //    return new FailedResponse("Please provide a valid IndividualWorkResult Date.");

            //if (string.IsNullOrWhiteSpace(IndividualWorkResult.ReciptionNumber.ToString()))
            //    return new FailedResponse("Please provide a valid Reciption Number.");

            //if (IndividualWorkResult.TableRowGuid == Guid.Empty)
            //{
            //    var queryStudent = Db.IndividualWorkResults.Where(r => r.StudentGuid == IndividualWorkResult.StudentGuid);
            //    var queryMonth = Db.IndividualWorkResults.Where(r => r.MonthGuid == IndividualWorkResult.MonthGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("A IndividualWorkResult provided has already been created. Please provide a different Student or Month.");
            //}
            //else
            //{
            //    var queryStudent = Db.IndividualWorkResults.Where(r => r.StudentGuid == IndividualWorkResult.StudentGuid && r.TableRowGuid != IndividualWorkResult.TableRowGuid);
            //    var queryMonth = Db.IndividualWorkResults.Where(r => r.MonthGuid == IndividualWorkResult.MonthGuid && r.TableRowGuid != IndividualWorkResult.TableRowGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("An IndividualWorkResult provided has already been created. Please provide a different Student or Month.");
            //}

            return new SuccessResponse("");
        }
        public Response Validate(List<StudentIndividualWorkResult> IndividualWorkResult)
        {

            foreach(StudentIndividualWorkResult sar in IndividualWorkResult)
            {
                decimal dbValue;
                if (string.IsNullOrWhiteSpace(sar.StudentGuid.ToString()))
                    return new FailedResponse("Please provide a valid Student Name.");
                if (string.IsNullOrWhiteSpace(sar.IndividualWorkScheduleGuid.ToString()))
                    return new FailedResponse("Please provide a valid Assignment.");
                if (string.IsNullOrWhiteSpace(sar.Result))
                    return new FailedResponse("Please provide a valid result.");
                if(!decimal.TryParse(sar.Result,out dbValue))
                    return new FailedResponse("Please provide a valid result.");



            
            }
            return new SuccessResponse("");
        }
        public Response Add(IndividualWorkResult IndividualWorkResult, Guid? userid)
        {
            Response response = new Response();

            response = Validate(IndividualWorkResult);
            if (!response.Success) return response;

            IndividualWorkResult.TableRowGuid = Guid.NewGuid();
            IndividualWorkResult.UserId = userid;

            return base.Add(IndividualWorkResult);
        }
        public Response SaveStudentResultByBatch(List<StudentIndividualWorkResult> IndividualWorkResult)
        {
            Response response = new Response();

            response = Validate(IndividualWorkResult);
            if (!response.Success) return response;

            List<IndividualWorkResult> ar = new List<IndividualWorkResult>();
            foreach (StudentIndividualWorkResult sar in IndividualWorkResult)
            {
                IndividualWorkResult x = new IndividualWorkResult();
                x.IndividualWorkScheduleGuid = sar.IndividualWorkScheduleGuid;
                x.StudentGuid = sar.StudentGuid;
                x.Result = Convert.ToDecimal(sar.Result);
                x.Comment = sar.Comment;
                x.TableRowGuid = Guid.NewGuid();

                ar.Add(x);

            }

            return base.Add(ar);
        }      
        public Response Update(IndividualWorkResult IndividualWorkResult, Guid? userid)
        {
            Response response = new Response();

            response = Validate(IndividualWorkResult);
            if (!response.Success) return response;

            IndividualWorkResult.UserId = userid;

            return base.Update(IndividualWorkResult);
        }
        public Response Delete(IndividualWorkResult IndividualWorkResult, Guid? userid)
        {
            IndividualWorkResult.UserId = userid;
            return base.Delete(IndividualWorkResult);
        }
        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new IndividualWorkResult() { TableRowGuid = id }, userid);
        }


    }
}
