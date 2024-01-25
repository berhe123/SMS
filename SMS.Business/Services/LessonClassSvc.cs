using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class LessonClassSvc : EntityBaseSvc<LessonClass>, IComboEntitySvc<LessonClass, LessonClass, LessonClass>
    {
        readonly SubjectTeacherClassRoomSvc subjectTeacherclassroomSvc;
        readonly TeacherSvc teacherSvc;
        readonly DaySvc daySvc;
        readonly PeriodSvc periodSvc;

        public LessonClassSvc(ILogger logger)
            : base(logger, "LessonClass")
        {
            subjectTeacherclassroomSvc = new SubjectTeacherClassRoomSvc(logger);
            teacherSvc = new TeacherSvc(logger);
            daySvc = new DaySvc(logger);
            periodSvc = new PeriodSvc(logger);          
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.LessonClasses
                    select new ComboItem()
                    {
                        Value = m.SubjectTeacherClassRoom.TeacherGuid,
                        Text = m.SubjectTeacherClassRoom.Teacher.FirstName + " " + m.SubjectTeacherClassRoom.Teacher.FatherName + " " + m.SubjectTeacherClassRoom.Teacher.GrandFatherName+" teaches "+m.SubjectTeacherClassRoom.Subject.SubjectName+" for "+m.SubjectTeacherClassRoom.ClassRoom.ClassRoomName+" on "+m.Day.DayName+" at "+m.Period.PeriodName+" from "+m.Period.TimeFrom+" to "+m.Period.TimeTo
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.LessonClasses
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.SubjectTeacherClassRoom.Teacher.FirstName + " " + m.SubjectTeacherClassRoom.Teacher.FatherName + " " + m.SubjectTeacherClassRoom.Teacher.GrandFatherName + " teaches " + m.SubjectTeacherClassRoom.Subject.SubjectName + " for " + m.SubjectTeacherClassRoom.ClassRoom.ClassRoomName + " on " + m.Day.DayName + " at " + m.Period.PeriodName + " from " + m.Period.TimeFrom + " to " + m.Period.TimeTo
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetClassRoomsByTeacherUserId()
        {
            Guid teacherId = new Guid();

            var q = from m in Db.LessonClasses
                    join c in Db.ClassRooms on m.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    join t in Db.Teachers on m.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                    where t.TableRowGuid == teacherId
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = c.ClassRoomName
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetClassRoomsByTeacherUserId(Guid teacherId)
        {
            var q = from m in Db.LessonClasses
                    join c in Db.ClassRooms on m.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    join t in Db.Teachers on m.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                    where t.TableRowGuid == teacherId
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = c.ClassRoomName
                    };
            return q.AsEnumerable();
        }        
        public IEnumerable<ComboItem> GetPeriodBySubjectId()
        {
            Guid subjectId = new Guid();

            var q = from m in Db.LessonClasses 
                    join d in Db.Days on m.DayGuid equals d.TableRowGuid
                    join p in Db.Periods on m.PeriodGuid equals p.TableRowGuid
                    where m.SubjectTeacherClassRoom.SubjectGuid == subjectId
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = d.DayName + " => "+ p.Session.SessionName + " at " + p.PeriodName
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetPeriodBySubjectId(Guid subjectId)
        {
            var q = from m in Db.LessonClasses
                    join d in Db.Days on m.DayGuid equals d.TableRowGuid
                    join p in Db.Periods on m.PeriodGuid equals p.TableRowGuid
                    where m.SubjectTeacherClassRoom.SubjectGuid == subjectId
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = d.DayName + " => " + p.Session.SessionName + " at " + p.PeriodName
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetTeacherByLessonClassId()
        {
            Guid LessonClassId = new Guid();

            var q = from m in Db.LessonClasses
                    join t in Db.Teachers on m.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                    where m.TableRowGuid == LessonClassId
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = t.FirstName + " " + t.FatherName + " " + t.GrandFatherName
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetTeacherByLessonClassId(Guid LessonClassId)
        {
            var q = from m in Db.LessonClasses
                    join t in Db.Teachers on m.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                    where m.TableRowGuid == LessonClassId
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = t.FirstName + " " + t.FatherName + " " + t.GrandFatherName
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetPeriodsByLessonClassId()
        {
            Guid lessonclassId = new Guid();

            var q = from m in Db.LessonClasses
                    join d in Db.Days on m.DayGuid equals d.TableRowGuid
                    join s in Db.Sessions on m.Period.SessionGuid equals s.TableRowGuid
                    join p in Db.Periods on m.PeriodGuid equals p.TableRowGuid
                    where m.TableRowGuid == lessonclassId
                    select new ComboItem
                    {
                        Value = m.TableRowGuid,
                        Text = d.DayName + " at " + s.SessionName + " => " + p.PeriodName
                    };
            return q.AsQueryable();
        }
        public IEnumerable<ComboItem> GetPeriodsBySubjectGuid(Guid userGuid, Guid subjectGuid)
        {
            Guid TeacherGuid = Db.Teachers.Where(x => x.LogInUserId == userGuid).Select(x => x.TableRowGuid).Single();

            var q = from m in Db.LessonClasses
                    join d in Db.Days on m.DayGuid equals d.TableRowGuid
                    join s in Db.Sessions on m.Period.SessionGuid equals s.TableRowGuid
                    join p in Db.Periods on m.PeriodGuid equals p.TableRowGuid
                    where (m.SubjectTeacherClassRoom.SubjectGuid == subjectGuid) 
                    && (m.SubjectTeacherClassRoom.TeacherGuid == TeacherGuid)
                    select new ComboItem { 
                        Value = m.TableRowGuid,
                        Text = d.DayName + " at " + s.SessionName + " => " + p.PeriodName                    
                    };
            return q.AsQueryable();
        }
        public IEnumerable<ComboItem> GetPeriodsBysubjectGuid(Guid teacherGuid, Guid subjectGuid)
        {
            var q = from m in Db.LessonClasses
                    join d in Db.Days on m.DayGuid equals d.TableRowGuid
                    join s in Db.Sessions on m.Period.SessionGuid equals s.TableRowGuid
                    join p in Db.Periods on m.PeriodGuid equals p.TableRowGuid
                    where (m.SubjectTeacherClassRoom.SubjectGuid == subjectGuid)
                    && (m.SubjectTeacherClassRoom.TeacherGuid == teacherGuid)
                    select new ComboItem
                    {
                        Value = m.TableRowGuid,
                        Text = d.DayName + " at " + s.SessionName + " => " + p.PeriodName
                    };
            return q.AsQueryable();
        }
        public IEnumerable<ComboItem> GetClassRoomSubjectsByTeacherGuid(Guid teacherGuid)
        {
            var q = from l in Db.LessonClasses
                        join c in Db.ClassRooms on l.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                        join s in Db.Subjects on l.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                        join t in Db.Teachers on l.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                        where t.TableRowGuid == teacherGuid
                        select new ComboItem
                        {
                            Value = l.SubjectTeacherClassRoom.SubjectGuid,
                            Text = c.ClassRoomName + " => " + s.SubjectName
                        };
            return q.AsEnumerable();
        }
        
        public IEnumerable<ComboItem> GetSubjectsByTeacherUserIdandClassRoomId(Guid userId, Guid classroomId)
        {           
            var query = from l in Db.LessonClasses
                        join c in Db.ClassRooms on l.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                        join s in Db.Subjects on l.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                        join t in Db.Teachers on l.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                        where (t.TableRowGuid == userId) 
                        && (c.TableRowGuid == classroomId)
                        select new ComboItem
                        {
                            Value = l.TableRowGuid,
                            Text = s.SubjectName
                        };
            return query.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetLessonClassesByTeacherUserIdandClassRoomId(string userId, Guid classroomId)
        {
            Guid userGuid = Guid.Parse(userId);
            Guid TeacherGuid = Db.Teachers.Where(m => m.LogInUserId == userGuid).Select(x => x.TableRowGuid).Single();

            var query = from l in Db.LessonClasses
                        join c in Db.ClassRooms on l.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                        join s in Db.Subjects on l.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                        join t in Db.Teachers on l.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                        join d in Db.Days on l.DayGuid equals d.TableRowGuid
                        join se in Db.Sessions on l.Period.SessionGuid equals se.TableRowGuid
                        join p in Db.Periods on l.PeriodGuid equals p.TableRowGuid
                        where (t.TableRowGuid == TeacherGuid) && (c.TableRowGuid == classroomId)
                        select new ComboItem
                        {
                            Value = l.TableRowGuid,
                            Text = s.SubjectName + " on " + d.DayName + " at " + se.SessionName + " => " + p.PeriodName + " from " + p.TimeFrom.ToString() + " to " + p.TimeTo.ToString()
                         };
            return query.AsEnumerable();
        }

        public LessonClass GetServiceFilters()
        {
            LessonClass LessonClass = new LessonClass();
            return new LessonClass();
        }
        public IQueryable<LessonClass> Get()
        {
            return Db.LessonClasses;
        }
        public LessonClass GetModel(Guid? userId)
        {
            //Guid teacherId = new Guid();
            return new LessonClass
            {
                Teachers = subjectTeacherclassroomSvc.GetComboItems(),
                //ClassRoomSubjects = subjectTeacherclassroomSvc.GetClassRoomSubjectByTeacherId(teacherId),
                Days = daySvc.GetComboItems(),
                Periods = periodSvc.GetComboItems()
            };
        }
        public LessonClass GetModelByUser(Guid? userId)
        {
            var lessclassroom = this.Get(userId);                             

            return new LessonClass
            {
                Teachers = teacherSvc.GetComboItems(),                
                Days = daySvc.GetComboItems(),
                Periods = periodSvc.GetComboItems()
            };
        }
        public LessonClass GetModel(Guid id)
        {
            var LessonClass = this.Get(id);

                LessonClass.Teachers = teacherSvc.GetComboItems(Db);                           
                LessonClass.TeacherGuid = (from t in Db.SubjectTeacherClassRooms
                                           where t.TableRowGuid == LessonClass.SubjectTeacherClassRoomGuid
                                           select t.TeacherGuid).Single();
                LessonClass.ClassRooms = subjectTeacherclassroomSvc.GetClassRoomSubjectsByTeacherGuid(LessonClass.TeacherGuid);                     
                LessonClass.Days = daySvc.GetComboItems(Db);
                LessonClass.Periods = periodSvc.GetComboItems(Db);
                  
            return LessonClass;
        }
        public LessonClass GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }
        public LessonClass Get(Guid id)
        {
            return Db.LessonClasses.SingleOrDefault(e => e.TableRowGuid == id);
        }
        public LessonClass Get(Guid? userId)
        {
            return Db.LessonClasses.SingleOrDefault(e => e.TableRowGuid == userId);
        }

        public JsTable<LessonClass> Get(PageDetail pageDetail)
        {
            var query = Db.LessonClasses.AsQueryable();

            List<Expression<Func<LessonClass, bool>>> filters = new List<Expression<Func<LessonClass, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.SubjectTeacherClassRoom.Teacher.FirstName + m.SubjectTeacherClassRoom.Teacher.FatherName + m.SubjectTeacherClassRoom.Teacher.GrandFatherName+m.SubjectTeacherClassRoom.Subject.SubjectName+m.SubjectTeacherClassRoom.ClassRoom.ClassRoomName+m.Day.DayName+m.Period.PeriodName+m.Period.Session.SessionName).Contains(pageDetail.Search));


            Func<LessonClass, LessonClass> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_LessonClassList> GetList(PageDetail pageDetail)
        {
            var query = Db.vw_LessonClassList.AsQueryable();

            List<Expression<Func<vw_LessonClassList, bool>>> filters = new List<Expression<Func<vw_LessonClassList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.TeacherProgram.Contains(pageDetail.Search));


            Func<vw_LessonClassList, vw_LessonClassList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_LessonClassListForTeacher> GetListByTeacher(PageDetail pageDetail, string userId)
        {
            Guid userGuid = Guid.Parse(userId);
            Guid TeacherGuid = Db.Teachers.Where(m => m.LogInUserId == userGuid).Select(x => x.TableRowGuid).Single();

            var query = (from m in Db.LessonClasses
                         join t in Db.Teachers on m.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                         where t.TableRowGuid == TeacherGuid
                         select new vw_LessonClassListForTeacher
                         {

                         }
                             ).AsQueryable();

            List<Expression<Func<vw_LessonClassListForTeacher, bool>>> filters = new List<Expression<Func<vw_LessonClassListForTeacher, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.TeacherProgram.Contains(pageDetail.Search));


            Func<vw_LessonClassListForTeacher, vw_LessonClassListForTeacher> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }               

        public Response Validate(LessonClass LessonClass)
        {
            if (string.IsNullOrWhiteSpace(LessonClass.TeacherGuid.ToString()))
                return new FailedResponse("Please provide a valid Teacher Name.");
            if (string.IsNullOrWhiteSpace(LessonClass.SubjectTeacherClassRoomGuid.ToString()))
                return new FailedResponse("Please provide a valid Class Room Name.");
            if (string.IsNullOrWhiteSpace(LessonClass.DayGuid.ToString()))
                return new FailedResponse("Please provide a valid Day Name.");
            if (string.IsNullOrWhiteSpace(LessonClass.PeriodGuid.ToString()))
                return new FailedResponse("Please provide a valid Period Name.");

            if (LessonClass.TableRowGuid == Guid.Empty)
            {
                var querySubjectTeacherClassRoom = Db.LessonClasses.Where(r => r.SubjectTeacherClassRoomGuid == LessonClass.SubjectTeacherClassRoomGuid);
                var queryDay = Db.LessonClasses.Where(r => r.DayGuid == LessonClass.DayGuid);
                var queryPeriod = Db.LessonClasses.Where(r => r.PeriodGuid == LessonClass.PeriodGuid);
                if ((querySubjectTeacherClassRoom.ToList().Count > 0) && (queryDay.ToList().Count > 0) && (queryPeriod.ToList().Count > 0))
                    return new FailedResponse("A Lesson Class provided has already been created. Please provide an other lesson class.");
            }
            else
            {
                var querySubjectTeacherClassRoom = Db.LessonClasses.Where(r => r.SubjectTeacherClassRoomGuid == LessonClass.SubjectTeacherClassRoomGuid && r.TableRowGuid != LessonClass.TableRowGuid);
                var queryDay = Db.LessonClasses.Where(r => r.DayGuid == LessonClass.DayGuid && r.TableRowGuid != LessonClass.TableRowGuid);
                var queryPeriod = Db.LessonClasses.Where(r => r.PeriodGuid == LessonClass.PeriodGuid && r.TableRowGuid != LessonClass.TableRowGuid);
                if ((querySubjectTeacherClassRoom.ToList().Count > 0) && (queryDay.ToList().Count > 0) && (queryPeriod.ToList().Count > 0))
                    return new FailedResponse("A Lesson Class provided has already been created. Please provide an other lesson class.");
            }

            if (LessonClass.TableRowGuid == Guid.Empty)
            {
                var querySubjectTeacherClassRoom = Db.LessonClasses.Where(r => r.DayGuid == LessonClass.DayGuid).Select(x => x.SubjectTeacherClassRoomGuid);
                if (querySubjectTeacherClassRoom.ToList().Count > 0)
                    return new FailedResponse("This Subject will be assign once for Day of Week. Please provide an other subject." );
            }
            else
            {
                var querySubjectTeacherClassRoom = Db.LessonClasses.Where(r => r.DayGuid == LessonClass.DayGuid && r.TableRowGuid != LessonClass.TableRowGuid).Select(x => x.SubjectTeacherClassRoomGuid);               
                if (querySubjectTeacherClassRoom.ToList().Count > 0)                 
                    return new FailedResponse("An Teacher Name provided has already been created. Please provide an other teacher name.");
            }

            return new SuccessResponse("");
        }
        public Response Add(LessonClass LessonClass, Guid? userid)
        {
            Response response = new Response();

            response = Validate(LessonClass);
            if (!response.Success) return response;

            LessonClass.TableRowGuid = Guid.NewGuid();



            LessonClass.UserId = userid;

            return base.Add(LessonClass);
        }
        public Response Update(LessonClass LessonClass, Guid? userid)
        {
            Response response = new Response();

            response = Validate(LessonClass);
            if (!response.Success) return response;

            LessonClass.UserId = userid;

            return base.Update(LessonClass);
        }
        public Response Delete(LessonClass LessonClass, Guid? userid)
        {
            LessonClass.UserId = userid;
            return base.Delete(LessonClass);
        }
        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new LessonClass() { TableRowGuid = id }, userid);
        }


    }
}
