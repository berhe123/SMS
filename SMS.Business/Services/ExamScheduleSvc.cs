using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class ExamScheduleSvc : EntityBaseSvc<ExamSchedule>, IComboEntitySvc<ExamSchedule, ExamSchedule, ExamSchedule>
    {
        readonly ClassRoomSvc classroomSvc;
        readonly SubjectTeacherClassRoomSvc subjectteacherclassroomSvc;
        readonly DaySvc daySvc;
        readonly SessionSvc sessionSvc;
        readonly TeacherSvc teacherSvc;

        ComboItem comboItem = new ComboItem();

        public ExamScheduleSvc(ILogger logger)
            : base(logger, "ExamSchedule")
        {
            classroomSvc = new ClassRoomSvc(logger);
            subjectteacherclassroomSvc = new SubjectTeacherClassRoomSvc(logger);
            daySvc = new DaySvc(logger);
            sessionSvc = new SessionSvc(logger);
            teacherSvc = new TeacherSvc(logger);
        }
        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.ExamSchedules
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text =  m.SubjectTeacherClassRoom.ClassRoom.ClassRoomName + " class room has " + m.SubjectTeacherClassRoom.Subject.SubjectName + " exam on " + m.Day.DayName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.ExamSchedules
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.SubjectTeacherClassRoom.ClassRoom.ClassRoomName + " class room has " + m.SubjectTeacherClassRoom.Subject.SubjectName + " exam on " + m.Day.DayName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetClassRoomsByTeacherId()
        {
            Guid teacherId = new Guid();

            var q = from m in Db.ExamSchedules
                    join c in Db.ClassRooms on m.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    where m.SubjectTeacherClassRoom.TeacherGuid == teacherId
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = c.ClassRoomName
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetClassRoomsByTeacherId(Guid teacherId)
        {
            var q = from m in Db.ExamSchedules
                    join c in Db.ClassRooms on m.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    where m.SubjectTeacherClassRoom.TeacherGuid == teacherId
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = c.ClassRoomName
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetExamsByTeacherUserId(string userId)
        {
            Guid userGuid = Guid.Parse(userId);
            Guid TeacherGuid = Db.Teachers.Where(m => m.LogInUserId == userGuid).Select(x => x.TableRowGuid).Single();

            var q = from m in Db.ExamSchedules
                    join t in Db.Teachers on m.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                    join c in Db.ClassRooms on m.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    join s in Db.Subjects on m.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                    where t.TableRowGuid == TeacherGuid
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = c.ClassRoomName + " => " + s.SubjectName
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetSubjectsByClassRoomId(Guid userId, Guid classroomId)
        {
            Guid teacherGuid = Db.Teachers.Where(m => m.LogInUserId == userId).Select(x => x.TableRowGuid).Single();

            var q = from m in Db.ExamSchedules
                    join t in Db.Teachers on m.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                    join c in Db.ClassRooms on m.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    join s in Db.Subjects on m.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                    where (c.TableRowGuid == classroomId) 
                    && (t.TableRowGuid == teacherGuid)
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = s.SubjectName + " => out of " + m.OutOf.ToString()
                    };
            return q.AsEnumerable();
        }
        
        public ExamSchedule GetServiceFilters()
        {
            ExamSchedule ExamSchedule = new ExamSchedule();
            return new ExamSchedule();
        }
        public IQueryable<ExamSchedule> Get()
        {
            return Db.ExamSchedules;
        }
        public ExamSchedule GetModel(Guid? userId)
        {
            return new ExamSchedule
            {
                ClassRooms = classroomSvc.GetComboItems(),                
                Days = daySvc.GetComboItems(),
                Sessions = sessionSvc.GetComboItems()
            };

        }
        public ExamSchedule GetModelByUser(Guid? userId)
        {
            return new ExamSchedule
            {
                ClassRooms = classroomSvc.GetComboItems(),
                Days = daySvc.GetComboItems(),
                Sessions = sessionSvc.GetComboItems()
            };
        }
        public ExamSchedule GetModel(Guid id)
        {
            var ExamSchedule = this.Get(id);

            ExamSchedule.ClassRooms = classroomSvc.GetComboItems(Db);           
            ExamSchedule.Days = daySvc.GetComboItems(Db);
            ExamSchedule.Sessions = sessionSvc.GetComboItems(Db);
            ExamSchedule.classroomGuid = (from c in Db.SubjectTeacherClassRooms
                                          where c.TableRowGuid == ExamSchedule.SubjectTeacherClassRoomGuid
                                          select c.ClassRoomGuid).Single();
            ExamSchedule.Teachers = subjectteacherclassroomSvc.GetTeacherByClassRoomGuid(ExamSchedule.classroomGuid);

            return ExamSchedule;
        }
        public ExamSchedule GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }
        public ExamSchedule Get(Guid id)
        {
            return Db.ExamSchedules.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<ExamSchedule> Get(PageDetail pageDetail)
        {
            var query = Db.ExamSchedules.AsQueryable();

            List<Expression<Func<ExamSchedule, bool>>> filters = new List<Expression<Func<ExamSchedule, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.SubjectTeacherClassRoom.Subject.SubjectName + m.SubjectTeacherClassRoom.ClassRoom.ClassRoomName + m.Day.DayName).Contains(pageDetail.Search));

            Func<ExamSchedule, ExamSchedule> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_ExamScheduleList> GetList(PageDetail pageDetail)
        {
            var query = Db.vw_ExamScheduleList.AsQueryable();

            List<Expression<Func<vw_ExamScheduleList, bool>>> filters = new List<Expression<Func<vw_ExamScheduleList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.ExamSchedule.Contains(pageDetail.Search));


            Func<vw_ExamScheduleList, vw_ExamScheduleList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(ExamSchedule ExamSchedule)
        {
            if (string.IsNullOrWhiteSpace(ExamSchedule.SubjectTeacherClassRoomGuid.ToString()))
                return new FailedResponse("Please provide a valid Teacher Name.");

            if (string.IsNullOrWhiteSpace(ExamSchedule.DayGuid.ToString()))
                return new FailedResponse("Please provide a valid Day.");

            if (string.IsNullOrWhiteSpace(ExamSchedule.SessionGuid.ToString()))
                return new FailedResponse("Please provide a valid Session.");
            
            if (string.IsNullOrWhiteSpace(ExamSchedule.StartTime.ToString()))
                return new FailedResponse("Please provide a valid Start Time.");

            if (string.IsNullOrWhiteSpace(ExamSchedule.EndTime.ToString()))
                return new FailedResponse("Please provide a valid End Time.");

            if (string.IsNullOrWhiteSpace(ExamSchedule.OutOf.ToString()))
                return new FailedResponse("Please provide a valid Out Of.");

            if (string.IsNullOrWhiteSpace(ExamSchedule.GivenDate.ToString()))
                return new FailedResponse("Please provide a valid given Date.");

            if (ExamSchedule.TableRowGuid == Guid.Empty)
            {
                var querySubjectTeacherClassRoom = Db.ExamSchedules.Where(r => r.SubjectTeacherClassRoomGuid == ExamSchedule.SubjectTeacherClassRoomGuid);

                if ((querySubjectTeacherClassRoom.ToList().Count > 0))
                    return new FailedResponse("A Teacher and Subject provided has already been created. Please provide a different Teacher or Subject.");               
            }
            else
            {
                var querySubjectTeacherClassRoom = Db.ExamSchedules.Where(r => r.SubjectTeacherClassRoomGuid == ExamSchedule.SubjectTeacherClassRoomGuid && r.TableRowGuid != ExamSchedule.TableRowGuid);

                if((querySubjectTeacherClassRoom.ToList().Count > 0))
                    return new FailedResponse("A Teacher and Subject provided has already been created. Please provide a different Teacher or Subject."); 
            }

            if (ExamSchedule.TableRowGuid == Guid.Empty)
            {
                var queryClassRoom = Db.ExamSchedules.Where(r => r.SubjectTeacherClassRoomGuid == ExamSchedule.SubjectTeacherClassRoomGuid).Select(x => x.SubjectTeacherClassRoom.ClassRoomGuid);
                var queryDay = Db.ExamSchedules.Where(r => r.DayGuid == ExamSchedule.DayGuid);
                var querySession = Db.ExamSchedules.Where(r => r.SessionGuid == ExamSchedule.SessionGuid);
                var queryStartTime = Db.ExamSchedules.Where(r => r.StartTime == ExamSchedule.StartTime);
                var queryEndTime = Db.ExamSchedules.Where(r => r.EndTime == ExamSchedule.EndTime);
                var queryGivenDate = Db.ExamSchedules.Where(r => r.GivenDate == ExamSchedule.GivenDate);

                if ((queryClassRoom.ToList().Count > 0) && (queryStartTime.ToList().Count > 0) && (queryEndTime.ToList().Count > 0) && (queryDay.ToList().Count > 0) && (querySession.ToList().Count > 0) && (queryGivenDate.ToList().Count > 0))
                    return new FailedResponse("A Exam Schedule provided has already been created. Please provide a different schedule.");
            }
            else
            {
                var queryClassRoom = Db.ExamSchedules.Where(r => r.SubjectTeacherClassRoomGuid == ExamSchedule.SubjectTeacherClassRoomGuid && r.TableRowGuid != ExamSchedule.TableRowGuid).Select(x => x.SubjectTeacherClassRoom.ClassRoomGuid);
                var queryDay = Db.ExamSchedules.Where(r => r.DayGuid == ExamSchedule.DayGuid && r.TableRowGuid != ExamSchedule.TableRowGuid);
                var queryStartTime = Db.ExamSchedules.Where(r => r.StartTime == ExamSchedule.StartTime && r.TableRowGuid != ExamSchedule.TableRowGuid);
                var queryEndTime = Db.ExamSchedules.Where(r => r.EndTime == ExamSchedule.EndTime && r.TableRowGuid != ExamSchedule.TableRowGuid);
                var querySession = Db.ExamSchedules.Where(r => r.SessionGuid == ExamSchedule.SessionGuid && r.TableRowGuid != ExamSchedule.TableRowGuid);
                var queryGivenDate = Db.ExamSchedules.Where(r => r.GivenDate == ExamSchedule.GivenDate && r.TableRowGuid != ExamSchedule.TableRowGuid);

                if ((queryClassRoom.ToList().Count > 0) && (queryStartTime.ToList().Count > 0) && (queryEndTime.ToList().Count > 0) && (queryDay.ToList().Count > 0) && (querySession.ToList().Count > 0) && (queryGivenDate.ToList().Count > 0))
                    return new FailedResponse("An Exam Schedule provided has already been created. Please provide a different schedule.");
            }

            return new SuccessResponse("");
        }
        public Response Add(ExamSchedule ExamSchedule, Guid? userid)
        {
            Response response = new Response();

            response = Validate(ExamSchedule);
            if (!response.Success) return response;

            ExamSchedule.TableRowGuid = Guid.NewGuid();
            ExamSchedule.UserId = userid;            

            return base.Add(ExamSchedule);
        }
        public Response Update(ExamSchedule ExamSchedule, Guid? userid)
        {
            Response response = new Response();

            response = Validate(ExamSchedule);
            if (!response.Success) return response;

            ExamSchedule.UserId = userid;
            
             return base.Update(ExamSchedule);            
        }
        public Response Delete(ExamSchedule ExamSchedule, Guid? userid)
        {
            ExamSchedule.UserId = userid;
            return base.Delete(ExamSchedule);
        }
        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new ExamSchedule() { TableRowGuid = id }, userid);
        }


    }
}
