using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class StudentSvc : EntityBaseSvc<Student>, IComboEntitySvc<Student, Student, Student>
    {
        readonly GenderSvc genderSvc;
        readonly UserProfileSvc userprofileSvc;

        public StudentSvc(ILogger logger)
            : base(logger, "Student")
        {
            genderSvc = new GenderSvc(logger);
            userprofileSvc = new UserProfileSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.Students
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.FirstName + " " + m.FatherName+ " " + m.GrandFatherName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.Students
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.FirstName + " " + m.FatherName + " " + m.GrandFatherName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetStudentsByClassRoomId(Guid classroomGuid)
        {
            var q = from m in Db.StudentClassRooms
                    join s in Db.Students on m.StudentGuid equals s.TableRowGuid
                    join c in Db.ClassRooms on m.ClassRoomGuid equals c.TableRowGuid
                    where m.ClassRoomGuid == c.TableRowGuid
                    select new ComboItem()
                    {
                        Value = m.StudentGuid,
                        Text = s.FirstName + " " + s.FatherName + " " + s.GrandFatherName
                    };
            return q.AsEnumerable();
        }
        public Student GetServiceFilters()
        {
            Student Student = new Student();
            return new Student();
        }
        public IQueryable<Student> Get()
        {
            return Db.Students;
        }
        public Student GetModel(Guid? userId)
        {
            return new Student
            {
                Genders = genderSvc.GetComboItems()
            };

        }
        public Student GetModelByUser(Guid? userId)
        {
            return new Student
            {
                Genders = genderSvc.GetComboItems()
            };

        }
        public Student GetModel(Guid id)
        {
            var Student = this.Get(id);

            Student.Genders = genderSvc.GetComboItems(Db);

            return Student;
        }
        public Student GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }
        public Student Get(Guid id)
        {
            return Db.Students.SingleOrDefault(e => e.TableRowGuid == id);
        }
        public JsTable<Student> Get(PageDetail pageDetail)
        {
            var query = Db.Students.AsQueryable();

            List<Expression<Func<Student, bool>>> filters = new List<Expression<Func<Student, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.FirstName.Contains(pageDetail.Search));


            Func<Student, Student> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public JsTable<vw_StudentListByClassRoomId> GetStudentsByClassRoomId(PageDetail pageDetail)
        {
            var query = Db.vw_StudentListByClassRoomId.AsQueryable();

            List<Expression<Func<vw_StudentListByClassRoomId, bool>>> filters = new List<Expression<Func<vw_StudentListByClassRoomId, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.StudentFullName.Contains(pageDetail.Search));

            filters.Add(x => x.ClassRoomGuid == pageDetail.ClassRoomGuid);

            Func<vw_StudentListByClassRoomId, vw_StudentListByClassRoomId> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentList> GetList(PageDetail pageDetail)
        {
            var query = Db.vw_StudentList.AsQueryable();

            List<Expression<Func<vw_StudentList, bool>>> filters = new List<Expression<Func<vw_StudentList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.StudentFullName.Contains(pageDetail.Search));


            Func<vw_StudentList, vw_StudentList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentsForLessonClassDataView> GetStudentsForLessonClassDataView(PageDetail pageDetail)
        {
            Guid studentGuid = Db.Students.Where(x => x.LogInUserId == pageDetail.UserId).Select(x => x.TableRowGuid).Single();
            var query = Db.vw_StudentsForLessonClassDataView.AsQueryable();

            List<Expression<Func<vw_StudentsForLessonClassDataView, bool>>> filters = new List<Expression<Func<vw_StudentsForLessonClassDataView, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.lessonclass.Contains(pageDetail.Search));

            filters.Add(x => x.studentGuid == studentGuid);

            Func<vw_StudentsForLessonClassDataView, vw_StudentsForLessonClassDataView> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentsForIndividualWorkScheduleDataView> GetStudentsForIndividualWorkScheduleDataView(PageDetail pageDetail)
        {
            Guid studentGuid = Db.Students.Where(x => x.LogInUserId == pageDetail.UserId).Select(x => x.TableRowGuid).Single();
            var query = Db.vw_StudentsForIndividualWorkScheduleDataView.AsQueryable();

            List<Expression<Func<vw_StudentsForIndividualWorkScheduleDataView, bool>>> filters = new List<Expression<Func<vw_StudentsForIndividualWorkScheduleDataView, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.IndividualWorkSchedule.Contains(pageDetail.Search));

            filters.Add(x => x.StudentGuid == studentGuid);

            Func<vw_StudentsForIndividualWorkScheduleDataView, vw_StudentsForIndividualWorkScheduleDataView> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentsForQuizScheduleDataView> GetStudentsForQuizScheduleDataView(PageDetail pageDetail)
        {
            Guid studentGuid = Db.Students.Where(x => x.LogInUserId == pageDetail.UserId).Select(x => x.TableRowGuid).Single();
            var query = Db.vw_StudentsForQuizScheduleDataView.AsQueryable();

            List<Expression<Func<vw_StudentsForQuizScheduleDataView, bool>>> filters = new List<Expression<Func<vw_StudentsForQuizScheduleDataView, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.QuizSchdule.Contains(pageDetail.Search));

            filters.Add(x => x.StudentGuid == studentGuid);

            Func<vw_StudentsForQuizScheduleDataView, vw_StudentsForQuizScheduleDataView> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentsForTestScheduleDataView> GetStudentsForTestScheduleDataView(PageDetail pageDetail)
        {
            Guid studentGuid = Db.Students.Where(x => x.LogInUserId == pageDetail.UserId).Select(x => x.TableRowGuid).Single();
            var query = Db.vw_StudentsForTestScheduleDataView.AsQueryable();

            List<Expression<Func<vw_StudentsForTestScheduleDataView, bool>>> filters = new List<Expression<Func<vw_StudentsForTestScheduleDataView, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.TestSchdule.Contains(pageDetail.Search));

            filters.Add(x => x.StudentGuid == studentGuid);

            Func<vw_StudentsForTestScheduleDataView, vw_StudentsForTestScheduleDataView> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentsForExamScheduleDataView> GetStudentsForExamScheduleDataView(PageDetail pageDetail)
        {
            Guid studentGuid = Db.Students.Where(x => x.LogInUserId == pageDetail.UserId).Select(x => x.TableRowGuid).Single();
            var query = Db.vw_StudentsForExamScheduleDataView.AsQueryable();

            List<Expression<Func<vw_StudentsForExamScheduleDataView, bool>>> filters = new List<Expression<Func<vw_StudentsForExamScheduleDataView, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.examSchdule.Contains(pageDetail.Search));

            filters.Add(x => x.StudentGuid == studentGuid);

            Func<vw_StudentsForExamScheduleDataView, vw_StudentsForExamScheduleDataView> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentsForQuizResultDataEntry> GetStudentsForQuizResultDataEntry(PageDetail pageDetail)
        {
            var query = Db.vw_StudentsForQuizResultDataEntry.AsQueryable();

            List<Expression<Func<vw_StudentsForQuizResultDataEntry, bool>>> filters = new List<Expression<Func<vw_StudentsForQuizResultDataEntry, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.StudentFullName).Contains(pageDetail.Search));

            filters.Add(x => x.QuizScheduleGuid == pageDetail.QuizScheduleGuid);

            Func<vw_StudentsForQuizResultDataEntry, vw_StudentsForQuizResultDataEntry> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentsForTestResultDataEntry> GetStudentsForTestResultDataEntry(PageDetail pageDetail)
        {
            var query = Db.vw_StudentsForTestResultDataEntry.AsQueryable();

            List<Expression<Func<vw_StudentsForTestResultDataEntry, bool>>> filters = new List<Expression<Func<vw_StudentsForTestResultDataEntry, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.StudentFullName).Contains(pageDetail.Search));

            filters.Add(x => x.TestScheduleGuid == pageDetail.TestScheduleGuid);

            Func<vw_StudentsForTestResultDataEntry, vw_StudentsForTestResultDataEntry> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentsForExerciseBookResultDataEntry> GetStudentsForExerciseBookResultDataEntry(PageDetail pageDetail)
        {
            var query = Db.vw_StudentsForExerciseBookResultDataEntry.AsQueryable();

            List<Expression<Func<vw_StudentsForExerciseBookResultDataEntry, bool>>> filters = new List<Expression<Func<vw_StudentsForExerciseBookResultDataEntry, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.StudentFullName).Contains(pageDetail.Search));

            filters.Add(x => x.ClassRoomGuid == pageDetail.ClassRoomGuid);

            Func<vw_StudentsForExerciseBookResultDataEntry, vw_StudentsForExerciseBookResultDataEntry> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentsForExamResultDataEntry> GetStudentsForExamResultDataEntry(PageDetail pageDetail)
        {
            var query = Db.vw_StudentsForExamResultDataEntry.AsQueryable();

            List<Expression<Func<vw_StudentsForExamResultDataEntry, bool>>> filters = new List<Expression<Func<vw_StudentsForExamResultDataEntry, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.StudentFullName).Contains(pageDetail.Search));

            Func<vw_StudentsForExamResultDataEntry, vw_StudentsForExamResultDataEntry> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentInfromationDataView> GetStudentsForInfromationDataView(PageDetail pageDetail)
        {
            Guid studentGuid = Db.Students.Where(x => x.LogInUserId == pageDetail.UserId).Select(x => x.TableRowGuid).Single();
            var query = Db.vw_StudentInfromationDataView.AsQueryable();

            List<Expression<Func<vw_StudentInfromationDataView, bool>>> filters = new List<Expression<Func<vw_StudentInfromationDataView, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.Information).Contains(pageDetail.Search));

            filters.Add(x => x.studentGuid == studentGuid);

            Func<vw_StudentInfromationDataView, vw_StudentInfromationDataView> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentPaymentDataView> GetStudentsForPaymentDataView(PageDetail pageDetail)
        {
            Guid studentGuid = Db.Students.Where(x => x.LogInUserId == pageDetail.UserId).Select(x => x.TableRowGuid).Single();
            var query = Db.vw_StudentPaymentDataView.AsQueryable();

            List<Expression<Func<vw_StudentPaymentDataView, bool>>> filters = new List<Expression<Func<vw_StudentPaymentDataView, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.payment).Contains(pageDetail.Search));

            filters.Add(x => x.StudentGuid == studentGuid);

            Func<vw_StudentPaymentDataView, vw_StudentPaymentDataView> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentUnPaymentDataView> GetStudentsForUnPaymentDataView(PageDetail pageDetail)
        {
            Guid studentGuid = Db.Students.Where(x => x.LogInUserId == pageDetail.UserId).Select(x => x.TableRowGuid).Single();
            var query = Db.vw_StudentUnPaymentDataView.AsQueryable();

            List<Expression<Func<vw_StudentUnPaymentDataView, bool>>> filters = new List<Expression<Func<vw_StudentUnPaymentDataView, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.StudentName).Contains(pageDetail.Search));

            filters.Add(x => x.studentGuidOnStudents == studentGuid);
            filters.Add(y => y.paymentGuid == null);


            Func<vw_StudentUnPaymentDataView, vw_StudentUnPaymentDataView> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentsForIndividualWorkResultDataView> GetStudentsForIndividualWorkResultDataView(PageDetail pageDetail)
        {
            Guid studentGuid = Db.Students.Where(x => x.LogInUserId == pageDetail.UserId).Select(x => x.TableRowGuid).Single();
            var query = Db.vw_StudentsForIndividualWorkResultDataView.AsQueryable();

            List<Expression<Func<vw_StudentsForIndividualWorkResultDataView, bool>>> filters = new List<Expression<Func<vw_StudentsForIndividualWorkResultDataView, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.StudentFullName.Contains(pageDetail.Search));

            filters.Add(x => x.StudentGuid == studentGuid);

            Func<vw_StudentsForIndividualWorkResultDataView, vw_StudentsForIndividualWorkResultDataView> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentsForQuizResultDataView> GetStudentsForQuizResultDataView(PageDetail pageDetail)
        {
            Guid studentGuid = Db.Students.Where(x => x.LogInUserId == pageDetail.UserId).Select(x => x.TableRowGuid).Single();
            var query = Db.vw_StudentsForQuizResultDataView.AsQueryable();

            List<Expression<Func<vw_StudentsForQuizResultDataView, bool>>> filters = new List<Expression<Func<vw_StudentsForQuizResultDataView, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.Quiz.Contains(pageDetail.Search));

            filters.Add(x => x.StudentGuid == studentGuid);

            Func<vw_StudentsForQuizResultDataView, vw_StudentsForQuizResultDataView> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentsForTestResultDataView> GetStudentsForTestResultDataView(PageDetail pageDetail)
        {
            Guid studentGuid = Db.Students.Where(x => x.LogInUserId == pageDetail.UserId).Select(x => x.TableRowGuid).Single();
            var query = Db.vw_StudentsForTestResultDataView.AsQueryable();

            List<Expression<Func<vw_StudentsForTestResultDataView, bool>>> filters = new List<Expression<Func<vw_StudentsForTestResultDataView, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.Test.Contains(pageDetail.Search));

            filters.Add(x => x.StudentGuid == studentGuid);

            Func<vw_StudentsForTestResultDataView, vw_StudentsForTestResultDataView> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentsForExerciseBookResultDataView> GetStudentsForExerciseBookResultDataView(PageDetail pageDetail)
        {
            Guid studentGuid = Db.Students.Where(x => x.LogInUserId == pageDetail.UserId).Select(x => x.TableRowGuid).Single();
            var query = Db.vw_StudentsForExerciseBookResultDataView.AsQueryable();

            List<Expression<Func<vw_StudentsForExerciseBookResultDataView, bool>>> filters = new List<Expression<Func<vw_StudentsForExerciseBookResultDataView, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.ExerciseBook.Contains(pageDetail.Search));

            filters.Add(x => x.StudentGuid == studentGuid);

            Func<vw_StudentsForExerciseBookResultDataView, vw_StudentsForExerciseBookResultDataView> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentsForExamResultDataView> GetStudentsForExamResultDataView(PageDetail pageDetail)
        {
            Guid studentGuid = Db.Students.Where(x => x.LogInUserId == pageDetail.UserId).Select(x => x.TableRowGuid).Single();
            var query = Db.vw_StudentsForExamResultDataView.AsQueryable();

            List<Expression<Func<vw_StudentsForExamResultDataView, bool>>> filters = new List<Expression<Func<vw_StudentsForExamResultDataView, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.StudentFullName.Contains(pageDetail.Search));

            filters.Add(x => x.StudentGuid == studentGuid);

            Func<vw_StudentsForExamResultDataView, vw_StudentsForExamResultDataView> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentsForIndividualWorkResultDataEntry> GetStudentsForIndividualWorkResultDataEntry(PageDetail pageDetail)
        {
            var query = Db.vw_StudentsForIndividualWorkResultDataEntry.AsQueryable();

            List<Expression<Func<vw_StudentsForIndividualWorkResultDataEntry, bool>>> filters = new List<Expression<Func<vw_StudentsForIndividualWorkResultDataEntry, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.StudentFullName).Contains(pageDetail.Search));

            filters.Add(x => x.IndividualWorkScheduleGuid == pageDetail.IndividualWorkScheduleGuid);

            Func<vw_StudentsForIndividualWorkResultDataEntry, vw_StudentsForIndividualWorkResultDataEntry> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }  

        public Response Validate(Student Student)
        {
            if (string.IsNullOrWhiteSpace(Student.FirstName))
                return new FailedResponse("Please provide a valid First Name.");

            if (string.IsNullOrWhiteSpace(Student.FatherName))
                return new FailedResponse("Please provide a valid Father Name.");

            if (string.IsNullOrWhiteSpace(Student.GrandFatherName))
                return new FailedResponse("Please provide a valid Grand Father Name.");

            if (string.IsNullOrWhiteSpace(Student.BirthDate.ToString()))
                return new FailedResponse("Please provide a valid Birth Date.");

            if (string.IsNullOrWhiteSpace(Student.GenderGuid.ToString()))
                return new FailedResponse("Please provide a valid Sex.");

            if (string.IsNullOrWhiteSpace(Student.RegistrationDate.ToString()))
                return new FailedResponse("Please provide a valid Registration Date.");

            //if (char.MaxValue.Equals(5))
            //{
            //    return new FailedResponse("Please provide a letter only for First Name.");
            //}

            //if (Student.TableRowGuid == Guid.Empty)
            //{
            //    var query = Db.Students.Where(r => r.StudentName == Student.StudentName);
            //    if (query.ToList().Count > 0)
            //        return new FailedResponse("A Student Name provided has already been created. Please provide a different name.");
            //}
            //else
            //{
            //    var query = Db.Students.Where(r => r.StudentName == Student.StudentName && r.TableRowGuid != Student.TableRowGuid);
            //    if (query.ToList().Count > 0)
            //        return new FailedResponse("An Student Name provided has already been created. Please provide a different name.");
            //}

            return new SuccessResponse("");
        }
        public Response Add(Student Student, Guid? userid)
        {
            Response response = new Response();

            response = Validate(Student);
            if (!response.Success) return response;

            Student.TableRowGuid = Guid.NewGuid();
            Student.UserId = userid;            

            return base.Add(Student);
        }
        public Response Update(Student Student, Guid? userid)
        {
            Response response = new Response();

            response = Validate(Student);
            if (!response.Success) return response;

            Student.UserId = userid;
            
             return base.Update(Student);            
        }
        public Response Delete(Student Student, Guid? userid)
        {
            Student.UserId = userid;
            return base.Delete(Student);
        }
        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new Student() { TableRowGuid = id }, userid);
        }


    }
}
