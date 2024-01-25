using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class TestResultSvc : EntityBaseSvc<TestResult>, IComboEntitySvc<TestResult, TestResult, TestResult>
    {
        readonly StudentSvc studentSvc;
        readonly TestScheduleSvc testscheduleSvc;
        readonly SubjectTeacherClassRoomSvc subjectteacherclassroomSvc;
        public TestResultSvc(ILogger logger)
            : base(logger, "TestResult")
        {
            studentSvc = new StudentSvc(logger);
            testscheduleSvc = new TestScheduleSvc(logger);
            subjectteacherclassroomSvc = new SubjectTeacherClassRoomSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.TestResults
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Student.FirstName + " " + m.Student.FatherName + " " + m.Student.GrandFatherName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.TestResults
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Student.FirstName + " " + m.Student.FatherName + " " + m.Student.GrandFatherName
                    };

            return q.AsEnumerable();
        }

        public TestResult GetServiceFilters()
        {
            TestResult TestResult = new TestResult();
            return new TestResult();
        }

        public IQueryable<TestResult> Get()
        {
            return Db.TestResults;
        }

        public TestResult GetModel(Guid? userId)
        {
            return new TestResult() {
                //Students = studentSvc.GetComboItems(),
                //TestSchedules = testscheduleSvc.GetComboItems()
            };

        }

        public TestResult GetModelByUserId(Guid? userId)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == userId.Value).Select(x => x.TableRowGuid).Single();
            return new TestResult()
            {
                ClassRooms = subjectteacherclassroomSvc.GetClassRoomsByTeacherGuid(teacherGuid)
            };

        }

        public TestResult GetModel(Guid id)
        {
            var TestResult = this.Get(id);

            //TestResult.Students = studentSvc.GetComboItems(Db);
            //TestResult.TestSchedules = testscheduleSvc.GetComboItems(Db);

            return TestResult;
        }

        public TestResult GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUserId(userId) : this.GetModel(id);
        }

        public TestResult Get(Guid id)
        {
            return Db.TestResults.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<TestResult> Get(PageDetail pageDetail)
        {
            var query = Db.TestResults.AsQueryable();

            List<Expression<Func<TestResult, bool>>> filters = new List<Expression<Func<TestResult, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.Student.FirstName + m.Student.FatherName + m.Student.GrandFatherName).Contains(pageDetail.Search));


            Func<TestResult, TestResult> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(TestResult TestResult)
        {
            if (string.IsNullOrWhiteSpace(TestResult.TestScheduleGuid.ToString()))
                return new FailedResponse("Please provide a valid Student Name.");

            //if (string.IsNullOrWhiteSpace(TestResult.MonthGuid.ToString()))
            //    return new FailedResponse("Please provide a valid Month Name.");

            //if (string.IsNullOrWhiteSpace(TestResult.TestResultDate.ToString()))
            //    return new FailedResponse("Please provide a valid TestResult Date.");

            //if (string.IsNullOrWhiteSpace(TestResult.ReciptionNumber.ToString()))
            //    return new FailedResponse("Please provide a valid Reciption Number.");

            //if (TestResult.TableRowGuid == Guid.Empty)
            //{
            //    var queryStudent = Db.TestResults.Where(r => r.StudentGuid == TestResult.StudentGuid);
            //    var queryMonth = Db.TestResults.Where(r => r.MonthGuid == TestResult.MonthGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("A TestResult provided has already been created. Please provide a different Student or Month.");
            //}
            //else
            //{
            //    var queryStudent = Db.TestResults.Where(r => r.StudentGuid == TestResult.StudentGuid && r.TableRowGuid != TestResult.TableRowGuid);
            //    var queryMonth = Db.TestResults.Where(r => r.MonthGuid == TestResult.MonthGuid && r.TableRowGuid != TestResult.TableRowGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("An TestResult provided has already been created. Please provide a different Student or Month.");
            //}

            return new SuccessResponse("");
        }

        public Response Validate(List<StudentTestResult> testResult)
        {

            foreach (StudentTestResult str in testResult)
            {
                decimal dbValue;
                if (string.IsNullOrWhiteSpace(str.StudentGuid.ToString()))
                    return new FailedResponse("Please provide a valid Student Name.");
                if (string.IsNullOrWhiteSpace(str.TestScheduleGuid.ToString()))
                    return new FailedResponse("Please provide a valid Test.");
                if (string.IsNullOrWhiteSpace(str.Result))
                    return new FailedResponse("Please provide a valid result.");
                if (!decimal.TryParse(str.Result, out dbValue))
                    return new FailedResponse("Please provide a valid result.");
            }
            return new SuccessResponse("");
        }

        public Response Add(TestResult TestResult, Guid? userid)
        {
            Response response = new Response();

            response = Validate(TestResult);
            if (!response.Success) return response;

            TestResult.TableRowGuid = Guid.NewGuid();
            TestResult.UserId = userid;

            return base.Add(TestResult);
        }

        public Response SaveStudentResultByBatch(List<StudentTestResult> testResult)
        {
            Response response = new Response();

            response = Validate(testResult);
            if (!response.Success) return response;

            List<TestResult> tr = new List<TestResult>();
            foreach (StudentTestResult str in testResult)
            {
                TestResult x = new TestResult();
                x.TestScheduleGuid = str.TestScheduleGuid;
                x.StudentGuid = str.StudentGuid;
                x.Result = Convert.ToDecimal(str.Result);
                x.Comment = str.Comment;
                x.TableRowGuid = Guid.NewGuid();

                tr.Add(x);

            }

            return base.Add(tr);
        }

        public Response Update(TestResult TestResult, Guid? userid)
        {
            Response response = new Response();

            response = Validate(TestResult);
            if (!response.Success) return response;

            TestResult.UserId = userid;

            return base.Update(TestResult);
        }

        public Response Delete(TestResult TestResult, Guid? userid)
        {
            TestResult.UserId = userid;
            return base.Delete(TestResult);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new TestResult() { TableRowGuid = id }, userid);
        }


    }
}
