using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class TestScheduleSvc : EntityBaseSvc<TestSchedule>, IComboEntitySvc<TestSchedule, TestSchedule, TestSchedule>
    {
        readonly LessonClassSvc LessonClassSvc;

        public TestScheduleSvc(ILogger logger)
            : base(logger, "TestSchedule")
        {
            LessonClassSvc = new LessonClassSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.TestSchedules
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.LessonClass.SubjectTeacherClassRoom.Teacher.FirstName + " " + m.LessonClass.SubjectTeacherClassRoom.Teacher.FatherName + " " + m.LessonClass.SubjectTeacherClassRoom.Teacher.GrandFatherName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.TestSchedules
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.LessonClass.SubjectTeacherClassRoom.Teacher.FirstName + " " + m.LessonClass.SubjectTeacherClassRoom.Teacher.FatherName + " " + m.LessonClass.SubjectTeacherClassRoom.Teacher.GrandFatherName 
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetClassRoomsByTeacherId()
        {
            Guid teacherId = new Guid();

            var q = from m in Db.TestSchedules
                    join c in Db.ClassRooms on m.LessonClass.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    where m.LessonClass.SubjectTeacherClassRoom.TeacherGuid == teacherId
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = c.ClassRoomName
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetClassRoomsByTeacherId(Guid teacherId)
        {
            var q = from m in Db.TestSchedules
                    join c in Db.ClassRooms on m.LessonClass.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    where m.LessonClass.SubjectTeacherClassRoom.TeacherGuid == teacherId
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = c.ClassRoomName
                    };
            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetSubjectsByClassRoomId()
        {
            Guid classroomGuid = new Guid();

            var q = from m in Db.TestSchedules
                    join s in Db.Subjects on m.LessonClass.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                    where m.LessonClass.SubjectTeacherClassRoom.ClassRoomGuid == classroomGuid
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = s.SubjectName
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetSubjectsByClassRoomId(Guid classroomGuid)
        {
            var q = from m in Db.TestSchedules
                    join s in Db.Subjects on m.LessonClass.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                    where m.LessonClass.SubjectTeacherClassRoom.ClassRoomGuid == classroomGuid
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = s.SubjectName
                    };
            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetTestsByClassRoomId(Guid userId, Guid classroomId)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == userId).Select(x => x.TableRowGuid).Single();
            var q = from m in Db.TestSchedules
                    join c in Db.ClassRooms on m.LessonClass.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    join t in Db.Teachers on m.LessonClass.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                    join s in Db.Subjects on m.LessonClass.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                    where (c.TableRowGuid == classroomId)
                    && (t.TableRowGuid == teacherGuid)
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = s.SubjectName + " => " + m.TestName + " out of " + m.OutOf.ToString()
                    };
            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetTestsByTeacherUserId(string userId)
        {
            Guid userGuid = Guid.Parse(userId);
            Guid TeacherGuid = Db.Teachers.Where(m => m.LogInUserId == userGuid).Select(x => x.TableRowGuid).Single();

            var q = from m in Db.TestSchedules
                    join s in Db.Subjects on m.LessonClass.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                    join c in Db.ClassRooms on m.LessonClass.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    join t in Db.Teachers on m.LessonClass.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                    where t.TableRowGuid == TeacherGuid
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.TestName + " on " + s.SubjectName + " for " + c.ClassRoomName
                    };
            return q.AsEnumerable();
        }

        public TestSchedule GetServiceFilters()
        {
            TestSchedule TestSchedule = new TestSchedule();
            return new TestSchedule();
        }

        public IQueryable<TestSchedule> Get()
        {
            return Db.TestSchedules;
        }

        public TestSchedule GetModel(Guid? userId)
        {
            return new TestSchedule() {
                //LessonClasss = LessonClassSvc.GetComboItems()            
            };

        }

        public TestSchedule GetModelByUserId(Guid? userId)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == userId.Value).Select(x => x.TableRowGuid).Single();
            return new TestSchedule()
            {
                ClassRoomSubjects = LessonClassSvc.GetClassRoomSubjectsByTeacherGuid(teacherGuid)
            };

        }

        public TestSchedule GetModel(Guid id)
        {
            var TestSchedule = this.Get(id);

            Guid TeacherGuid = Db.TestSchedules.Where(x => x.TableRowGuid == id).Select(x => x.LessonClass.SubjectTeacherClassRoom.TeacherGuid).Single();
            TestSchedule.ClassRoomSubjects = LessonClassSvc.GetClassRoomSubjectsByTeacherGuid(TeacherGuid);
            TestSchedule.classroomsubjectGuid = (from cs in Db.LessonClasses
                                                 where cs.TableRowGuid == TestSchedule.LessonClassGuid
                                                 select cs.SubjectTeacherClassRoom.SubjectGuid).Single();
            TestSchedule.Periods = LessonClassSvc.GetPeriodsBysubjectGuid(TeacherGuid, TestSchedule.classroomsubjectGuid);

            return TestSchedule;
        }

        public TestSchedule GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUserId(userId) : this.GetModel(id);
        }

        public TestSchedule Get(Guid id)
        {
            return Db.TestSchedules.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<TestSchedule> Get(PageDetail pageDetail)
        {
            var query = Db.TestSchedules.AsQueryable();

            List<Expression<Func<TestSchedule, bool>>> filters = new List<Expression<Func<TestSchedule, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.LessonClass.SubjectTeacherClassRoom.Teacher.FirstName + " " + m.LessonClass.SubjectTeacherClassRoom.Teacher.FatherName + " " + m.LessonClass.SubjectTeacherClassRoom.Teacher.GrandFatherName).Contains(pageDetail.Search));


            Func<TestSchedule, TestSchedule> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public JsTable<vw_TestScheduleList> GetList(PageDetail pageDetail)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == pageDetail.UserId).Select(x => x.TableRowGuid).Single();

            var query = Db.vw_TestScheduleList.AsQueryable();

            List<Expression<Func<vw_TestScheduleList, bool>>> filters = new List<Expression<Func<vw_TestScheduleList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.Test.Contains(pageDetail.Search));

            filters.Add(x => x.TeacherGuid == teacherGuid);
            Func<vw_TestScheduleList, vw_TestScheduleList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(TestSchedule TestSchedule)
        {
            if (string.IsNullOrWhiteSpace(TestSchedule.LessonClassGuid.ToString()))
                return new FailedResponse("Please provide a valid Student Name.");

            //if (string.IsNullOrWhiteSpace(TestSchedule.MonthGuid.ToString()))
            //    return new FailedResponse("Please provide a valid Month Name.");

            //if (string.IsNullOrWhiteSpace(TestSchedule.TestScheduleDate.ToString()))
            //    return new FailedResponse("Please provide a valid TestSchedule Date.");

            //if (string.IsNullOrWhiteSpace(TestSchedule.ReciptionNumber.ToString()))
            //    return new FailedResponse("Please provide a valid Reciption Number.");

            //if (TestSchedule.TableRowGuid == Guid.Empty)
            //{
            //    var queryStudent = Db.TestSchedules.Where(r => r.StudentGuid == TestSchedule.StudentGuid);
            //    var queryMonth = Db.TestSchedules.Where(r => r.MonthGuid == TestSchedule.MonthGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("A TestSchedule provided has already been created. Please provide a different Student or Month.");
            //}
            //else
            //{
            //    var queryStudent = Db.TestSchedules.Where(r => r.StudentGuid == TestSchedule.StudentGuid && r.TableRowGuid != TestSchedule.TableRowGuid);
            //    var queryMonth = Db.TestSchedules.Where(r => r.MonthGuid == TestSchedule.MonthGuid && r.TableRowGuid != TestSchedule.TableRowGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("An TestSchedule provided has already been created. Please provide a different Student or Month.");
            //}

            return new SuccessResponse("");
        }

        public Response Add(TestSchedule TestSchedule, Guid? userid)
        {
            Response response = new Response();

            response = Validate(TestSchedule);
            if (!response.Success) return response;

            TestSchedule.TableRowGuid = Guid.NewGuid();
            TestSchedule.UserId = userid;

            return base.Add(TestSchedule);
        }

        public Response Update(TestSchedule TestSchedule, Guid? userid)
        {
            Response response = new Response();

            response = Validate(TestSchedule);
            if (!response.Success) return response;

            TestSchedule.UserId = userid;

            return base.Update(TestSchedule);
        }

        public Response Delete(TestSchedule TestSchedule, Guid? userid)
        {
            TestSchedule.UserId = userid;
            return base.Delete(TestSchedule);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new TestSchedule() { TableRowGuid = id }, userid);
        }


    }
}
