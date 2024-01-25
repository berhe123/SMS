using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class IndividualWorkScheduleSvc : EntityBaseSvc<IndividualWorkSchedule>, IComboEntitySvc<IndividualWorkSchedule, IndividualWorkSchedule, IndividualWorkSchedule>
    {
        readonly SubjectTeacherClassRoomSvc subjectteacherclassroomSvc;
        public IndividualWorkScheduleSvc(ILogger logger)
            : base(logger, "IndividualWorkSchedule")
        {
            subjectteacherclassroomSvc = new SubjectTeacherClassRoomSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.IndividualWorkSchedules
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.SubjectTeacherClassRoom.Teacher.FirstName + " " + m.SubjectTeacherClassRoom.Teacher.FatherName + " " + m.SubjectTeacherClassRoom.Teacher.GrandFatherName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.IndividualWorkSchedules
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.SubjectTeacherClassRoom.Teacher.FirstName + " " + m.SubjectTeacherClassRoom.Teacher.FatherName + " " + m.SubjectTeacherClassRoom.Teacher.GrandFatherName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetIndividualWorksByTeacherGuid(Guid userId)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == userId).Select(x => x.TableRowGuid).Single();
            var q = from m in Db.IndividualWorkSchedules
                    join s in Db.Subjects on m.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                    join c in Db.ClassRooms on m.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    join t in Db.Teachers on m.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                    where t.TableRowGuid == teacherGuid
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.IndividualWorkName + "(" + m.OutOf.ToString() + ")" + " on " + s.SubjectName + " for " + c.ClassRoomName
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetClassRoomsByTeacherId()
        {
            Guid teacherId = new Guid();

            var q = from m in Db.IndividualWorkSchedules
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
            var q = from m in Db.IndividualWorkSchedules
                    join c in Db.ClassRooms on m.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    where m.SubjectTeacherClassRoom.TeacherGuid == teacherId
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = c.ClassRoomName
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetSubjectsByClassRoomId()
        {
            Guid  classroomGuid = new Guid();

            var q = from m in Db.IndividualWorkSchedules
                    join s in Db.Subjects on m.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                    where m.SubjectTeacherClassRoom.ClassRoomGuid == classroomGuid
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = s.SubjectName
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetSubjectsByClassRoomId(Guid classroomGuid)
        {
            var q = from m in Db.IndividualWorkSchedules
                    join s in Db.Subjects on m.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                    where m.SubjectTeacherClassRoom.ClassRoomGuid == classroomGuid
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = s.SubjectName
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetIndividualWorksByteacherId()
        {
            var q = from m in Db.IndividualWorkSchedules
                    join c in Db.ClassRooms on m.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    join s in Db.Subjects on m.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                    join t in Db.Teachers on m.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                    where t.TableRowGuid == m.SubjectTeacherClassRoom.TeacherGuid
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = c.ClassRoomName + " => " + s.SubjectName + " which " + m.IndividualWorkName + " out of " + m.OutOf.ToString()
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetIndividualWorksByteacherId(Guid teacherId)
        {            
            var q = from m in Db.IndividualWorkSchedules
                    join c in Db.ClassRooms on m.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    join s in Db.Subjects on m.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                    join t in Db.Teachers on m.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                    where t.TableRowGuid == teacherId
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = c.ClassRoomName + " => " + s.SubjectName + " which " + m.IndividualWorkName + " out of " + m.OutOf.ToString() 
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetIndividualWorksByClassRoomIdandTeacherId()
        {
            Guid classroomId = new Guid();
            Guid teacherId = new Guid();

            var q = from m in Db.IndividualWorkSchedules
                    join c in Db.ClassRooms on m.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    join s in Db.Subjects on m.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                    join t in Db.Teachers on m.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                    where (t.TableRowGuid == teacherId)
                    && (c.TableRowGuid == classroomId)
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = s.SubjectName + " => " + m.IndividualWorkName + " out of " + m.OutOf.ToString()
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetIndividualWorkSchedulesBySubjectId(Guid subjectGuid, Guid userId)
        {
            Guid teacherGuid = Db.Teachers.Where(m => m.LogInUserId == userId).Select(m => m.TableRowGuid).SingleOrDefault();

            var q = from m in Db.IndividualWorkSchedules
                    join c in Db.ClassRooms on m.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    join s in Db.Subjects on m.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                    join t in Db.Teachers on m.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                    where (t.TableRowGuid == teacherGuid)
                    && (s.TableRowGuid == subjectGuid)
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text =  m.IndividualWorkName + " out of " + m.OutOf.ToString()
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetSubjectsByTeacherUserIdandClassRoomId(Guid classroomId, string userId)
        {
            Guid userGuid = Guid.Parse(userId);
            Guid TeacherGuid = Db.Teachers.Where(m => m.LogInUserId == userGuid).Select(x => x.TableRowGuid).Single();

            var q = from a in Db.IndividualWorkSchedules
                    join c in Db.ClassRooms on a.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    join t in Db.Teachers on a.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                    join s in Db.Subjects on a.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                    where (c.TableRowGuid == classroomId) && (t.TableRowGuid == TeacherGuid)
                    select new ComboItem
                    {
                        Value = a.TableRowGuid,
                        Text = s.SubjectName
                    };
            return q.AsEnumerable();

        }

        public IndividualWorkSchedule GetServiceFilters()
        {
            IndividualWorkSchedule IndividualWorkSchedule = new IndividualWorkSchedule();
            return new IndividualWorkSchedule();
        }
        public IQueryable<IndividualWorkSchedule> Get()
        {
            return Db.IndividualWorkSchedules;
        }
        public IndividualWorkSchedule GetModel(Guid? userId)
        {
            Guid teacherId = Db.Teachers.Where(x => x.LogInUserId == userId.Value).Select(x => x.TableRowGuid).Single();

            return new IndividualWorkSchedule
            {
                ClassRooms = subjectteacherclassroomSvc.GetSubjectClassRoomsByTeacherGuid(teacherId)
            };
        }
        public IndividualWorkSchedule GetModelByUser(Guid? userId)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == userId.Value).Select(x => x.TableRowGuid).Single();

            return new IndividualWorkSchedule
            {
                ClassRooms = subjectteacherclassroomSvc.GetClassRoomSubjectsByTeacherGuid(teacherGuid)
            };
        }
        public IndividualWorkSchedule GetModel(Guid id)
        {
            var IndividualWorkSchedule = this.Get(id);
            Guid TeacherGuid = Db.IndividualWorkSchedules.Where(x => x.TableRowGuid == id).Select(x => x.SubjectTeacherClassRoom.TeacherGuid).Single();
            IndividualWorkSchedule.ClassRooms = subjectteacherclassroomSvc.GetClassRoomSubjectsByTeacherGuid(TeacherGuid);

            return IndividualWorkSchedule;
        }
        public IndividualWorkSchedule GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }
        public IndividualWorkSchedule Get(Guid id)
        {           
            return Db.IndividualWorkSchedules.SingleOrDefault(e => e.TableRowGuid == id);
        }       

        public JsTable<IndividualWorkSchedule> Get(PageDetail pageDetail)
        {
            var query = Db.IndividualWorkSchedules.AsQueryable();

            List<Expression<Func<IndividualWorkSchedule, bool>>> filters = new List<Expression<Func<IndividualWorkSchedule, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.SubjectTeacherClassRoom.Teacher.FirstName + " " + m.SubjectTeacherClassRoom.Teacher.FatherName + " " + m.SubjectTeacherClassRoom.Teacher.GrandFatherName).Contains(pageDetail.Search));


            Func<IndividualWorkSchedule, IndividualWorkSchedule> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_IndividualWorkScheduleList> GetList(PageDetail pageDetail)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == pageDetail.UserId).Select(x => x.TableRowGuid).Single();
           
            var query = Db.vw_IndividualWorkScheduleList.AsQueryable();

            List<Expression<Func<vw_IndividualWorkScheduleList, bool>>> filters = new List<Expression<Func<vw_IndividualWorkScheduleList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.IndividualWork.Contains(pageDetail.Search));

            filters.Add(x => x.TeacherGuid == teacherGuid);

            Func<vw_IndividualWorkScheduleList, vw_IndividualWorkScheduleList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_IndividualWorkScheduleList> GetAllIndividualWorksForDataView(PageDetail pageDetail)
        {
            var query = Db.vw_IndividualWorkScheduleList.AsQueryable();

            List<Expression<Func<vw_IndividualWorkScheduleList, bool>>> filters = new List<Expression<Func<vw_IndividualWorkScheduleList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.IndividualWorkName.Contains(pageDetail.Search));

            filters.Add(x => x.SubjectTeacherClassRoomGuid == pageDetail.SubjectTeacherClassRoomGuid);

            Func<vw_IndividualWorkScheduleList, vw_IndividualWorkScheduleList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(IndividualWorkSchedule IndividualWorkSchedule)
        {
            //if (string.IsNullOrWhiteSpace(IndividualWorkSchedule.FirstName))
            //    return new FailedResponse("Please provide a valid First Name.");

            //if (string.IsNullOrWhiteSpace(IndividualWorkSchedule.FatherName))
            //    return new FailedResponse("Please provide a valid Father Name.");

            //if (string.IsNullOrWhiteSpace(IndividualWorkSchedule.GrandFatherName))
            //    return new FailedResponse("Please provide a valid Grand Father Name.");

            //if (string.IsNullOrWhiteSpace(IndividualWorkSchedule.BirthDate.ToString()))
            //    return new FailedResponse("Please provide a valid Birth Date.");

            //if (string.IsNullOrWhiteSpace(IndividualWorkSchedule.GenderGuid.ToString()))
            //    return new FailedResponse("Please provide a valid Sex.");

            //if (string.IsNullOrWhiteSpace(IndividualWorkSchedule.RegistrationDate.ToString()))
                //return new FailedResponse("Please provide a valid Registration Date.");

            //if (char.MaxValue.Equals(5))
            //{
            //    return new FailedResponse("Please provide a letter only for First Name.");
            //}

            //if (IndividualWorkSchedule.TableRowGuid == Guid.Empty)
            //{
            //    var query = Db.IndividualWorkSchedules.Where(r => r.IndividualWorkScheduleName == IndividualWorkSchedule.IndividualWorkScheduleName);
            //    if (query.ToList().Count > 0)
            //        return new FailedResponse("A IndividualWorkSchedule Name provided has already been created. Please provide a different name.");
            //}
            //else
            //{
            //    var query = Db.IndividualWorkSchedules.Where(r => r.IndividualWorkScheduleName == IndividualWorkSchedule.IndividualWorkScheduleName && r.TableRowGuid != IndividualWorkSchedule.TableRowGuid);
            //    if (query.ToList().Count > 0)
            //        return new FailedResponse("An IndividualWorkSchedule Name provided has already been created. Please provide a different name.");
            //}

            return new SuccessResponse("");
        }
        public Response Add(IndividualWorkSchedule IndividualWorkSchedule, Guid? userid)
        {
            Response response = new Response();

            response = Validate(IndividualWorkSchedule);
            if (!response.Success) return response;

            IndividualWorkSchedule.TableRowGuid = Guid.NewGuid();
            IndividualWorkSchedule.UserId = userid;            

            return base.Add(IndividualWorkSchedule);
        }
        public Response Update(IndividualWorkSchedule IndividualWorkSchedule, Guid? userid)
        {
            Response response = new Response();

            response = Validate(IndividualWorkSchedule);
            if (!response.Success) return response;

            IndividualWorkSchedule.UserId = userid;
            
             return base.Update(IndividualWorkSchedule);            
        }
        public Response Delete(IndividualWorkSchedule IndividualWorkSchedule, Guid? userid)
        {
            IndividualWorkSchedule.UserId = userid;
            return base.Delete(IndividualWorkSchedule);
        }
        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new IndividualWorkSchedule() { TableRowGuid = id }, userid);
        }

    }
}
