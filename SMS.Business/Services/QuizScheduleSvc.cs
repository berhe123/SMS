using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class QuizScheduleSvc : EntityBaseSvc<QuizSchedule>, IComboEntitySvc<QuizSchedule, QuizSchedule, QuizSchedule>
    {
        readonly LessonClassSvc lessonscheduleSvc;
        public QuizScheduleSvc(ILogger logger)
            : base(logger, "QuizSchedule")
        {
            lessonscheduleSvc = new LessonClassSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.QuizSchedules
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.LessonClass.SubjectTeacherClassRoom.Teacher.FirstName + " " + m.LessonClass.SubjectTeacherClassRoom.Teacher.FatherName + " " + m.LessonClass.SubjectTeacherClassRoom.Teacher.GrandFatherName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.QuizSchedules
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

            var q = from m in Db.QuizSchedules
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
            var q = from m in Db.QuizSchedules
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

            var q = from m in Db.QuizSchedules
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
            var q = from m in Db.QuizSchedules
                    join s in Db.Subjects on m.LessonClass.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                    where m.LessonClass.SubjectTeacherClassRoom.ClassRoomGuid == classroomGuid
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = s.SubjectName
                    };
            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetQuizsByClassRoomId(Guid userId, Guid classroomId)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == userId).Select(x => x.TableRowGuid).Single();
            var q = from m in Db.QuizSchedules
                    join c in Db.ClassRooms on m.LessonClass.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    join t  in Db.Teachers on m.LessonClass.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                    join s in Db.Subjects on m .LessonClass.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                    where (c.TableRowGuid == classroomId) 
                    && (t.TableRowGuid == teacherGuid)
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = s.SubjectName + " => " + m.QuizName + " out of " + m.OutOf.ToString()
                    };
            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetQuizsByTeacherUserId(string userId)
        {
            Guid userGuid = Guid.Parse(userId);
            Guid TeacherGuid = Db.Teachers.Where(m => m.LogInUserId == userGuid).Select(x => x.TableRowGuid).Single();

            var q = from m in Db.QuizSchedules
                    join s in Db.Subjects on m.LessonClass.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                    join c in Db.ClassRooms on m.LessonClass.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    join t in Db.Teachers on m.LessonClass.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                    where t.TableRowGuid == TeacherGuid
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.QuizName + " on " + s.SubjectName + " for " + c.ClassRoomName
                    };
            return q.AsEnumerable();
        }

        public QuizSchedule GetServiceFilters()
        {
            QuizSchedule QuizSchedule = new QuizSchedule();
            return new QuizSchedule();
        }

        public IQueryable<QuizSchedule> Get()
        {
            return Db.QuizSchedules;
        }

        public QuizSchedule GetModel(Guid? userId)
        {
            return new QuizSchedule() {
                //LessonSchedules = lessonscheduleSvc.GetComboItems()            
            };

        }

        public QuizSchedule GetModelByUser(Guid? userId)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == userId.Value).Select(x => x.TableRowGuid).Single();
            return new QuizSchedule()
            {
                ClassRoomSubjects = lessonscheduleSvc.GetClassRoomSubjectsByTeacherGuid(teacherGuid)            
            };

        }

        public QuizSchedule GetModel(Guid id)
        {
            var QuizSchedule = this.Get(id);
            Guid TeacherGuid = Db.QuizSchedules.Where(x => x.TableRowGuid == id).Select(x => x.LessonClass.SubjectTeacherClassRoom.TeacherGuid).Single();
            QuizSchedule.ClassRoomSubjects = lessonscheduleSvc.GetClassRoomSubjectsByTeacherGuid(TeacherGuid);
            QuizSchedule.classroomsubjectGuid = (from cs in Db.LessonClasses
                                                 where cs.TableRowGuid == QuizSchedule.LessonClassGuid
                                                 select cs.SubjectTeacherClassRoom.SubjectGuid).Single();
            QuizSchedule.Periods = lessonscheduleSvc.GetPeriodsBysubjectGuid(TeacherGuid, QuizSchedule.classroomsubjectGuid);
            return QuizSchedule;
        }

        public QuizSchedule GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }

        public QuizSchedule Get(Guid id)
        {
            return Db.QuizSchedules.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<QuizSchedule> Get(PageDetail pageDetail)
        {
            var query = Db.QuizSchedules.AsQueryable();

            List<Expression<Func<QuizSchedule, bool>>> filters = new List<Expression<Func<QuizSchedule, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.LessonClass.SubjectTeacherClassRoom.Teacher.FirstName + " " + m.LessonClass.SubjectTeacherClassRoom.Teacher.FatherName + " " + m.LessonClass.SubjectTeacherClassRoom.Teacher.GrandFatherName).Contains(pageDetail.Search));


            Func<QuizSchedule, QuizSchedule> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public JsTable<vw_QuizScheduleList> GetList(PageDetail pageDetail)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == pageDetail.UserId).Select(x => x.TableRowGuid).Single();

            var query = Db.vw_QuizScheduleList.AsQueryable();

            List<Expression<Func<vw_QuizScheduleList, bool>>> filters = new List<Expression<Func<vw_QuizScheduleList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.Quiz.Contains(pageDetail.Search));

            filters.Add(x => x.TeacherGuid == teacherGuid);

            Func<vw_QuizScheduleList, vw_QuizScheduleList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(QuizSchedule QuizSchedule)
        {
            if (string.IsNullOrWhiteSpace(QuizSchedule.LessonClassGuid.ToString()))
                return new FailedResponse("Please provide a valid Student Name.");

            //if (string.IsNullOrWhiteSpace(QuizSchedule.MonthGuid.ToString()))
            //    return new FailedResponse("Please provide a valid Month Name.");

            //if (string.IsNullOrWhiteSpace(QuizSchedule.QuizScheduleDate.ToString()))
            //    return new FailedResponse("Please provide a valid QuizSchedule Date.");

            //if (string.IsNullOrWhiteSpace(QuizSchedule.ReciptionNumber.ToString()))
            //    return new FailedResponse("Please provide a valid Reciption Number.");

            //if (QuizSchedule.TableRowGuid == Guid.Empty)
            //{
            //    var queryStudent = Db.QuizSchedules.Where(r => r.StudentGuid == QuizSchedule.StudentGuid);
            //    var queryMonth = Db.QuizSchedules.Where(r => r.MonthGuid == QuizSchedule.MonthGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("A QuizSchedule provided has already been created. Please provide a different Student or Month.");
            //}
            //else
            //{
            //    var queryStudent = Db.QuizSchedules.Where(r => r.StudentGuid == QuizSchedule.StudentGuid && r.TableRowGuid != QuizSchedule.TableRowGuid);
            //    var queryMonth = Db.QuizSchedules.Where(r => r.MonthGuid == QuizSchedule.MonthGuid && r.TableRowGuid != QuizSchedule.TableRowGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("An QuizSchedule provided has already been created. Please provide a different Student or Month.");
            //}

            return new SuccessResponse("");
        }

        public Response Add(QuizSchedule QuizSchedule, Guid? userid)
        {
            Response response = new Response();

            response = Validate(QuizSchedule);
            if (!response.Success) return response;

            QuizSchedule.TableRowGuid = Guid.NewGuid();
            QuizSchedule.UserId = userid;
            
            return base.Add(QuizSchedule);
        }

        public Response Update(QuizSchedule QuizSchedule, Guid? userid)
        {
            Response response = new Response();

            response = Validate(QuizSchedule);
            if (!response.Success) return response;

            QuizSchedule.UserId = userid;

            return base.Update(QuizSchedule);
        }

        public Response Delete(QuizSchedule QuizSchedule, Guid? userid)
        {
            QuizSchedule.UserId = userid;
            return base.Delete(QuizSchedule);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new QuizSchedule() { TableRowGuid = id }, userid);
        }


    }
}
