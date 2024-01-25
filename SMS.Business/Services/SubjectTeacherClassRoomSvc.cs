using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class SubjectTeacherClassRoomSvc : EntityBaseSvc<SubjectTeacherClassRoom>, IComboEntitySvc<SubjectTeacherClassRoom, SubjectTeacherClassRoom, SubjectTeacherClassRoom>
    {
        readonly ClassRoomSvc classroomSvc;
        readonly SubjectSvc subjectSvc;
        readonly TeacherSvc teacherSvc;

        public SubjectTeacherClassRoomSvc(ILogger logger)
            : base(logger, "SubjectTeacherClassRoom")
        {
            classroomSvc = new ClassRoomSvc(logger);
            subjectSvc = new SubjectSvc(logger);
            teacherSvc = new TeacherSvc(logger);            
        }

        public IEnumerable<ComboItem> GetComboItems()
        {            
            var q = from m in Db.SubjectTeacherClassRooms
                    select new ComboItem()
                    {
                        Value = m.TeacherGuid,
                        Text = m.Teacher.FirstName + " " + m.Teacher.FatherName + " " + m.Teacher.GrandFatherName
                    };
            return q.AsEnumerable(); 
        }
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.SubjectTeacherClassRooms
                    select new ComboItem()
                    {
                        Value = m.TeacherGuid,
                        Text = m.Teacher.FirstName + " " + m.Teacher.FatherName + " " + m.Teacher.GrandFatherName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetClassRoomSubjectByTeacherGuid(Guid userId)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == userId).Select(x => x.TableRowGuid).Single();
            var q = from m in Db.SubjectTeacherClassRooms
                    join s in Db.Subjects on m.Subject.TableRowGuid equals s.TableRowGuid
                    join c in Db.ClassRooms on m.ClassRoom.TableRowGuid equals c.TableRowGuid
                    join t in Db.Teachers on m.Teacher.TableRowGuid equals t.TableRowGuid
                    where t.TableRowGuid == teacherGuid
                    select new ComboItem
                    {
                        Value = m.TableRowGuid,
                        Text = m.ClassRoom.ClassRoomName + " => " + m.Subject.SubjectName 
                    };
            return q.AsQueryable();
        }
        public IEnumerable<ComboItem> GetSubjectClassRoomsByTeacherGuid(Guid teacherGuid)
        {
            var q = from m in Db.SubjectTeacherClassRooms
                    join s in Db.Subjects on m.Subject.TableRowGuid equals s.TableRowGuid
                    join c in Db.ClassRooms on m.ClassRoom.TableRowGuid equals c.TableRowGuid
                    join t in Db.Teachers on m.Teacher.TableRowGuid equals t.TableRowGuid
                    where t.TableRowGuid == teacherGuid
                    select new ComboItem
                    {
                        Value = m.TableRowGuid,
                        Text = m.ClassRoom.ClassRoomName + " => " + m.Subject.SubjectName
                    };
            return q.AsQueryable();
        }
        public IEnumerable<ComboItem> GetTeacherByClassRoomGuid(Guid classroomId)
        {
            var q = from m in Db.SubjectTeacherClassRooms
                    join c in Db.ClassRooms on m.ClassRoomGuid equals c.TableRowGuid
                    join s in Db.Subjects on m.SubjectGuid equals s.TableRowGuid
                    join t in Db.Teachers on m.TeacherGuid equals t.TableRowGuid
                    where c.TableRowGuid == classroomId
                    select new ComboItem
                    {
                        Value = m.TableRowGuid,
                        Text = m.Teacher.FirstName + " " + m.Teacher.FatherName + " " + m.Teacher.GrandFatherName + " teaches " + m.Subject.SubjectName
                    };
            return q.AsQueryable();
        }
        public IEnumerable<ComboItem> GetClassRoomsByTeacherGuid(Guid teacherId)
        {
            var q = from stc in Db.SubjectTeacherClassRooms
                        join c in Db.ClassRooms on stc.ClassRoomGuid equals c.TableRowGuid
                        join t in Db.Teachers on stc.TeacherGuid equals t.TableRowGuid
                        where stc.TeacherGuid == teacherId
                        select new ComboItem
                        {
                            Value = stc.ClassRoom.TableRowGuid,
                            Text = c.ClassRoomName
                        };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetSubjectsByClassRoomId(Guid userId, Guid classroomId)
        {
            Guid TeacherGuid = Db.Teachers.Where(m => m.LogInUserId == userId).Select(x => x.TableRowGuid).Single();

            var q = from m in Db.SubjectTeacherClassRooms
                    join s in Db.Subjects on m.SubjectGuid equals s.TableRowGuid
                    join t in Db.Teachers on m.TeacherGuid equals t.TableRowGuid
                    join c in Db.ClassRooms on m.ClassRoomGuid equals c.TableRowGuid
                    where (m.ClassRoomGuid == classroomId) 
                    && (m.TeacherGuid == TeacherGuid)
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = s.SubjectName
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetsubjectsByClassRoomId(Guid teacherId, Guid classroomId)
        {
            var q = from m in Db.SubjectTeacherClassRooms
                    join s in Db.Subjects on m.SubjectGuid equals s.TableRowGuid
                    join t in Db.Teachers on m.TeacherGuid equals t.TableRowGuid
                    join c in Db.ClassRooms on m.ClassRoomGuid equals c.TableRowGuid
                    where (m.ClassRoomGuid == classroomId)
                    && (m.TeacherGuid == teacherId)
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = s.SubjectName
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetClassRoomSubjectsByTeacherGuid(Guid teacherGuid)
        {
            var q = from stc in Db.SubjectTeacherClassRooms
                    join c in Db.ClassRooms on stc.ClassRoomGuid equals c.TableRowGuid
                    join s in Db.Subjects on stc.SubjectGuid equals s.TableRowGuid
                    join t in Db.Teachers on stc.TeacherGuid equals t.TableRowGuid
                    where t.TableRowGuid == teacherGuid
                    select new ComboItem
                    {
                        Value = stc.TableRowGuid,
                        Text = c.ClassRoomName + " => " + s.SubjectName
                    };
            return q.AsEnumerable();
        }

        public SubjectTeacherClassRoom GetServiceFilters()
        {
            SubjectTeacherClassRoom SubjectTeacherClassRoom = new SubjectTeacherClassRoom();
            return new SubjectTeacherClassRoom();
        }
        public IQueryable<SubjectTeacherClassRoom> Get()
        {
            return Db.SubjectTeacherClassRooms;
        }
        public SubjectTeacherClassRoom GetModel(Guid? userId)
        {
            return new SubjectTeacherClassRoom
            {
                ClassRooms = classroomSvc.GetComboItems(),
                Subjects = subjectSvc.GetComboItems(),
                Teachers = teacherSvc.GetComboItems(),               
            };

        }
        public SubjectTeacherClassRoom GetModelByUser(Guid? userId)
        {
            return new SubjectTeacherClassRoom
            {
                ClassRooms = classroomSvc.GetComboItems(),
                Subjects = subjectSvc.GetComboItems(),
                Teachers = teacherSvc.GetComboItems(),
            };
        }
        public SubjectTeacherClassRoom GetModel(Guid id)
        {
            var SubjectTeacherClassRoom = this.Get(id);

            SubjectTeacherClassRoom.ClassRooms = classroomSvc.GetComboItems(Db);
            SubjectTeacherClassRoom.Subjects = subjectSvc.GetComboItems(Db);
            SubjectTeacherClassRoom.Teachers = teacherSvc.GetComboItems(Db);
            SubjectTeacherClassRoom.homeClassRoom = bool.Parse(SubjectTeacherClassRoom.HomeClassRoom.ToString());

            return SubjectTeacherClassRoom;
        }
        public SubjectTeacherClassRoom GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }
        public SubjectTeacherClassRoom Get(Guid id)
        {
            return Db.SubjectTeacherClassRooms.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<SubjectTeacherClassRoom> Get(PageDetail pageDetail)
        {
            var query = Db.SubjectTeacherClassRooms.AsQueryable();

            List<Expression<Func<SubjectTeacherClassRoom, bool>>> filters = new List<Expression<Func<SubjectTeacherClassRoom, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.Teacher.FirstName+" "+m.Teacher.FatherName+" "+m.Teacher.GrandFatherName).Contains(pageDetail.Search));


            Func<SubjectTeacherClassRoom, SubjectTeacherClassRoom> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_SubjectTeacherClassRoomList> GetList(PageDetail pageDetail)
        {
            var query = Db.vw_SubjectTeacherClassRoomList.AsQueryable();

            List<Expression<Func<vw_SubjectTeacherClassRoomList, bool>>> filters = new List<Expression<Func<vw_SubjectTeacherClassRoomList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.SubjectTeacherClassRoom.Contains(pageDetail.Search));


            Func<vw_SubjectTeacherClassRoomList, vw_SubjectTeacherClassRoomList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(SubjectTeacherClassRoom SubjectTeacherClassRoom)
        {
            if (string.IsNullOrWhiteSpace(SubjectTeacherClassRoom.ClassRoomGuid.ToString()))
                return new FailedResponse("Please provide a valid Class Room.");

            if (string.IsNullOrWhiteSpace(SubjectTeacherClassRoom.SubjectGuid.ToString()))
                return new FailedResponse("Please provide a valid Subject.");

            if (string.IsNullOrWhiteSpace(SubjectTeacherClassRoom.TeacherGuid.ToString()))
                return new FailedResponse("Please provide a valid Grand Teacher.");

            if (SubjectTeacherClassRoom.TableRowGuid == Guid.Empty)
            {
                var queryTeacher = Db.SubjectTeacherClassRooms.Where(r => r.TeacherGuid == SubjectTeacherClassRoom.TeacherGuid);
                var queryClassRoom = Db.SubjectTeacherClassRooms.Where(r => r.ClassRoomGuid == SubjectTeacherClassRoom.ClassRoomGuid);
                var querySubject = Db.SubjectTeacherClassRooms.Where(r => r.SubjectGuid == SubjectTeacherClassRoom.SubjectGuid);
                var queryHomeClassRoomTeacher = Db.SubjectTeacherClassRooms.Where(r => r.TeacherGuid == SubjectTeacherClassRoom.TeacherGuid).Select(x => x.HomeClassRoom);
                var queryhomeclassroomTeacher = Db.SubjectTeacherClassRooms.Where(r => r.ClassRoomGuid == SubjectTeacherClassRoom.ClassRoomGuid).Select(x => x.HomeClassRoom);

                //if ((queryTeacher.ToList().Count > 0) && (queryClassRoom.ToList().Count > 0) && (querySubject.ToList().Count > 0))
                //    return new FailedResponse("A Subject, Teacher and ClassRoom provided has already been created. Please provide a different Teacher or Subject or ClassRoom.");
                //if ((queryClassRoom.ToList().Count > 0) && (querySubject.ToList().Count > 0))
                //    return new FailedResponse("A Subject and ClassRoom provided has already been created. Please provide a different Subject or ClassRoom.");
                if (SubjectTeacherClassRoom.homeClassRoom.Equals(true))
                {
                    if (queryHomeClassRoomTeacher.ToList().Contains(true))
                        return new FailedResponse("A Teacher provided has already been Home Teacher. Please provide an Other Teacher for this Class Room."); 
                }
                if (SubjectTeacherClassRoom.homeClassRoom.Equals(true))
                {
                    if(queryhomeclassroomTeacher.ToList().Contains(true))
                        return new FailedResponse("This Class Room has already been Home Teacher. Please provide an Other Class Room for this Teacher."); 
                }                                        
            }
            else
            {
                var queryTeacher = Db.SubjectTeacherClassRooms.Where(r => r.TeacherGuid == SubjectTeacherClassRoom.TeacherGuid && r.TableRowGuid != SubjectTeacherClassRoom.TableRowGuid);
                var queryClassRoom = Db.SubjectTeacherClassRooms.Where(r => r.ClassRoomGuid == SubjectTeacherClassRoom.ClassRoomGuid && r.TableRowGuid != SubjectTeacherClassRoom.TableRowGuid);
                var querySubject = Db.SubjectTeacherClassRooms.Where(r => r.SubjectGuid == SubjectTeacherClassRoom.SubjectGuid && r.TableRowGuid != SubjectTeacherClassRoom.TableRowGuid);
                var queryHomeClassRoomTeacher = Db.SubjectTeacherClassRooms.Where(r => r.TeacherGuid == SubjectTeacherClassRoom.TeacherGuid && r.TableRowGuid != SubjectTeacherClassRoom.TableRowGuid).Select(x => x.HomeClassRoom);
                var queryhomeclassroomTeacher = Db.SubjectTeacherClassRooms.Where(r => r.ClassRoomGuid == SubjectTeacherClassRoom.ClassRoomGuid && r.TableRowGuid != SubjectTeacherClassRoom.TableRowGuid).Select(x => x.HomeClassRoom);

                if ((queryTeacher.ToList().Count > 0) && (queryClassRoom.ToList().Count > 0) && (querySubject.ToList().Count > 0))
                    return new FailedResponse("A Subject, Teacher and ClassRoom provided has already been created. Please provide a different Teacher or Subject or ClassRoom.");
                if ((queryClassRoom.ToList().Count > 0) && (querySubject.ToList().Count > 0))
                    return new FailedResponse("A Subject and ClassRoom provided has already been created. Please provide a different Subject or ClassRoom.");
                if (SubjectTeacherClassRoom.homeClassRoom.Equals(true))
                {
                    if (queryHomeClassRoomTeacher.ToList().Contains(true))
                        return new FailedResponse("A Teacher provided has already been Home Teacher. Please provide an Other Teacher for this Class Room.");
                }
                if (SubjectTeacherClassRoom.homeClassRoom.Equals(true))
                {
                    if (queryhomeclassroomTeacher.ToList().Contains(true))
                        return new FailedResponse("This Class Room has already been Home Teacher. Please provide an Other Class Room for this Teacher.");
                }
            }                     
            return new SuccessResponse("");
        }
        public Response Add(SubjectTeacherClassRoom SubjectTeacherClassRoom, Guid? userid)
        {
            Response response = new Response();

            response = Validate(SubjectTeacherClassRoom);
            if (!response.Success) return response;

            SubjectTeacherClassRoom.TableRowGuid = Guid.NewGuid();
            SubjectTeacherClassRoom.HomeClassRoom = SubjectTeacherClassRoom.homeClassRoom;
            SubjectTeacherClassRoom.UserId = userid;

            return base.Add(SubjectTeacherClassRoom);
        }
        public Response Update(SubjectTeacherClassRoom SubjectTeacherClassRoom, Guid? userid)
        {
            Response response = new Response();

            response = Validate(SubjectTeacherClassRoom);
            if (!response.Success) return response;

            SubjectTeacherClassRoom.UserId = userid;

            return base.Update(SubjectTeacherClassRoom);
        }
        public Response Delete(SubjectTeacherClassRoom SubjectTeacherClassRoom, Guid? userid)
        {
            SubjectTeacherClassRoom.UserId = userid;
            return base.Delete(SubjectTeacherClassRoom);
        }
        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new SubjectTeacherClassRoom() { TableRowGuid = id }, userid);
        }
    }
}
